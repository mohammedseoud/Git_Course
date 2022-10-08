using ElBayt.Infra.Entities;
using ElBayt.Core.GenericIRepository;
using System;
using System.Threading.Tasks;
using ElBayt.Common.Infra.Models;

namespace ElBayt.Core.IRepositories
{
    public interface IProductTypeRepository : IGenericRepository<ProductTypeModel, Guid>
    {
        //Task UpdateProductType(ProductTypeEntity productType);
        //Task<ProductTypeEntity> GetProductTypeByName(string Name, Guid Id);
    }
}
