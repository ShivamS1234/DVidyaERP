﻿using System;
using System.Collections.Generic;
using SQLite.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace DVidyaERP.Data
{
    public interface IDatabase<T> where T : class, new()
    {
        Task<List<T>> Get();
        Task<T> Get(int id);
        Task<List<T>> Get<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null);
        Task<T> Get(Expression<Func<T, bool>> predicate);
       // AsyncTableQuery<T> AsQueryable();
        Task<int> Insert(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(T entity);
    }
}
