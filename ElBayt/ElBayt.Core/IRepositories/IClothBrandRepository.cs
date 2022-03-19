using ElBayt.Core.Entities;
using ElBayt.Core.GenericIRepository;
using System;
using System.Threading.Tasks;

namespace ElBayt.Core.IRepositories
{
    public interface IClothBrandRepository : IGenericRepository<ClothBrandEntity, Guid>
    {
        Task UpdateClothBrand(ClothBrandEntity clothBrand);
        Task<ClothBrandEntity> GetClothBrandByName(string Name, Guid Id);
    }
}
