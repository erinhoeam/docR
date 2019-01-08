using DocR.Domain.Core.Notifications;
using DocR.Domain.Interfaces;
using FluentValidation.Results;
using MediatR;

namespace DocR.Domain.CommandHandlers
{
    public class CommandHandler
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediatorHandler _mediator;
        private readonly DomainNotificationHandler _notifications;

        protected CommandHandler(IUnitOfWork uow,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator)
        {
            _uow = uow;
            _notifications = (DomainNotificationHandler)notifications;
            _mediator = mediator;
        }

        protected void NotificarValidacoesError(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                _mediator.PublicarEvento(new DomainNotification(error.PropertyName, error.ErrorMessage));
            }
        }

        protected bool Commit()
        {
            //TODO: Validar se há alguma validação de negocio com erro.

            if (_notifications.HasNotifications()) return false;

            var commandResponse = _uow.Commit();

            if (commandResponse.Success) return true;

            _mediator.PublicarEvento(new DomainNotification("Commit", "Ocorreu um erro ao salvar os dados."));
            return false;
        }
    }
}