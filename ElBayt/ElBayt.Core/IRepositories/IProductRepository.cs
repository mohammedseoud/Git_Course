using ElBayt.Common.Entities;
using ElBayt.Infra.Entities;
using ElBayt.Core.GenericIRepository;
using System;
using System.Threading.Tasks;
using ElBayt.Common.Infra.Models;

namespace ElBayt.Core.IRepositories
{
    public interface IProductRepository : IGenericRepository<ProductModel, Guid>
    {
        //public Task UpdateProduct(ProductEntity product);
        //public Task AddProductImage(ProductImageEntity Image);
        //public Task<ProductEntity> GetProductByName(string Name, Guid Id);

    }
}
