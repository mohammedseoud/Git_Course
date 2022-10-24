using ElBayt.Core.GenericIRepository;
using ElBayt.Common.Infra.Models;

namespace ElBayt.Core.IRepositories
{
    public interface IProductTypeRepository : IGenericRepository<ProductTypeModel, int>
    {
        //Task UpdateProductType(ProductTypeEntity productType);
        //Task<ProductTypeEntity> GetProductTypeByName(string Name, Guid Id);
    }
}
