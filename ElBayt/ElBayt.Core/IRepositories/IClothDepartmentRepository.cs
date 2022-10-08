﻿using ElBayt.Infra.Entities;
using ElBayt.Core.GenericIRepository;
using System;
using System.Threading.Tasks;
using ElBayt.Core.Models;

namespace ElBayt.Core.IRepositories
{
    public interface IClothDepartmentRepository : IGenericRepository<ClothDepartmentModel, Guid>
    {
        Task UpdateClothDepartment(ClothDepartmentModel clothDepartment);
        Task<ClothDepartmentModel> GetClothDepartmentByName(string Name, Guid Id);
    }
}
