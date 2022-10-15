using ElBayt.Common.Entities;
using ElBayt.Infra.Entities;
using ElBayt.Core.GenericIRepository;
using System;
using System.Threading.Tasks;
using ElBayt.Common.Infra.Models;

namespace ElBayt.Core.IRepositories
{
    public interface IProductCategoryRepository : IGenericRepository<ProductCategoryModel, int>
    {
        //Task UpdateProductCategory(ProductCategoryEntity productCategory);
        //Task<ProductCategoryEntity> GetProductCategoryByName(string Name, Guid Id);
    }
}
