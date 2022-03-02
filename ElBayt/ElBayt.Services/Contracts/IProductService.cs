using ElBayt.Common.Enums;
using ElBayt.DTO.ELBayt.DBDTOs;
using ElBayt.DTO.ELBayt.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElBayt.Services.Contracts
{
    public interface IProductService
    {
        #region Products
        public Task<NumberProductDTO> AddNewProduct(IFormCollection form, string DiskDirectory);
        public object GetProducts();
        public Task<string> DeleteProduct(Guid Id);
        public Task<NumberProductDTO> UpdateProduct(IFormCollection Form, string DiskDirectory);
        public Task<NumberProductDTO> GetProduct(Guid Id);
        public Task<ProductImageDTO> SaveProductImage(string ProductId, IFormFile file, string DiskDirectory);
        public Task<ProductImageDTO> GetProductImage(Guid Id);
        public Task<List<ProductImageDataDTO>> GetProductImages(Guid ProductId);
        public Task<string> DeleteProductImage(Guid ImageId);
        public Task<string> DeleteProductImageByURL(string URL);
        #endregion

        #region Categories
        public Task<EnumInsertingResult> AddNewProductCategory(ProductCategoryDTO productCategory);
        public object GetProductCategories();
        public Task<string> DeleteProductCategory(Guid Id);
        public Task<EnumUpdatingResult> UpdateProductCategory(ProductCategoryDTO ProductCategory, string DiskDirectory);
        public Task<ProductCategoryDTO> GetProductCategory(Guid Id);
        #endregion

        #region Types
        public Task<EnumInsertingResult> AddNewProductType(ProductTypeDTO productType);
        public object GetProductTypes();
        public Task<string> DeleteProductType(Guid Id);
        public Task<EnumUpdatingResult> UpdateProductType(ProductTypeDTO ProductType, string DiskDirectory);
        public Task<ProductTypeDTO> GetProductType(Guid Id);
        #endregion

        #region Departments
        public Task<EnumInsertingResult> AddNewProductDepartment(ProductDepartmentDTO productDepartment);
        public object GetProductDepartments();
        public Task<string> DeleteProductDepartment(Guid Id);
        public Task<EnumUpdatingResult> UpdateProductDepartment(ProductDepartmentDTO ProductDepartment);
        public Task<ProductDepartmentDTO> GetProductDepartment(Guid Id);
        #endregion

     }
}
