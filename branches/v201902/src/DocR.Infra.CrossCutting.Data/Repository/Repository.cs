using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DocR.Domain.Core.Models;
using DocR.Domain.Interfaces;
using DocR.Infra.CrossCutting.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DocR.Infra.CrossCutting.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<TEntity>
    {
        protected DataBaseContext Db;
        protected DbSet<TEntity> DbSet;

        protected Repository(DataBaseContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }
        public virtual void Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Atualizar(TEntity entity)
        {
            Db.Update(entity);
        }

        public virtual void Remover(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public virtual TEntity ObterPorId(Guid id)
        {
            return DbSet.AsNoTracking().FirstOrDefault(n => n.Id == id);
        }

        public virtual IEnumerable<TEntity> ObterTodos()
        {
            return DbSet.ToList();
        }

        public virtual IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate);
        }

        public int SaveChange()
        {
            return Db.SaveChanges();
        }
        public void Dispose()
        {
            Db.Dispose();
        }
    }
}