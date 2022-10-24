using ElBayt.Common.Infra.Common;
using ElBayt.Common.Core.Mapping;
using ElBayt.Core.IRepositories;
using ElBayt.Infra.Context;
using ElBayt.Core.Models;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ElBayt.Infra.Repositories
{
    public class ClothBrandRepository : GenericRepository<ClothBrandModel, int>, IClothBrandRepository
    {
        private readonly ElBaytContext _dbContext;
        private readonly ITypeMapper _mapper;
        

        public ClothBrandRepository(ElBaytContext dbContext, ITypeMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }

        public async Task<ClothBrandModel> GetClothBrandByName(string Name, int Id)
        {
            var brand = await _dbContext.ClothBrands
               .Where(c => c.Name.Trim() == Name && c.Id != Id).
               AsNoTracking().FirstOrDefaultAsync();
            return brand;
        }

        public async Task UpdateClothBrand(ClothBrandModel clothBrand)
        {
            var ClothBrand = await _dbContext.ClothBrands.FindAsync(clothBrand.Id);
            if (ClothBrand != null)
            {
                ClothBrand.Name = clothBrand.Name;
                ClothBrand.BrandPic = clothBrand.BrandPic;
            }
        }
    }
}
