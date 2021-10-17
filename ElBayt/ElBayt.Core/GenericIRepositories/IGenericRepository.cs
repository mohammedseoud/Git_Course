using ElBayt.Common.Entities;
using ElBayt.Common.Infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ElBayt.Common.Common;

namespace ElBayt.Core.GenericIRepository
{
    public interface IGenericRepository<TEntity,TId>
        where TEntity : BaseEntity
    {
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void AddRange(List<TEntity> entities);
        Task AddRangeAsync(List<TEntity> entities);
        TEntity Get(TId id);
        Task<TEntity> GetAsync(TId id);
        IQueryable<BaseModel> GetAll(Expression<Func<BaseModel,
            bool>> filter = null, Func<IQueryable<BaseModel>,
        IOrderedQueryable<BaseModel>> orderby = null, string IncludeProperties = null);
        TEntity GetFirstOrDefault(Expression<Func<BaseModel, bool>> filter = null, string IncludeProperties = null);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<BaseModel, bool>> filter = null, string IncludeProperties = null);
        bool Remove(TId id);
        Task<bool> RemoveAsync(TId id);
        bool RemoveRange(IEnumerable<TEntity> entities);
        bool ReomveEntity(TEntity entity);

    }
}
