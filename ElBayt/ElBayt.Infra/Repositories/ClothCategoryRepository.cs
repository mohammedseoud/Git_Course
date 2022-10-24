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
    public class ClothCategoryRepository : GenericRepository<ClothCategoryModel, int>, IClothCategoryRepository
    {
        private readonly ElBaytContext _dbContext;
        private readonly ITypeMapper _mapper;
        

        public ClothCategoryRepository(ElBaytContext dbContext, ITypeMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }

        public async Task<ClothCategoryModel> GetClothCategoryByName(string Name, int Id)
        {
            var category = await _dbContext.ClothCategories
               .Where(c => c.Name.Trim() == Name && c.Id != Id).
               AsNoTracking().FirstOrDefaultAsync();
            return category;
        }

        public async Task UpdateClothCategory(ClothCategoryModel clothCategory)
        {
            var Category = await _dbContext.ClothCategories.FindAsync(clothCategory.Id);
            if (Category != null)
            {
                Category.Name = clothCategory.Name;
                Category.ClothTypeId = clothCategory.ClothTypeId;
            }
        }
    }
}
