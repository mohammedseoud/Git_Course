using Dapper;
using ElBayt.Common.Core.Logging;
using ElBayt.Common.Core.Mapping;
using ElBayt.Common.Core.SecurityModels;
using ElBayt.Core.Entities;
using ElBayt.Core.IUnitOfWork;
using ElBayt.DTO.ELBaytDTO_s;
using ElBayt.Infra.SPs;
using ElBayt.Services.Contracts;
using System;
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

        #region Categories
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

        public object GetProductCategories()
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail("GetProductCategories", correlationGuid, nameof(ProductService), nameof(GetProductCategories), 1, _userIdentity.Name);

                #endregion Logging info

                return _unitOfWork.ProductCategoryRepository.GetAll();

            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail("GetProductCategories", correlationGuid, $"{nameof(ProductService)}_{nameof(GetProductCategories)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task<string> DeleteProductCategory(Guid Id)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Id, correlationGuid, nameof(ProductService), nameof(DeleteProductCategory), 1, _userIdentity.Name);

                #endregion Logging info

                var IsDeleted = await _unitOfWork.ProductCategoryRepository.RemoveAsync(Id);
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

                _logger.ErrorInDetail(Id, correlationGuid, $"{nameof(ProductService)}_{nameof(DeleteProductCategory)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                return ex.Message;
            }
        }

        public async Task UpdateProductCategory(ProductCategoryDTO ProductCategory)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(ProductCategory, correlationGuid, nameof(ProductService), nameof(UpdateProductCategory), 1, _userIdentity.Name);

                #endregion Logging info


                var Entity = _mapper.Map<ProductCategoryDTO, ProductCategoryEntity>(ProductCategory);
                await _unitOfWork.ProductCategoryRepository.UpdateProductCategory(Entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(ProductCategory, correlationGuid, $"{nameof(ProductService)}_{nameof(UpdateProductCategory)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task<ProductCategoryDTO> GetProductCategory(Guid Id)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Id, correlationGuid, nameof(ProductService), nameof(GetProductCategory), 1, _userIdentity.Name);

                #endregion Logging info

                var Model = await _unitOfWork.ProductCategoryRepository.GetAsync(Id);
                return _mapper.Map<ProductCategoryEntity, ProductCategoryDTO>(Model);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Id, correlationGuid, $"{nameof(ProductService)}_{nameof(GetProductCategory)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        #endregion

        #region Types

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

        public object GetProductTypes()
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail("GetProductTypes", correlationGuid, nameof(ProductService), nameof(GetProductTypes), 1, _userIdentity.Name);

                #endregion Logging info

                return _unitOfWork.ProductTypeRepository.GetAll();

            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail("GetProductTypes", correlationGuid, $"{nameof(ProductService)}_{nameof(GetProductTypes)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task<string> DeleteProductType(Guid Id)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Id, correlationGuid, nameof(ProductService), nameof(DeleteProductType), 1, _userIdentity.Name);

                #endregion Logging info

                var IsDeleted = await _unitOfWork.ProductTypeRepository.RemoveAsync(Id);
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

                _logger.ErrorInDetail(Id, correlationGuid, $"{nameof(ProductService)}_{nameof(DeleteProductType)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                return ex.Message;
            }
        }

        public async Task UpdateProductType(ProductTypeDTO ProductType)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(ProductType, correlationGuid, nameof(ProductService), nameof(UpdateProductType), 1, _userIdentity.Name);

                #endregion Logging info


                var Entity = _mapper.Map<ProductTypeDTO, ProductTypeEntity>(ProductType);
                await _unitOfWork.ProductTypeRepository.UpdateProductType(Entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(ProductType, correlationGuid, $"{nameof(ProductService)}_{nameof(UpdateProductType)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task<ProductTypeDTO> GetProductType(Guid Id)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Id, correlationGuid, nameof(ProductService), nameof(GetProductType), 1, _userIdentity.Name);

                #endregion Logging info

                var Model = await _unitOfWork.ProductTypeRepository.GetAsync(Id);
                return _mapper.Map<ProductTypeEntity, ProductTypeDTO>(Model);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Id, correlationGuid, $"{nameof(ProductService)}_{nameof(GetProductType)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        #endregion

        #region Departments
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

        #region Product

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

        public object GetProducts()
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail("GetProducts", correlationGuid, nameof(ProductService), nameof(GetProducts), 1, _userIdentity.Name);

                #endregion Logging info

                return _unitOfWork.ProductRepository.GetAll();

            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail("GetProducts", correlationGuid, $"{nameof(ProductService)}_{nameof(GetProducts)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task<string> DeleteProduct(Guid Id)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Id, correlationGuid, nameof(ProductService), nameof(DeleteProduct), 1, _userIdentity.Name);

                #endregion Logging info

                var IsDeleted = await _unitOfWork.ProductRepository.RemoveAsync(Id);
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

                _logger.ErrorInDetail(Id, correlationGuid, $"{nameof(ProductService)}_{nameof(DeleteProductCategory)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                return ex.Message;
            }
        }

        public async Task UpdateProduct(ProductDTO Product)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Product, correlationGuid, nameof(ProductService), nameof(UpdateProduct), 1, _userIdentity.Name);

                #endregion Logging info


                var Entity = _mapper.Map<ProductDTO, ProductEntity>(Product);
                await _unitOfWork.ProductRepository.UpdateProduct(Entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Product, correlationGuid, $"{nameof(ProductService)}_{nameof(UpdateProductCategory)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task<ProductDTO> GetProduct(Guid Id)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Id, correlationGuid, nameof(ProductService), nameof(GetProduct), 1, _userIdentity.Name);

                #endregion Logging info

                var Model = await _unitOfWork.ProductRepository.GetAsync(Id);
                return _mapper.Map<ProductEntity, ProductDTO>(Model);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Id, correlationGuid, $"{nameof(ProductService)}_{nameof(GetProductCategory)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task SaveProductImage(ProductImageDTO Image)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Image, correlationGuid, nameof(ProductService), nameof(SaveProductImage), 1, _userIdentity.Name);

                #endregion Logging info


                var Entity = _mapper.Map<ProductImageDTO, ProductImageEntity>(Image);
                await _unitOfWork.ProductRepository.AddProductImage(Entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Image, correlationGuid, $"{nameof(ProductService)}_{nameof(SaveProductImage)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task<ProductImageDTO> GetProductImage(Guid Id)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Id, correlationGuid, nameof(ProductService), nameof(SaveProductImage), 1, _userIdentity.Name);

                #endregion Logging info


                var entity = await _unitOfWork.ProductImageRepository.GetAsync(Id);
                return _mapper.Map<ProductImageEntity, ProductImageDTO>(entity);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Id, correlationGuid, $"{nameof(ProductService)}_{nameof(SaveProductImage)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public object GetProductImages(Guid ProductId)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(ProductId, correlationGuid, nameof(ProductService), nameof(GetProductImages), 1, _userIdentity.Name);

                #endregion Logging info

                return  _unitOfWork.ProductImageRepository.GetProductImages(ProductId);
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(ProductId, correlationGuid, $"{nameof(ProductService)}_{nameof(GetProductImages)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }

        public async Task<string> GetProductImageDirectory(Guid ProductId)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(ProductId, correlationGuid, nameof(ProductService), nameof(GetProductImageDirectory), 1, _userIdentity.Name);

                #endregion Logging info
                var SPParameters = new DynamicParameters();
                SPParameters.Add("@ProductId", ProductId);

                var Directory = await _unitOfWork.SP.SingleAsnyc<string>(StoredProcedure.GETPRODUCTIMAGEDIRECTOY, SPParameters);
                return Directory;
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(ProductId, correlationGuid, $"{nameof(ProductService)}_{nameof(GetProductImageDirectory)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                throw;
            }
        }
        #endregion
    }
}
