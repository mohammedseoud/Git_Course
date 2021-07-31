using ElBayt.Common.Entities;
using ElBayt.Common.Infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ElBayt.Common.Infra.Common
{
    public interface IGenericRepository<TEntity> 
    {
        void Add(IEquatable<TEntity> entity);
        Task AddAsync(IEquatable<TEntity> entity);
        void AddRange(List<TEntity> entities);
        Task AddRangeAsync(List<TEntity> entities);
        BaseModel<TEntity> Get(int id);
        Task<BaseModel<TEntity>> GetAsync(int id);
        IQueryable<BaseModel<TEntity>> GetAll(Expression<Func<BaseModel<TEntity>,
            bool>> filter = null, Func<IQueryable<BaseModel<TEntity>>,
        IOrderedQueryable<BaseModel<TEntity>>> orderby = null, string IncludeProperties = null);
        BaseModel<TEntity> GetFirstOrDefault(Expression<Func<BaseModel<TEntity>, bool>> filter = null, string IncludeProperties = null);
        Task<BaseModel<TEntity>> FirstOrDefaultAsync(Expression<Func<BaseModel<TEntity>, bool>> filter = null, string IncludeProperties = null);
        void Remove(int id);
        Task RemoveAsync(int id);
        void RemoveRange(IEnumerable<BaseModel<TEntity>> entities);
        void ReomveEntity(BaseModel<TEntity> entity);

    }
}
