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
    public interface IGenericRepository<TModel, TId>
        where TModel : BaseModel
    {
        void Add(TModel entity);
        Task AddAsync(TModel entity);
        void AddRange(IEnumerable<TModel> entities);
        Task AddRangeAsync(IEnumerable<TModel> entities);
        TModel Get(TId id);
        Task<TModel> GetAsync(TId id);
        Task<IEnumerable<TModel>> GetAllAsync(Expression<Func<TModel,
             bool>> filter = null, Func<IQueryable<TModel>,
         IOrderedQueryable<TModel>> orderby = null, string IncludeProperties = null);
        IEnumerable<TModel> GetAll(Expression<Func<TModel,
             bool>> filter = null, Func<IQueryable<TModel>,
         IOrderedQueryable<TModel>> orderby = null, string IncludeProperties = null);
        TModel GetFirstOrDefault(Expression<Func<TModel, bool>> filter = null, string IncludeProperties = null);
        Task<TModel> FirstOrDefaultAsync(Expression<Func<TModel, bool>> filter = null, string IncludeProperties = null);
        bool Remove(TId id);
        Task<bool> RemoveAsync(TId id);
        void RemoveRange(IEnumerable<TModel> entities);
        void ReomveEntity(TModel entity);

    }
}
