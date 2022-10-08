using ElBayt.Infra.Entities;
using ElBayt.Core.GenericIRepository;
using System;
using System.Threading.Tasks;
using ElBayt.Core.Models;

namespace ElBayt.Core.IRepositories
{
    public interface IClothCategoryRepository : IGenericRepository<ClothCategoryModel, Guid>
    {
        Task UpdateClothCategory(ClothCategoryModel clothCategory);
        Task<ClothCategoryModel> GetClothCategoryByName(string Name, Guid Id);
    }
}
