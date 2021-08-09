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
        void Add(BaseEntity entity);
        Task AddAsync(BaseEntity entity);
        void AddRange(List<BaseEntity> entities);
        Task AddRangeAsync(List<BaseEntity> entities);
        BaseEntity Get(int id);
        Task<BaseEntity> GetAsync(int id);
        IQueryable<BaseModel<TEntity>> GetAll(Expression<Func<BaseModel<TEntity>,
            bool>> filter = null, Func<IQueryable<BaseModel<TEntity>>,
        IOrderedQueryable<BaseModel<TEntity>>> orderby = null, string IncludeProperties = null);
        BaseEntity GetFirstOrDefault(Expression<Func<BaseModel<TEntity>, bool>> filter = null, string IncludeProperties = null);
        Task<BaseEntity> FirstOrDefaultAsync(Expression<Func<BaseModel<TEntity>, bool>> filter = null, string IncludeProperties = null);
        void Remove(int id);
        Task RemoveAsync(int id);
        void RemoveRange(IEnumerable<BaseEntity> entities);
        void ReomveEntity(BaseEntity entity);

    }
}
