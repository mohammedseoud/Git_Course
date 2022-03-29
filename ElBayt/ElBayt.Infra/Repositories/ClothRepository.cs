using ElBayt.Common.Infra.Common;
using ElBayt.Common.Core.Mapping;
using ElBayt.Core.Entities;
using ElBayt.Core.IRepositories;
using ElBayt.Infra.Context;
using ElBayt.Infra.Models;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ElBayt.Infra.Repositories
{
    public class ClothRepository: GenericRepository<ClothEntity,ClothModel, Guid>, IClothRepository
    {
        private readonly ElBaytContext _dbContext;
        private readonly ITypeMapper _mapper;
        

        public ClothRepository(ElBaytContext dbContext, ITypeMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }

        public async Task AddClothImage(ClothImageEntity Image)
        {
            var model = _mapper.Map<ClothImageEntity, ClothImageModel>(Image);
            await _dbContext.ClothImages.AddAsync(model);
        }

        public async Task UpdateCloth(ClothEntity cloth)
        {
            var _Cloth = await _dbContext.Clothes.FindAsync(cloth.Id);
            _Cloth.Name = cloth.Name;
            _Cloth.Description = cloth.Description;
        }

        public async Task<ClothEntity> GetClothByName(string Name, Guid Id)
        {
            var cloth = await _dbContext.Clothes
                .Where(c => c.Name.Trim() == Name && c.Id != Id).
                AsNoTracking().FirstOrDefaultAsync();
            return _mapper.Map<ClothModel, ClothEntity>(cloth);
        }

    }
}
