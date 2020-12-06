﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MovieShop.Core.RepositoryInterfaces
{
    public interface IAsyncRepository<T> where T:class
    {
        Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> ListAllAsync();

        Task<IEnumerable<T>> ListAsync(Expression<Func<T,bool>> filter);

        Task<IEnumerable<T>> ListAllWithIncludesAsync(Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] includes);

        Task<int> GetCountAsync(Expression<Func<T,bool>> filter=null);

        Task<bool> GetExistsAsync(Expression<Func<T, bool>> filter=null);

        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddAsync(IEnumerable<T> entity);
        
        Task<T> UpdateAsync(T entity);
        Task<IEnumerable<T>> UpdateAsync(IEnumerable<T> entity);
        
        Task DeleteAsync(T entity);
        Task DeleteAsync(IEnumerable<T> entity);
    }
}