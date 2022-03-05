using ElBayt.Common.Common;
using ElBayt.Core.Entities;
using ElBayt.Core.GenericIRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ElBayt.Core.IRepositories
{
    public interface IClothTypeRepository : IGenericRepository<ClothTypeEntity, Guid>
    {
        Task UpdateClothType(ClothTypeEntity clothesTypes);
        Task<ClothTypeEntity> GetClothTypeByName(string Name, Guid Id);
    }
}
