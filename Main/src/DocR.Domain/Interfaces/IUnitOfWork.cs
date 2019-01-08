using System;
using DocR.Domain.Core.Commands;

namespace DocR.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        CommandResponse Commit();
    }
}