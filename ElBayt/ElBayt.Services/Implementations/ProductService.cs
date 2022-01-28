using Dapper;
using ElBayt.Common.Common;
using ElBayt.Common.Core.Logging;
using ElBayt.Common.Core.Mapping;
using ElBayt.Common.Core.SecurityModels;
using ElBayt.Common.Enums;
using ElBayt.Common.Utilities;
using ElBayt.Core.Entities;
using ElBayt.Core.IUnitOfWork;
using ElBayt.DTO.ELBayt.DBDTOs;
using ElBayt.DTO.ELBayt.DTOs;
using ElBayt.Infra.SPs;
using ElBayt.Services.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public async Task<EnumInsertingResult> AddNewProductCategory(ProductCategoryDTO productCategory)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(productCategory, correlationGuid, nameof(ProductService), nameof(AddNewProduct), 1, _userIdentity.Name);

                #endregion Logging info

                var Category = await _unitOfWork.ProductCategoryRepository.GetProductCategoryByName(productCategory.Name.Trim(), productCategory.Id);
                if (Category == null)
                {
                    var Entity = _mapper.Map<ProductCategoryDTO, ProductCategoryEntity>(productCategory);
                    Entity.Id = Guid.NewGuid();
                    await _unitOfWork.ProductCategoryRepository.AddAsync(Entity);
                    await _unitOfWork.SaveAsync();
                    return EnumInsertingResult.Successed;
                }
                return EnumInsertingResult.Failed;
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

        public async Task<EnumUpdatingResult> UpdateProductCategory(ProductCategoryDTO ProductCategory)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(ProductCategory, correlationGuid, nameof(ProductService), nameof(UpdateProductCategory), 1, _userIdentity.Name);

                #endregion Logging info

                var Category = await _unitOfWork.ProductCategoryRepository.GetProductCategoryByName(ProductCategory.Name.Trim(), ProductCategory.Id);

                if (Category == null)
                {
                    var Entity = _mapper.Map<ProductCategoryDTO, ProductCategoryEntity>(ProductCategory);
                    await _unitOfWork.ProductCategoryRepository.UpdateProductCategory(Entity);
                    await _unitOfWork.SaveAsync();

                    return EnumUpdatingResult.Successed;
                }

                return EnumUpdatingResult.Failed;
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

        public async Task<EnumInsertingResult> AddNewProductType(ProductTypeDTO productType)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(productType, correlationGuid, nameof(ProductService), nameof(AddNewProduct), 1, _userIdentity.Name);

                #endregion Logging info

                var Type = await _unitOfWork.ProductTypeRepository.GetProductTypeByName(productType.Name.Trim(), productType.Id);
                if (Type == null)
                {
                    var Entity = _mapper.Map<ProductTypeDTO, ProductTypeEntity>(productType);
                    Entity.Id = Guid.NewGuid();
                    await _unitOfWork.ProductTypeRepository.AddAsync(Entity);
                    await _unitOfWork.SaveAsync();
                    return EnumInsertingResult.Successed;
                }
                return EnumInsertingResult.Failed;
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

        public async Task<EnumUpdatingResult> UpdateProductType(ProductTypeDTO ProductType)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(ProductType, correlationGuid, nameof(ProductService), nameof(UpdateProductType), 1, _userIdentity.Name);

                #endregion Logging info

                var Type = await _unitOfWork.ProductTypeRepository.GetProductTypeByName(ProductType.Name.Trim(), ProductType.Id);
                if (Type == null)
                {
                    var Entity = _mapper.Map<ProductTypeDTO, ProductTypeEntity>(ProductType);
                    await _unitOfWork.ProductTypeRepository.UpdateProductType(Entity);
                    await _unitOfWork.SaveAsync();
                    return EnumUpdatingResult.Successed;
                }

                return EnumUpdatingResult.Failed;
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
        public async Task<EnumInsertingResult> AddNewProductDepartment(ProductDepartmentDTO productDepartment)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(productDepartment, correlationGuid, nameof(ProductService), nameof(AddNewProduct), 1, _userIdentity.Name);

                #endregion Logging info
               
                var Department = await _unitOfWork.ProductDepartmentRepository.GetProductDepartmentByName(productDepartment.Name.Trim(), productDepartment.Id);
                if (Department == null)
                {
                    var Entity = _mapper.Map<ProductDepartmentDTO, ProductDepartmentEntity>(productDepartment);
                    Entity.Id = Guid.NewGuid();
                    await _unitOfWork.ProductDepartmentRepository.AddAsync(Entity);
                    await _unitOfWork.SaveAsync();
                    return EnumInsertingResult.Successed ;
                }

                return EnumInsertingResult.Failed;
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

        public async Task<EnumUpdatingResult> UpdateProductDepartment(ProductDepartmentDTO ProductDepartment)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(ProductDepartment, correlationGuid, nameof(ProductService), nameof(UpdateProductDepartment), 1, _userIdentity.Name);

                #endregion Logging info

                var Department = await _unitOfWork.ProductDepartmentRepository.GetProductDepartmentByName(ProductDepartment.Name.Trim(), ProductDepartment.Id);
                if (Department == null)
                {
                    var Entity = _mapper.Map<ProductDepartmentDTO, ProductDepartmentEntity>(ProductDepartment);
                    await _unitOfWork.ProductDepartmentRepository.UpdateProductDepartment(Entity);
                    await _unitOfWork.SaveAsync();
                    return EnumUpdatingResult.Successed;
                }
                return EnumUpdatingResult.Failed;
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

        public async Task<ProductDTO> AddNewProduct(IFormCollection Form, string DiskDirectory)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info
                _logger.InfoInDetail(Form, correlationGuid, nameof(ProductService), nameof(AddNewProduct), 1, _userIdentity.Name);
                #endregion Logging info

                var Product = await _unitOfWork.ProductRepository.GetProductByName(Form["Name"].ToString().Trim(), Guid.NewGuid());
                if (Product == null)
                {

                    var identityName = _userIdentity?.Name ?? "Unknown";

                    decimal PriceAfterDiscount ;
                    var product = new ProductDTO
                    {
                        Id = Guid.NewGuid(),
                        CreatedBy = identityName,
                        CreatedDate = DateTime.Now,
                        ModifiedBy = identityName,
                        ModifiedDate = DateTime.Now,
                        Price = Form["Price"].ToString(),
                        PriceAfterDiscount = decimal.TryParse(Form["PriceAfterDiscount"].ToString(), out PriceAfterDiscount) ? PriceAfterDiscount.ToString() : null,
                        Name = Form["Name"].ToString(),
                        Description = Form["Description"].ToString(),
                        ProductCategoryId = Guid.Parse(Form["ProductCategoryId"].ToString())
                    };
                    var ProductEntity = _mapper.Map<ProductDTO, UTDProductDTO>(product);
                    var Products = new List<UTDProductDTO>
                    {
                       ProductEntity
                    };
                    var image1 = new ProductImageDTO
                    {
                        Id = Guid.NewGuid(),
                        ProductId = product.Id,
                        CreatedBy = identityName,
                        CreatedDate = DateTime.Now,
                        ModifiedBy = identityName,
                        ModifiedDate = DateTime.Now
                    };



                    var Image1Entity = _mapper.Map<ProductImageDTO, UTDProductImageDTO>(image1);

                    var Images1 = new List<UTDProductImageDTO>
                {
                    Image1Entity
                };
                    var Images2 = new List<UTDProductImageDTO>();
                    if (Form.Files.Count > 1)
                    {
                        var image2 = new ProductImageDTO
                        {
                            Id = Guid.NewGuid(),
                            ProductId = product.Id,
                            CreatedBy = identityName,
                            CreatedDate = DateTime.Now,
                            ModifiedBy = identityName,
                            ModifiedDate = DateTime.Now
                        };
                        var Image2Entity = _mapper.Map<ProductImageDTO, UTDProductImageDTO>(image2);

                        Images2 = new List<UTDProductImageDTO>
                {
                    Image2Entity
                };
                    }
                    var image1table = ObjectDatatableConverter.ToDataTable(Images1);
                    var image2table = ObjectDatatableConverter.ToDataTable(Images2);
                    var Producttable = ObjectDatatableConverter.ToDataTable(Products);
                    var SPParameters = new DynamicParameters();
                    SPParameters.Add("@UDTProduct", Producttable.AsTableValuedParameter(UDT.UDTPRODUCT));
                    SPParameters.Add("@UDTProductImage", image1table.AsTableValuedParameter(UDT.UDTPRODUCTIMAGE));
                    SPParameters.Add("@UDTProductImage2", image2table.AsTableValuedParameter(UDT.UDTPRODUCTIMAGE));
                    SPParameters.Add("@Extension1", Path.GetExtension(Form.Files[0].FileName));
                    if (Form.Files.Count > 1)
                        SPParameters.Add("@Extension2", Path.GetExtension(Form.Files[1].FileName));
                    else
                        SPParameters.Add("@Extension2", General.NOEXTENSION);

                    SPParameters.Add("@DiskDirectory", DiskDirectory);



                    var List = await _unitOfWork.SP.ListAsnyc<ProductDTO>(StoredProcedure.ADDPRODUCT, SPParameters);
                    return List.FirstOrDefault();
                }
                return null;
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Form, correlationGuid, $"{nameof(ProductService)}_{nameof(AddNewProduct)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

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

                _logger.ErrorInDetail(Id, correlationGuid, $"{nameof(ProductService)}_{nameof(DeleteProduct)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                return ex.Message;
            }
        }

        public async Task<ProductDTO> UpdateProduct(IFormCollection Form)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(Form, correlationGuid, nameof(ProductService), nameof(UpdateProduct), 1, _userIdentity.Name);

                #endregion Logging info



                var product = await _unitOfWork.ProductRepository.GetProductByName(Form["Name"].ToString().Trim(), Guid.Parse(Form["Id"].ToString()));

                if (product == null)
                {
                    var identityName = _userIdentity?.Name ?? "Unknown";

                    var Newproduct = new ProductDTO
                    {
                        Id = Guid.Parse(Form["Id"].ToString()),
                        ModifiedBy = identityName,
                        ModifiedDate = DateTime.Now,
                        Price = Form["Price"].ToString(),
                        PriceAfterDiscount = Form["PriceAfterDiscount"].ToString(),
                        Name = Form["Name"].ToString(),
                        Description = Form["Description"].ToString(),
                        ProductCategoryId = Guid.Parse(Form["ProductCategoryId"].ToString()),
                    };

                    if (!string.IsNullOrEmpty(Form["Img1"]))
                    {
                        var url = product.ProductImageURL1.Split(".")[0] + Path.GetExtension(Form.Files[0].FileName);
                        Newproduct.ProductImageURL1 = url;
                    }
                    if (!string.IsNullOrEmpty(Form["Img2"]))
                    {
                        var i = Form.Files.Count == 1 ? 0 : 1;
                        var url = product.ProductImageURL2.Split(".")[0] + Path.GetExtension(Form.Files[i].FileName);
                        Newproduct.ProductImageURL2 = url;
                    }


                    var UTDProduct = _mapper.Map<ProductDTO, UTDProductDTO>(Newproduct);
                    var Products = new List<UTDProductDTO>
                    {
                       UTDProduct
                    };

                    var Producttable = ObjectDatatableConverter.ToDataTable(Products);
                    var SPParameters = new DynamicParameters();
                    SPParameters.Add("@UDTProduct", Producttable.AsTableValuedParameter(UDT.UDTPRODUCT));
                    if (!string.IsNullOrEmpty(Form["Img2"]))
                        SPParameters.Add("@Img2Id", Guid.NewGuid());
                    else
                        SPParameters.Add("@Img2Id", null);

              


                    var List = await _unitOfWork.SP.ListAsnyc<ProductDTO>(StoredProcedure.UPDATEPRODUCT, SPParameters);
                    return List.FirstOrDefault();
                }
                return null;
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(Form, correlationGuid, $"{nameof(ProductService)}_{nameof(UpdateProduct)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

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

        public async Task<ProductImageDTO> SaveProductImage(string ProductId, IFormFile file
            ,string DiskDirectory)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(file, correlationGuid, nameof(ProductService), nameof(SaveProductImage), 1, _userIdentity.Name);

                #endregion Logging info

                var identityName = _userIdentity?.Name ?? "Unknown";

                var image = new ProductImageDTO
                {
                    Id = Guid.NewGuid(),
                    ProductId = Guid.Parse(ProductId),
                    CreatedBy = identityName,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = identityName,
                    ModifiedDate = DateTime.Now
                };


                var Entity = _mapper.Map<ProductImageDTO, UTDProductImageDTO>(image);
                var Images = new List<UTDProductImageDTO>
                {
                    Entity
                };
                var table = ObjectDatatableConverter.ToDataTable(Images);

                var SPParameters = new DynamicParameters();
                SPParameters.Add("@UDTProductImage", table.AsTableValuedParameter(UDT.UDTPRODUCTIMAGE));
                SPParameters.Add("@Extension", Path.GetExtension(file.FileName));
                SPParameters.Add("@DiskDirectory", DiskDirectory);
                
                var Imglist = await _unitOfWork.SP.ListAsnyc<UTDProductImageDTO>(StoredProcedure.ADDPRODUCTIMAGE, SPParameters);
                var imageDTO = _mapper.Map<UTDProductImageDTO,ProductImageDTO>(Imglist.FirstOrDefault());
                return imageDTO;


            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(file, correlationGuid, $"{nameof(ProductService)}_{nameof(SaveProductImage)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

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

        public async Task<string> DeleteProductImage(Guid ImageId)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(ImageId, correlationGuid, nameof(ProductService), nameof(DeleteProductImage), 1, _userIdentity.Name);

                #endregion Logging info

                var IsDeleted = await _unitOfWork.ProductImageRepository.RemoveAsync(ImageId);
                if (IsDeleted)
                {
                    var res = await _unitOfWork.SaveAsync();
                    return "true";

                }
                return "This Image Not Exist";
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(ImageId, correlationGuid, $"{nameof(ProductService)}_{nameof(DeleteProductImage)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                return ex.Message;
            }
        }

        public async Task<string> DeleteProductImageByURL(string URL)
        {
            var correlationGuid = Guid.NewGuid();

            try
            {
                #region Logging info

                _logger.InfoInDetail(URL, correlationGuid, nameof(ProductService), nameof(DeleteProductImageByURL), 1, _userIdentity.Name);

                #endregion Logging info

                var IsDeleted =  _unitOfWork.ProductImageRepository.DeleteByURL(URL);
                if (IsDeleted)
                {
                    var res = await _unitOfWork.SaveAsync();
                    return "true";

                }
                return "This Image Not Exist";
            }
            catch (Exception ex)
            {
                #region Logging info

                _logger.ErrorInDetail(URL, correlationGuid, $"{nameof(ProductService)}_{nameof(DeleteProductImage)}_{nameof(Exception)}", ex, 1, _userIdentity.Name);

                #endregion Logging info

                return ex.Message;
            }

        }
        #endregion
    }
}
