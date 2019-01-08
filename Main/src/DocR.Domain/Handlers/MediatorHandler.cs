using System.Threading.Tasks;
using DocR.Domain.Core.Commands;
using DocR.Domain.Core.Events;
using DocR.Domain.Interfaces;
using MediatR;

namespace DocR.Domain.Handlers
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task EnviarComando<T>(T comando) where T : Command
        {
            await _mediator.Send(comando);
        }

        public async Task PublicarEvento<T>(T evento) where T : Event
        {
            //if (!evento.MessageType.Equals("DomainNotification"))
            //_eventStore?.SalvarEvento(evento);

            await _mediator.Publish(evento);
        }
    }
}