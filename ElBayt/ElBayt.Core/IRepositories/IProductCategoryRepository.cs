using ElBayt.Core.GenericIRepository;
using ElBayt.Common.Infra.Models;

namespace ElBayt.Core.IRepositories
{
    public interface IProductCategoryRepository : IGenericRepository<ProductCategoryModel, int>
    {
        //Task UpdateProductCategory(ProductCategoryEntity productCategory);
        //Task<ProductCategoryEntity> GetProductCategoryByName(string Name, Guid Id);
    }
}
