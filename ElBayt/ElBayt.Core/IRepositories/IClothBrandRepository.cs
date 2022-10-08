﻿using ElBayt.Infra.Entities;
using ElBayt.Core.GenericIRepository;
using System;
using System.Threading.Tasks;
using ElBayt.Core.Models;

namespace ElBayt.Core.IRepositories
{
    public interface IClothBrandRepository : IGenericRepository<ClothBrandModel, Guid>
    {
        Task UpdateClothBrand(ClothBrandModel clothBrand);
        Task<ClothBrandModel> GetClothBrandByName(string Name, Guid Id);
    }
}
