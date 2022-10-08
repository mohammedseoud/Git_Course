using AutoMapper;
using ElBayt.Common.Infra.Common;
using ElBayt.Common.Infra.Mapping;
using ElBayt.Common.Core.Mapping;
using ElBayt.Infra.Entities;
using ElBayt.Core.IRepositories;
using ElBayt.Infra.Context;
using ElBayt.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ElBayt.Infra.Repositories
{
    public class ClothCategoryRepository : GenericRepository<ClothCategoryModel, Guid>, IClothCategoryRepository
    {
        private readonly ElBaytContext _dbContext;
        private readonly ITypeMapper _mapper;
        

        public ClothCategoryRepository(ElBaytContext dbContext, ITypeMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }

        public async Task<ClothCategoryModel> GetClothCategoryByName(string Name, Guid Id)
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
