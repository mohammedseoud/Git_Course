using ElBayt.Infra.Entities;
using ElBayt.Core.GenericIRepository;
using System;
using System.Threading.Tasks;
using ElBayt.Core.Models;
using System.Collections.Generic;

namespace ElBayt.Core.IRepositories
{
    public interface IClothSizeRepository : IGenericRepository<ClothSizeModel, Guid>
    {
        Task UpdateClothSize(ClothSizeModel clothSize);
        Task<ClothSizeModel> GetClothSizeByName(string Name, Guid Id);
        Task<List<ClothSizeModel>> GetClothSizes(Guid ClothId);
    }
}
