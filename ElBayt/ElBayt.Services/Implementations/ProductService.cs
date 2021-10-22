using ElBayt.Common.Core.Logging;
using ElBayt.Common.Core.Mapping;
using ElBayt.Common.Core.SecurityModels;
using ElBayt.Core.Entities;
using ElBayt.Core.IUnitOfWork;
using ElBayt.DTO.ELBaytDTO_s;
using ElBayt.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ElBayt.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IELBaytUnitOfWork _unitOfWork;
        private readonly IUserIdentity _userIdentity;
        private readonly ILogger _logger;
        private readonly ITypeMapper _mapper;

        public ProductService(IELBaytUnitOfWork unitOfWork, IUserIdentity userIdentity, ILogger logger,
              ITypeMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userIdentity = userIdentity ?? throw new ArgumentNullException(nameof(userIdentity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task AddNewProduct(ProductDTO product)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(product, correlationGuid, nameof(ProductService), nameof(AddNewProduct), 1, _userIdentity.Name);

                #endregion Logging info
                var Entity = _mapper.Map<ProductDTO, ProductEntity>(product);
                Entity.Id = Guid.NewGuid();
                await _unitOfWork.ProductRepository.AddAsync(Entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(product, correlationGuid, $"{nameof(ProductService)}_{nameof(AddNewProduct)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task AddNewProductCategory(ProductCategoryDTO productCategory)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(productCategory, correlationGuid, nameof(ProductService), nameof(AddNewProduct), 1, _userIdentity.Name);

                #endregion Logging info
                var Entity = _mapper.Map<ProductCategoryDTO, ProductCategoryEntity>(productCategory);
                Entity.Id = Guid.NewGuid();
                await _unitOfWork.ProductCategoryRepository.AddAsync(Entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(productCategory, correlationGuid, $"{nameof(ProductService)}_{nameof(AddNewProduct)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task AddNewProductType(ProductTypeDTO productType)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(productType, correlationGuid, nameof(ProductService), nameof(AddNewProduct), 1, _userIdentity.Name);

                #endregion Logging info
                var Entity = _mapper.Map<ProductTypeDTO, ProductTypeEntity>(productType);
                Entity.Id = Guid.NewGuid();
                await _unitOfWork.ProductTypeRepository.AddAsync(Entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(productType, correlationGuid, $"{nameof(ProductService)}_{nameof(AddNewProduct)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        #region Department
        public async Task AddNewProductDepartment(ProductDepartmentDTO productDepartment)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(productDepartment, correlationGuid, nameof(ProductService), nameof(AddNewProduct), 1, _userIdentity.Name);

                #endregion Logging info
                var Entity = _mapper.Map<ProductDepartmentDTO, ProductDepartmentEntity>(productDepartment);
                Entity.Id = Guid.NewGuid();
                await _unitOfWork.ProductDepartmentRepository.AddAsync(Entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(productDepartment, correlationGuid, $"{nameof(ProductService)}_{nameof(AddNewProduct)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task<string> DeleteProductDepartment(Guid id)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(id, correlationGuid, nameof(ProductService), nameof(DeleteProductDepartment), 1, _userIdentity.Name);

                #endregion Logging info

                var IsDeleted = await _unitOfWork.ProductDepartmentRepository.RemoveAsync(id);
                if (IsDeleted)
                {
                    var res = await _unitOfWork.SaveAsync();
                    return "true";

                }
                return "This Item Not Exist";
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(id, correlationGuid, $"{nameof(ProductService)}_{nameof(DeleteProductDepartment)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                return ex.Message;
            }
        }

        public object GetProductDepartments()
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail("GetProductDepartments", correlationGuid, nameof(ProductService), nameof(GetProductDepartments), 1, _userIdentity.Name);

                #endregion Logging info
             
                return _unitOfWork.ProductDepartmentRepository.GetAll();

            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail("GetProductDepartments", correlationGuid, $"{nameof(ProductService)}_{nameof(GetProductDepartments)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task UpdateProductDepartment(ProductDepartmentDTO ProductDepartment)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(ProductDepartment, correlationGuid, nameof(ProductService), nameof(UpdateProductDepartment), 1, _userIdentity.Name);

                #endregion Logging info


                var Entity = _mapper.Map<ProductDepartmentDTO, ProductDepartmentEntity>(ProductDepartment);
                await _unitOfWork.ProductDepartmentRepository.UpdateProductDepartment(Entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(ProductDepartment, correlationGuid, $"{nameof(ProductService)}_{nameof(UpdateProductDepartment)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task<ProductDepartmentDTO> GetProductDepartment(Guid Id)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Id, correlationGuid, nameof(ProductService), nameof(GetProductDepartment), 1, _userIdentity.Name);

                #endregion Logging info

                var Model = await _unitOfWork.ProductDepartmentRepository.GetAsync(Id);
                return _mapper.Map<ProductDepartmentEntity, ProductDepartmentDTO>(Model);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Id, correlationGuid, $"{nameof(ProductService)}_{nameof(GetProductDepartment)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        #endregion
    }
}
