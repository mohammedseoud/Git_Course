using ElBayt.Core.Entities;
using ElBayt.Core.GenericIRepository;
using System;

namespace ElBayt.Core.IRepositories
{
    public interface IProductRepository : IGenericRepository<ProductEntity, Guid>
    {

    }
}
