using ElBayt.Core.GenericIRepository;
using ElBayt.Core.Models;

namespace ElBayt.Core.IRepositories
{
    public interface IClothImageRepository : IGenericRepository<ClothImageModel, int>
    {
        object GetClothImages(int ClothId);
        bool DeleteByURL(string URL);
    }
}
