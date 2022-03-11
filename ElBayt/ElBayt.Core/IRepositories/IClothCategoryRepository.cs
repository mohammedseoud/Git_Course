using ElBayt.Core.Entities;
using ElBayt.Core.GenericIRepository;
using System;
using System.Threading.Tasks;

namespace ElBayt.Core.IRepositories
{
    public interface IClothCategoryRepository : IGenericRepository<ClothCategoryEntity, Guid>
    {
        Task UpdateClothCategory(ClothCategoryEntity clothCategory);
        Task<ClothCategoryEntity> GetClothCategoryByName(string Name, Guid Id);
    }
}
