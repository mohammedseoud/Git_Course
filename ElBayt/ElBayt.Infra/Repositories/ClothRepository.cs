using ElBayt.Common.Infra.Common;
using ElBayt.Common.Core.Mapping;
using ElBayt.Infra.Entities;
using ElBayt.Core.IRepositories;
using ElBayt.Infra.Context;
using ElBayt.Core.Models;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ElBayt.Infra.Repositories
{
    public class ClothRepository: GenericRepository<ClothModel, Guid>, IClothRepository
    {
        private readonly ElBaytContext _dbContext;
        private readonly ITypeMapper _mapper;
        

        public ClothRepository(ElBaytContext dbContext, ITypeMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }

        public async Task AddClothImage(ClothImageModel Image)
         => await _dbContext.ClothImages.AddAsync(Image);
       
        public async Task UpdateCloth(ClothModel cloth)
        {
            var _Cloth = await _dbContext.Clothes.FindAsync(cloth.Id);
            if (_Cloth != null)
            {
                _Cloth.Name = cloth.Name;
                _Cloth.Description = cloth.Description;
                _Cloth.ClothCategoryId = cloth.ClothCategoryId;
                _Cloth.ProductImageURL1 = cloth.ProductImageURL1;
                _Cloth.ProductImageURL2= cloth.ProductImageURL2;
            }          
        }

        public async Task<ClothModel> GetClothByName(string Name, Guid Id)
        {
            var cloth = await _dbContext.Clothes
                .Where(c => c.Name.Trim() == Name && c.Id != Id).
                AsNoTracking().FirstOrDefaultAsync();
            return cloth;
        }

    }
}
