using ElBayt.Core.Entities;
using ElBayt.Core.GenericIRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElBayt.Core.IRepositories
{
    public interface IClothImageRepository : IGenericRepository<ClothImageEntity, Guid>
    {
        object GetClothImages(Guid ClothId);
        bool DeleteByURL(string URL);
    }
}
