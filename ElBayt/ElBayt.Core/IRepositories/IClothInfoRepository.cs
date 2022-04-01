using ElBayt.Core.Entities;
using ElBayt.Core.GenericIRepository;
using System;
using System.Threading.Tasks;

namespace ElBayt.Core.IRepositories
{
    public interface IClothInfoRepository : IGenericRepository<ClothInfoEntity, Guid>
    {
        public Task UpdateInfo(ClothInfoEntity cloth);
        public Task<object> GetClothInfo(Guid ClothId);
        public Task<object> GetClothInfo(Guid SizeId, Guid? ColorId, Guid? BrandId);
    }
}
