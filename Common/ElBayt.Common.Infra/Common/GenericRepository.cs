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
    public class GenericRepository<Entity> : IGenericRepository<Entity>
    {
        private readonly DbContext _dbContext;
        private readonly ITypeMapper _mapper;
        private DbSet<BaseModel<Entity>> _set;

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _set = _dbContext.Set<BaseModel<Entity>>();
            _mapper = new TypeMapper();
        }
        public void Add(BaseEntity entity)
        {
            var model = _mapper.Map<BaseModel<Entity>>(entity);
            _set.Add(model);
        }

        public async Task AddAsync(BaseEntity entity)
        {
            var model = _mapper.Map<BaseModel<Entity>>(entity);
            await _set.AddAsync(model);
        }

        public void AddRange(List<BaseEntity> entities)
        {
            var models = new List<BaseModel<Entity>>();
            foreach (var entity in entities)
            {
                var model = _mapper.Map<BaseModel<Entity>>(entity);
                models.Add(model);
            }

            _dbContext.AddRange(models);
        }

        public async Task AddRangeAsync(List<BaseEntity> entities)
        {
            var models = new List<BaseModel<Entity>>();
            foreach (var entity in entities)
            {
                var model = _mapper.Map<BaseModel<Entity>>(entity);
                models.Add(model);
            }

            await _dbContext.AddRangeAsync(models);
        }

        public BaseEntity Get(int id)
        {
            var model = _set.Find(id);
            var entity = _mapper.Map<BaseEntity>(model);
            return entity;
        }

        public async Task<BaseEntity> GetAsync(int id)
        {
            var model = await _set.FindAsync(id);
            var entity = _mapper.Map<BaseEntity>(model);
            return entity;
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
        public BaseEntity GetFirstOrDefault(Expression<Func<BaseModel<Entity>, bool>> filter = null, string IncludeProperties = null)
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
            var model = query.FirstOrDefault();
            var entity = _mapper.Map<BaseEntity>(model);
            return entity;
        }

        public async Task<BaseEntity> FirstOrDefaultAsync(Expression<Func<BaseModel<Entity>, bool>> filter = null, string IncludeProperties = null)
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

            var model = await query.FirstOrDefaultAsync();
            var entity = _mapper.Map<BaseEntity>(model);
            return entity;
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

        public void RemoveRange(IEnumerable<BaseEntity> entities)
        {
            var models = new List<BaseModel<Entity>>();
            foreach (var entity in entities)
            {
                var model = _mapper.Map<BaseModel<Entity>>(entity);
                models.Add(model);
            }


            _set.RemoveRange(models);
        }

        public void ReomveEntity(BaseEntity entity)
        {
            var model = _mapper.Map<BaseModel<Entity>>(entity);
            _set.Remove(model);
        }
    }
}
