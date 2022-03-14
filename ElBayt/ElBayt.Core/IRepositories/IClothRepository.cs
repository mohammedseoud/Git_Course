using ElBayt.Core.Entities;
using ElBayt.Core.GenericIRepository;
using System;
using System.Threading.Tasks;

namespace ElBayt.Core.IRepositories
{
    public interface IClothRepository : IGenericRepository<ClothEntity, Guid>
    {
        public Task UpdateCloth(ClothEntity cloth);
        public Task AddClothImage(ClothImageEntity Image);
        public Task<ClothEntity> GetClothByName(string Name, Guid Id);

    }
}
