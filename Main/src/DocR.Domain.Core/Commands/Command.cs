using System;
using DocR.Domain.Core.Events;
using MediatR;

namespace DocR.Domain.Core.Commands
{
    public class Command : Message, IRequest
    {
        public DateTime TimeStamp { get; private set; }

        public Command()
        {
            TimeStamp = DateTime.Now;
        }
    }
}