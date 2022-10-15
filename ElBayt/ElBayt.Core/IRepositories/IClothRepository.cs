using ElBayt.Infra.Entities;
using ElBayt.Core.GenericIRepository;
using System;
using System.Threading.Tasks;
using ElBayt.Core.Models;

namespace ElBayt.Core.IRepositories
{
    public interface IClothRepository : IGenericRepository<ClothModel, int>
    {
        public Task UpdateCloth(ClothModel cloth);
        public Task AddClothImage(ClothImageModel Image);
        public Task<ClothModel> GetClothByName(string Name, int Id);

    }
}
