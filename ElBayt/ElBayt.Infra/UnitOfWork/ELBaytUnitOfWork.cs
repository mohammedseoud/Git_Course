using ElBayt.Common.Common;
using ElBayt.Common.Core.Logging;
using ElBayt.Common.Core.Mapping;
using ElBayt.Common.Core.SecurityModels;
using ElBayt.Common.Security;
using ElBayt.Core.GenericIRepository;
using ElBayt.Core.IRepositories;
using ElBayt.Core.IUnitOfWork;
using ElBayt.Infra.Context;
using ElBayt.Infra.Repositories;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ElBayt.Infra.UnitOfWork
{
    public class ELBaytUnitOfWork : IELBaytUnitOfWork
    {

        private readonly ElBaytContext _context;
        private readonly ILogger _logger;
        private readonly IUserIdentity _userIdentity;
        private readonly ITypeMapper _mapper;
        private DbContextTransaction _dbContextTransaction;
        

        public ELBaytUnitOfWork(ElBaytContext context, ILogger logger, IUserIdentity userIdentity,ITypeMapper mapper)
        {
            Guard.ArgumentIsNull(context, nameof(context));
            Guard.ArgumentIsNull(logger, nameof(logger));
            Guard.ArgumentIsNull(userIdentity, nameof(userIdentity));
            Guard.ArgumentIsNull(mapper, nameof(mapper));
            _context = context;
            _logger = logger;
            _userIdentity = userIdentity;
            _mapper = mapper;
        }

       

        public void BeginTransaction()
        {
            _dbContextTransaction =(DbContextTransaction) _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _context.SaveChanges();
            _dbContextTransaction?.Commit();
        }

        public void RollBack()
        {
            _dbContextTransaction?.Rollback();
        }

        public int Save()
        {
            var contextTransaction = _context.Database.BeginTransaction();
            try
            {
                var result = _context.SaveChanges();
                contextTransaction.Commit();
                return result;
            }
            catch (DbEntityValidationException ex)
            {
                contextTransaction.Rollback();
                var correlationId = Guid.NewGuid();
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Class: {0}, Property: {1}, Error: {2}",
                            validationErrors.Entry.Entity.GetType().FullName,
                            validationError.PropertyName, validationError.ErrorMessage);

                        _logger.ErrorInDetail(validationError, correlationId,
                            $"{nameof(ELBaytUnitOfWork)}_{nameof(DbEntityValidationException)}", ex, 0,
                            _userIdentity.Name);
                    }
                }

                throw;
            }
            catch (Exception ex)
            {
                contextTransaction.Rollback();
                _logger.ErrorInDetail(nameof(Exception), Guid.NewGuid(),
                    $"{nameof(ELBaytUnitOfWork)}_{nameof(Exception)}", ex, 0, _userIdentity.Name);
                throw;
            }
        }

        public async Task<int> SaveAsync()
        {
            var contextTransaction = _context.Database.BeginTransaction();
            try
            {
                var result = await _context.SaveChangesAsync();
                contextTransaction.Commit();
                return result;
            }
            catch (DbEntityValidationException ex)
            {
                contextTransaction.Rollback();
                var correlationId = Guid.NewGuid();
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Class: {0}, Property: {1}, Error: {2}",
                            validationErrors.Entry.Entity.GetType().FullName,
                            validationError.PropertyName, validationError.ErrorMessage);

                        _logger.ErrorInDetail(validationError, correlationId,
                            $"{nameof(ELBaytUnitOfWork)}_{nameof(DbEntityValidationException)}", ex, 0,
                            _userIdentity.Name);
                    }
                }

                throw;
            }
            catch (Exception ex)
            {
                contextTransaction.Rollback();
                _logger.ErrorInDetail(nameof(Exception), Guid.NewGuid(),
                    $"{nameof(ELBaytUnitOfWork)}_{nameof(Exception)}", ex, 0, _userIdentity.Name);
                throw;
            }
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            var contextTransaction = _context.Database.BeginTransaction();
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                var result = await _context.SaveChangesAsync(cancellationToken);
                contextTransaction.Commit();
                return result;
            }
            catch (DbEntityValidationException ex)
            {
                contextTransaction.Rollback();
                var correlationId = Guid.NewGuid();
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Class: {0}, Property: {1}, Error: {2}",
                            validationErrors.Entry.Entity.GetType().FullName,
                            validationError.PropertyName, validationError.ErrorMessage);

                        _logger.ErrorInDetail(validationError, correlationId,
                            $"{nameof(ELBaytUnitOfWork)}_{nameof(DbEntityValidationException)}", ex, 0,
                            _userIdentity.Name);
                    }
                }

                throw;
            }
            catch (Exception ex)
            {
                contextTransaction.Rollback();
                _logger.ErrorInDetail(nameof(Exception), Guid.NewGuid(),
                    $"{nameof(ELBaytUnitOfWork)}_{nameof(Exception)}", ex, 0, _userIdentity.Name);
                throw;
            }
        }

       



        #region properties
        private IProductRepository _productRepository ;
        private IProductImageRepository _productImageRepository;
        private IProductCategoryRepository _productCategoryRepository;
        private IProductTypeRepository _productTypeRepository;
        private IProductDepartmentRepository _productDepartmentRepository;
        private IServiceRepository _serviceRepository;
        private IServiceDepartmentRepository _serviceDepartmentRepository;
        private ISPRepository _SPRepository;

        #endregion

        #region Getter

        public IProductRepository ProductRepository =>
            _productRepository ??= new ProductRepository(_context,_mapper);
        public IProductImageRepository ProductImageRepository =>
           _productImageRepository ??= new ProductImageRepository(_context, _mapper);
        public IProductCategoryRepository ProductCategoryRepository =>
            _productCategoryRepository ??= new ProductCategoryRepository(_context, _mapper);
        public IProductTypeRepository ProductTypeRepository =>
           _productTypeRepository ??= new ProductTypeRepository(_context, _mapper);
        public IProductDepartmentRepository ProductDepartmentRepository =>
           _productDepartmentRepository ??= new ProductDepartmentRepository(_context, _mapper);
        public IServiceRepository ServiceRepository =>
           _serviceRepository ??= new ServiceRepository(_context, _mapper);
        public IServiceDepartmentRepository ServiceDepartmentRepository =>
           _serviceDepartmentRepository ??= new ServiceDepartmentRepository(_context, _mapper);

        public ISPRepository SP => _SPRepository ??= new SPRepository(_context);
        #endregion
    }
}
