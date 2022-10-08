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
    public class GenericRepository<TModel, TId> : IGenericRepository<TModel, TId>
        where TModel : BaseModel
    {
        private readonly DbContext _dbContext;
        private DbSet<TModel> _set;

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _set = _dbContext.Set<TModel>();
        }
        public void Add(TModel model)
        {
            try
            {
                _set.Add(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddAsync(TModel model)
        {
            try
            {
                await _set.AddAsync(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddRange(IEnumerable<TModel> models)
        {
            try
            {
                _dbContext.AddRange(models);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }

        }

        public async Task AddRangeAsync(IEnumerable<TModel> models)
        {
            try
            {
                await _dbContext.AddRangeAsync(models);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TModel Get(TId id) => _set.Find(id);

        public async Task<TModel> GetAsync(TId id) => await _set.FindAsync(id);

        public async Task<IEnumerable<TModel>> GetAllAsync(Expression<Func<TModel, bool>> filter = null,
                        Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderby = null,
                        string IncludeProperties = null)
        {
            IQueryable<TModel> query = _set;
            if (filter != null)
            {
                query = _set.Where(filter).AsNoTracking();
            }
            if (IncludeProperties != null)
            {
                var properties = IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var property in properties)
                {
                    query = query.Include(property);
                }
            }
            if (orderby != null) { query = orderby(query); }
            var result = await query.ToListAsync();
            return result;
        }

        public IEnumerable<TModel> GetAll(Expression<Func<TModel, bool>> filter = null,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderby = null,
            string IncludeProperties = null)
        {
            IQueryable<TModel> query = _set;
            if (filter != null)
            {
                query = _set.Where(filter).AsNoTracking();
            }
            if (IncludeProperties != null)
            {
                var properties = IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var property in properties)
                {
                    query = query.Include(property);
                }
            }
            if (orderby != null) { query = orderby(query); }
            var result = query.ToList();
            return result;
        }

        public TModel GetFirstOrDefault(Expression<Func<TModel, bool>> filter = null, string IncludeProperties = null)
        {
            IQueryable<TModel> query = _set;
            if (filter != null) { query = _set.Where(filter).AsNoTracking(); }
            if (IncludeProperties != null)
            {
                var properties = IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var property in properties)
                {
                    query = query.Include(property);
                }
            }
            return query.FirstOrDefault(); ;
        }

        public async Task<TModel> FirstOrDefaultAsync(Expression<Func<TModel, bool>> filter = null, string IncludeProperties = null)
        {
            IQueryable<TModel> query = _set;
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

        public bool Remove(TId id)
        {
            var entity = _set.Find(id);
            if (entity == null) { return false; }
            try
            {
                _set.Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> RemoveAsync(TId id)
        {
            var entity = await _set.FindAsync(id);
            if(entity == null) { return false; }
            try
            {
                _set.Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RemoveRange(IEnumerable<TModel> models)
        {

            try
            {
                _set.RemoveRange(models);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ReomveEntity(TModel model)
        {
            try
            {
                _set.Remove(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
