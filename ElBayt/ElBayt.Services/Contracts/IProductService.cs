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
        public Task AddNewProductCategory(ProductCategoryDTO productCategory);
        public Task AddNewProductType(ProductTypeDTO productType);

        #region Departments
        public Task AddNewProductDepartment(ProductDepartmentDTO productDepartment);
        public object GetProductDepartments();
        public Task<string> DeleteProductDepartment(Guid Id);
        public Task UpdateProductDepartment(ProductDepartmentDTO ProductDepartment);
        public Task<ProductDepartmentDTO> GetProductDepartment(Guid Id);
        #endregion

    }
}
