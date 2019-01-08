using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using DocR.Domain.Core.Notifications;
using DocR.Domain.Interfaces;
using DocR.Service.Model;
using DocR.Service.Setting;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DocR.Service.Controllers
{
    public class DocController : BaseController
    {

        private readonly IMediatorHandler _mediator;
        private readonly Settings _settings;
        private readonly IMapper _mapper;
        private IHostingEnvironment _hostingEnvironment;


        public DocController(INotificationHandler<DomainNotification> notifications,
            IUser user,
            IMapper mapper,
            IOptions<Settings> settingsAccessor,
            IMediatorHandler mediator, IHostingEnvironment hostingEnvironment) : base(notifications, user, mediator)
        {
            _mediator = mediator;
            _settings = settingsAccessor.Value;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET api/values
        [HttpPost]
        [Route("upload-image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            //var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

            //var filePath = Path.Combine(uploads, file.FileName);
            //using (var fileStream = new FileStream(filePath, FileMode.Create))
            //{
            //    await file.CopyToAsync(fileStream);
            //}

            var fileStream = ReadFully(file.OpenReadStream());
            await ReadHandwrittenText(fileStream);
            return Response(new {  });
        }

        private async Task<IActionResult> ReadHandwrittenText(byte[] byteData)
        {
            var client = new HttpClient();

            var response = await EnviarImagemCognitive(client, byteData);

            string operationLocation;

            if (response.IsSuccessStatusCode)
                operationLocation = response.Headers.GetValues("Operation-Location").FirstOrDefault();
            else
            {
                var errorString = await response.Content.ReadAsStringAsync();
                
                NotificarError("1", errorString);

                return Response();
            }

            string contentString;
            var i = 0;
            do
            {
                System.Threading.Thread.Sleep(1000);
                response = await client.GetAsync(operationLocation);
                contentString = await response.Content.ReadAsStringAsync();
                ++i;
            }
            while (i < 10 && contentString.IndexOf("\"status\":\"Succeeded\"", StringComparison.Ordinal) == -1);

            if (i == 10 && contentString.IndexOf("\"status\":\"Succeeded\"", StringComparison.Ordinal) == -1)
            {
                NotificarError("1", "Timeout");
                return Response();
            }

            var obj = JsonConvert.DeserializeObject<TextOperationResult>(contentString);

            return Response(obj);
        }

        private async Task< HttpResponseMessage> EnviarImagemCognitive(HttpClient client, byte[] byteData)
        {
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _settings.SubscriptionKey);

            const string requestParameters = "mode=Printed";

            var uri = $"{_settings.UriBase}?{requestParameters}";

            HttpResponseMessage response;

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                response = await client.PostAsync(uri, content);
            }

            return response;
        }
        private static byte[] GetImageAsByteArray(string imageFilePath)
        {
            using (var fileStream =
                new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
            {
                var binaryReader = new BinaryReader(fileStream);
                return binaryReader.ReadBytes((int)fileStream.Length);
            }
        }
        public static byte[] ReadFully(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
