using System;
using System.Linq;
using DocR.Domain.Core.Notifications;
using DocR.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DocR.Service.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json", "application/json-patch+json", "multipart/form-data")]
    public abstract class BaseController : Controller
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediator;
        protected Guid ConcurseiroId { get; set; }
        // ReSharper disable once InconsistentNaming
        protected readonly IUser _user;

        protected BaseController(INotificationHandler<DomainNotification> notifications,
                                 IUser user,
                                 IMediatorHandler mediator)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediator = mediator;
            _user = user;

            if (user.IsAuthenticated())
            {
                ConcurseiroId = user.GetUserId();
            }
        }

#pragma warning disable 109
        protected new IActionResult Response(object result = null)
#pragma warning restore 109
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                erros = _notifications.GetNotifications().Select(n => n.Value)
            });
        }
        protected bool OperacaoValida()
        {
            return !_notifications.HasNotifications();
        }
        protected void NotificarError(string codigo, string mensagem)
        {
            _mediator.PublicarEvento(new DomainNotification(codigo, mensagem));
        }
        protected void AdicionarErrosIdentity(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                NotificarError(result.ToString(), error.Description);
            }
        }
        protected void NotificarErroModelInvalida()
        {
            var erros = ModelState.Values.SelectMany(n => n.Errors);

            foreach (var erro in erros)
            {
                NotificarError(string.Empty, erro.ErrorMessage);
            }
        }
    }
}