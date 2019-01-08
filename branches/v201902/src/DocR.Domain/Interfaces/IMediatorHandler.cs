using System.Threading.Tasks;
using DocR.Domain.Core.Commands;
using DocR.Domain.Core.Events;

namespace DocR.Domain.Interfaces
{
    public interface IMediatorHandler
    {
        Task EnviarComando<T>(T comando) where T : Command;
        Task PublicarEvento<T>(T evento) where T : Event;
    }
}