using ElBayt.Common.Common;
using ElBayt.Common.Entities;
using ElBayt.Common.Infra.Mapping;
using ElBayt.Common.Infra.Models;
using ElBayt.Common.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ElBayt.Common.Infra.Common
{
    public  class GenericRepository<Entity>: IGenericRepository<Entity>
    {
        private readonly DbContext _dbContext;
        private DbSet<BaseModel<Entity>> _set;

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _set = _dbContext.Set<BaseModel<Entity>>();
        }
        public void Add(EnhancedEntity<Entity> entity)
        {
            _set.Add(entity);
        }

        public async Task AddAsync(BaseModel<Entity> entity)
        {
            await _set.AddAsync(entity);
        }

        public void AddRange(List<Entity> entities)
        {
            _dbContext.AddRange(entities);
        }

        public async Task AddRangeAsync(List<Entity> entities)
        {
          await  _dbContext.AddRangeAsync(entities);
        }

        public BaseModel<Entity> Get(int id)
        {
            return _set.Find(id);
        }

        public async Task<BaseModel<Entity>> GetAsync(int id)
        {
            return await _set.FindAsync(id);
        }

        public IQueryable<BaseModel<Entity>> GetAll(Expression<Func<BaseModel<Entity>, bool>> filter = null, Func<IQueryable<BaseModel<Entity>>,
            IOrderedQueryable<BaseModel<Entity>>> orderby = null, string IncludeProperties = null)
        {
            IQueryable<BaseModel<Entity>> query = _set;
            if (filter != null) { query = _set.Where(filter).AsNoTracking(); }
            if (IncludeProperties != null)
            {
                var properties = IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var property in properties)
                {
                    query = query.Include(property);
                }
            }
            if (orderby != null) { query = orderby(query); }
            return query;
        }

        
        public BaseModel<Entity> GetFirstOrDefault(Expression<Func<BaseModel<Entity>, bool>> filter = null, string IncludeProperties = null)
        {
            IQueryable<BaseModel<Entity>> query = _set;
            if (filter != null) { query = _set.Where(filter).AsNoTracking(); }
            if (IncludeProperties != null)
            {
                var properties = IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var property in properties)
                {
                    query = query.Include(property);
                }
            }
            return query.FirstOrDefault();
        }

        public async Task<BaseModel<Entity>> FirstOrDefaultAsync(Expression<Func<BaseModel<Entity>, bool>> filter = null, string IncludeProperties = null)
        {
            IQueryable<BaseModel<Entity>> query = _set;
            if (filter != null) { query = _set.Where(filter).AsNoTracking(); }
            if (IncludeProperties != null)
            {
                var properties = IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var property in properties)
                {
                    query = query.Include(property);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public void Remove(int id)
        {
            var entity = _set.Find(id);
            _set.Remove(entity);
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await _set.FindAsync(id);
            _set.Remove(entity);
        }

        public void RemoveRange(IEnumerable<BaseModel<Entity>> entities)
        {
            _set.RemoveRange(entities);
        }
      
        public void ReomveEntity(BaseModel<Entity> entity)
        {
            _set.Remove(entity);
        }
    }
}
