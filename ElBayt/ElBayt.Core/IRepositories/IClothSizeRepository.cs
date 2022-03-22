using ElBayt.Core.Entities;
using ElBayt.Core.GenericIRepository;
using System;
using System.Threading.Tasks;

namespace ElBayt.Core.IRepositories
{
    public interface IClothSizeRepository : IGenericRepository<ClothSizeEntity, Guid>
    {
        Task UpdateClothSize(ClothSizeEntity clothSize);
        Task<ClothSizeEntity> GetClothSizeByName(string Name, Guid Id);
    }
}
