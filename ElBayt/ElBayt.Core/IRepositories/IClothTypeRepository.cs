﻿using ElBayt.Core.GenericIRepository;
using System.Threading.Tasks;
using ElBayt.Core.Models;

namespace ElBayt.Core.IRepositories
{
    public interface IClothTypeRepository : IGenericRepository<ClothTypeModel, int>
    {
        Task UpdateClothType(ClothTypeModel clothesTypes);
        Task<ClothTypeModel> GetClothTypeByName(string Name, int Id);
    }
}
