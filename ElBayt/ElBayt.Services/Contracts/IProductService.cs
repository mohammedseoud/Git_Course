using ElBayt.DTO.ELBayt.DBDTOs;
using System;
using System.Threading.Tasks;

namespace ElBayt.Services.Contracts
{
    public interface IProductService
    {
        #region Products
        public Task AddNewProduct(ProductDTO product);
        public object GetProducts();
        public Task<string> DeleteProduct(Guid Id);
        public Task UpdateProduct(ProductDTO Product);
        public Task<ProductDTO> GetProduct(Guid Id);
        public Task SaveProductImage(ProductImageDTO Image);
        public Task<ProductImageDTO> GetProductImage(Guid Id);
        public object GetProductImages(Guid ProductId);
        public Task<string> GetProductImageDirectory(Guid ProductId);
        public Task<string> DeleteProductImage(Guid ImageId);
        #endregion

        #region Categories
        public Task AddNewProductCategory(ProductCategoryDTO productCategory);
        public object GetProductCategories();
        public Task<string> DeleteProductCategory(Guid Id);
        public Task UpdateProductCategory(ProductCategoryDTO ProductCategory);
        public Task<ProductCategoryDTO> GetProductCategory(Guid Id);
        #endregion

        #region Types
        public Task AddNewProductType(ProductTypeDTO productType);
        public object GetProductTypes();
        public Task<string> DeleteProductType(Guid Id);
        public Task UpdateProductType(ProductTypeDTO ProductType);
        public Task<ProductTypeDTO> GetProductType(Guid Id);
        #endregion

        #region Departments
        public Task AddNewProductDepartment(ProductDepartmentDTO productDepartment);
        public object GetProductDepartments();
        public Task<string> DeleteProductDepartment(Guid Id);
        public Task UpdateProductDepartment(ProductDepartmentDTO ProductDepartment);
        public Task<ProductDepartmentDTO> GetProductDepartment(Guid Id);
        #endregion

     }
}
