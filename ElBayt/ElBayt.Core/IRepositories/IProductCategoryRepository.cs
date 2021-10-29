using ElBayt.Core.Entities;
using ElBayt.Core.GenericIRepository;
using System;
using System.Threading.Tasks;

namespace ElBayt.Core.IRepositories
{
    public interface IProductCategoryRepository : IGenericRepository<ProductCategoryEntity, Guid>
    {
        Task UpdateProductCategory(ProductCategoryEntity productCategory);
    }
}
