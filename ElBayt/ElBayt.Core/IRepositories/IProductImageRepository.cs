using ElBayt.Core.GenericIRepository;
using ElBayt.Common.Infra.Models;

namespace ElBayt.Core.IRepositories
{
    public interface IProductImageRepository : IGenericRepository<ProductImageModel, int>
    {
        //object GetProductImages(Guid ProductId);
        //bool DeleteByURL(string URL);
    }
}
