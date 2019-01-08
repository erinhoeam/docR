using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using DocR.Domain.Core.Notifications;
using DocR.Domain.Interfaces;
using DocR.Service.Setting;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace DocR.Service.Controllers
{
    public class DocController : BaseController
    {

        private readonly IMediatorHandler _mediator;
        private readonly Settings _settings;

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);
        public DocController(INotificationHandler<DomainNotification> notifications,
            IUser user,
            IMapper mapper,
            IOptions<Settings> settingsAccessor,
            IMediatorHandler mediator) : base(notifications, user, mediator)
        {
            _mediator = mediator;
            _settings = settingsAccessor.Value;
        }

        // GET api/values
        [HttpPost]
        [Route("upload-image")]
        public async Task<IActionResult> UploadImage(Guid id, IFormFile file)
        {
            return Response(new {  });
        }

        private async Task ReadHandwrittenText(string imageFilePath)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _settings.SubscriptionKey);

            const string requestParameters = "mode=printed";

            var uri = $"{_settings.UriBase}?{requestParameters}";

            HttpResponseMessage response;

            string operationLocation;

            var byteData = GetImageAsByteArray(imageFilePath);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                response = await client.PostAsync(uri, content);
            }

            if (response.IsSuccessStatusCode)
                operationLocation = response.Headers.GetValues("Operation-Location").FirstOrDefault();
            else
            {
                var errorString = await response.Content.ReadAsStringAsync();
                Console.WriteLine("\n\nResponse:\n{0}\n",
                    JToken.Parse(errorString));
                return;
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
                Console.WriteLine("\nTimeout error.\n");
                return;
            }

            Console.WriteLine("\nResponse:\n\n{0}\n",
                JToken.Parse(contentString).ToString());
            
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
    }
}
