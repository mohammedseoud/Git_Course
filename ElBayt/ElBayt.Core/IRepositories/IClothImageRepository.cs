using ElBayt.Infra.Entities;
using ElBayt.Core.GenericIRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElBayt.Core.Models;

namespace ElBayt.Core.IRepositories
{
    public interface IClothImageRepository : IGenericRepository<ClothImageModel, int>
    {
        object GetClothImages(int ClothId);
        bool DeleteByURL(string URL);
    }
}
