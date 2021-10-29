using ElBayt.Common.Core.Services;
using ElBayt.DTO.ELBaytDTO_s;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElBayt.Services.Contracts
{
    public interface IProductService
    {
        public Task AddNewProduct(ProductDTO product);

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
