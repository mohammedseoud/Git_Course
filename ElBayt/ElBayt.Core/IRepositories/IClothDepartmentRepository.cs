using ElBayt.Core.Entities;
using ElBayt.Core.GenericIRepository;
using System;
using System.Threading.Tasks;

namespace ElBayt.Core.IRepositories
{
    public interface IClothDepartmentRepository : IGenericRepository<ClothDepartmentEntity, Guid>
    {
        Task UpdateClothDepartment(ClothDepartmentEntity clothDepartment);
        Task<ClothDepartmentEntity> GetClothDepartmentByName(string Name, Guid Id);
    }
}
