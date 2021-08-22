using AutoMapper;
using ElBayt.Common.Common;
using ElBayt.Common.Entities;
using ElBayt.Common.Infra.Mapping;
using ElBayt.Common.Infra.Models;
using ElBayt.Common.Core.Mapping;
using ElBayt.Core.GenericIRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ElBayt.Common.Infra.Common
{
    public class GenericRepository<TEntity,TModel> : IGenericRepository<TEntity> 
        where TModel : BaseModel
        where TEntity:BaseEntity
    {
        private readonly DbContext _dbContext;
        private readonly ITypeMapper _mapper;
        private DbSet<TModel> _set;

        public GenericRepository(DbContext dbContext, ITypeMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _set = _dbContext.Set<TModel>();
            _mapper = mapper;
        }
        public void Add(TEntity entity)
        {
            
            var model = _mapper.Map<TEntity, TModel>(entity);
            _set.Add(model);
        }

        public async Task AddAsync(TEntity entity)
        {
            var model = _mapper.Map<TEntity, TModel>(entity);
            await _set.AddAsync(model);
        }

        public void AddRange(List<TEntity> entities)
        {
            var models = new List<TModel>();
            foreach (var entity in entities)
            {
                var model = _mapper.Map<TEntity, TModel>(entity);
                models.Add(model);
            }

            _dbContext.AddRange(models);
        }

        public async Task AddRangeAsync(List<TEntity> entities)
        {
            var models = new List<TModel>();
            foreach (var entity in entities)
            {
                var model = _mapper.Map<TEntity, TModel>(entity);
                models.Add(model);
            }

            await _dbContext.AddRangeAsync(models);
        }

        public TEntity Get(int id)
        {
            var model = _set.Find(id);
            var entity = _mapper.Map<TModel, TEntity>(model);
            return entity;
        }

        public async Task<TEntity> GetAsync(int id)
        {
            var model = await _set.FindAsync(id);
            var entity = _mapper.Map<TModel, TEntity>(model);
            return entity;
        }

        public IQueryable<BaseModel> GetAll(Expression<Func<BaseModel, bool>> filter = null, Func<IQueryable<BaseModel>,
            IOrderedQueryable<BaseModel>> orderby = null, string IncludeProperties = null)
        {
            IQueryable<BaseModel> query = _set;
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
        public TEntity GetFirstOrDefault(Expression<Func<BaseModel, bool>> filter = null, string IncludeProperties = null)
        {
            IQueryable<BaseModel> query = _set;
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
            var entity = _mapper.Map<BaseModel, TEntity>(model);
            return entity;
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<BaseModel, bool>> filter = null, string IncludeProperties = null)
        {
            IQueryable<BaseModel> query = _set;
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
            var entity = _mapper.Map<BaseModel, TEntity>(model);
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

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            var models = new List<TModel>();
            foreach (var entity in entities)
            {
                var model = _mapper.Map<TEntity, TModel>(entity);
                models.Add(model);
            }


            _set.RemoveRange(models);
        }

        public void ReomveEntity(TEntity entity)
        {
            var model = _mapper.Map<TEntity, TModel>(entity);
            _set.Remove(model);
        }
    }
}
