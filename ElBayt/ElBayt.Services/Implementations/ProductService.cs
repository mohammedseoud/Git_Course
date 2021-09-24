using AutoMapper;
using ElBayt.Common.Common;
using ElBayt.Common.Core.Logging;
using ElBayt.Common.Core.Mapping;
using ElBayt.Common.Core.SecurityModels;
using ElBayt.Common.Security;
using ElBayt.Core.Entities;
using ElBayt.Core.IUnitOfWork;
using ElBayt.DTO.ELBaytDTO_s;
using ElBayt.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

                _logger.InfoInDetail(correlationGuid, correlationGuid, nameof(ProductService), nameof(AddNewProduct), 1, _userIdentity.Name);

                #endregion Logging info
                var Entity = _mapper.Map<ProductDTO, ProductEntity>(product);
                Entity.Id = Guid.NewGuid();
                await _unitOfWork.ProductRepository.AddAsync(Entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(correlationGuid, correlationGuid, $"{nameof(ProductService)}_{nameof(AddNewProduct)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

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

                _logger.InfoInDetail(correlationGuid, correlationGuid, nameof(ProductService), nameof(AddNewProduct), 1, _userIdentity.Name);

                #endregion Logging info
                var Entity = _mapper.Map<ProductCategoryDTO, ProductCategoryEntity>(productCategory);
                Entity.Id = Guid.NewGuid();
                await _unitOfWork.ProductCategoryRepository.AddAsync(Entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(correlationGuid, correlationGuid, $"{nameof(ProductService)}_{nameof(AddNewProduct)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task AddNewProductDepartment(ProductDepartmentDTO productDepartment)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(correlationGuid, correlationGuid, nameof(ProductService), nameof(AddNewProduct), 1, _userIdentity.Name);

                #endregion Logging info
                var Entity = _mapper.Map<ProductDepartmentDTO, ProductDepartmentEntity>(productDepartment);
                Entity.Id = Guid.NewGuid();
                await _unitOfWork.ProductDepartmentRepository.AddAsync(Entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(correlationGuid, correlationGuid, $"{nameof(ProductService)}_{nameof(AddNewProduct)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

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

                _logger.InfoInDetail(correlationGuid, correlationGuid, nameof(ProductService), nameof(AddNewProduct), 1, _userIdentity.Name);

                #endregion Logging info
                var Entity = _mapper.Map<ProductTypeDTO, ProductTypeEntity>(productType);
                Entity.Id = Guid.NewGuid();
                await _unitOfWork.ProductTypeRepository.AddAsync(Entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(correlationGuid, correlationGuid, $"{nameof(ProductService)}_{nameof(AddNewProduct)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }
    }
}
