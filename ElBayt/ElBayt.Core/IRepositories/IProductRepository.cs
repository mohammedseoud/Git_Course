using ElBayt.Core.GenericIRepository;
using ElBayt.Common.Infra.Models;

namespace ElBayt.Core.IRepositories
{
    public interface IProductRepository : IGenericRepository<ProductModel, int>
    {
        //public Task UpdateProduct(ProductEntity product);
        //public Task AddProductImage(ProductImageEntity Image);
        //public Task<ProductEntity> GetProductByName(string Name, Guid Id);

    }
}
