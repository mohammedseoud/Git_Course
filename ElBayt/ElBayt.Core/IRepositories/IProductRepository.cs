using ElBayt.Core.Entities;
using ElBayt.Core.GenericIRepository;
using System;
using System.Threading.Tasks;

namespace ElBayt.Core.IRepositories
{
    public interface IProductRepository : IGenericRepository<ProductEntity, Guid>
    {
        public Task UpdateProduct(ProductEntity product);
        public Task AddProductImage(ProductImageEntity Image);
    }
}
