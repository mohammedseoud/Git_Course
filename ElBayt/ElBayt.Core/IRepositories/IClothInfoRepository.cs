using ElBayt.Core.GenericIRepository;
using System.Threading.Tasks;
using ElBayt.Core.Models;
using System.Collections.Generic;

namespace ElBayt.Core.IRepositories
{
    public interface IClothInfoRepository : IGenericRepository<ClothInfoModel, int>
    {
        public Task UpdateInfo(ClothInfoModel cloth);
        public Task<List<ClothInfoModel>> GetClothInfo(int ClothId);
        public Task<List<ClothInfoModel>> GetClothInfo(int SizeId, int? ColorId, int? BrandId, int ClothId);
    }
}
