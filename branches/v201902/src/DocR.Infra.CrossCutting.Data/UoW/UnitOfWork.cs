using DocR.Domain.Core.Commands;
using DocR.Domain.Interfaces;
using DocR.Infra.CrossCutting.Data.Context;

namespace DocR.Infra.CrossCutting.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataBaseContext _context;

        public UnitOfWork(DataBaseContext context)
        {
            _context = context;
        }

        public CommandResponse Commit()
        {
            var rowsAffected = _context.SaveChanges();
            return new CommandResponse(rowsAffected > 0);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}