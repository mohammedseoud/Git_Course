using ElBayt.Infra.Entities;
using ElBayt.Core.GenericIRepository;
using System;
using System.Threading.Tasks;
using ElBayt.Core.Models;
using System.Collections.Generic;

namespace ElBayt.Core.IRepositories
{
    public interface IClothInfoRepository : IGenericRepository<ClothInfoModel, Guid>
    {
        public Task UpdateInfo(ClothInfoModel cloth);
        public Task<List<ClothInfoModel>> GetClothInfo(Guid ClothId);
        public Task<List<ClothInfoModel>> GetClothInfo(Guid SizeId, Guid? ColorId, Guid? BrandId);
    }
}
