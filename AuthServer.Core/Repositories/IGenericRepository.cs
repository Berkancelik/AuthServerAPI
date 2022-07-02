﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.Reposityories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {

        Task<TEntity> GetBtIdAsync(int id);
        Task<IQueryable<TEntity>> GetAllAsync();
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);   
        Task AddAsync(TEntity entity);
        TEntity Update(TEntity entity);
        void Remove(TEntity entity);

    }
}