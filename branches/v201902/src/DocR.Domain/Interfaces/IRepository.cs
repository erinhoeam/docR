using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DocR.Domain.Core.Models;

namespace DocR.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity<TEntity>
    {
        void Adicionar(TEntity entity);
        void Atualizar(TEntity entity);
        void Remover(Guid id);
        TEntity ObterPorId(Guid id);
        IEnumerable<TEntity> ObterTodos();
        IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate);
        int SaveChange();
    }
}