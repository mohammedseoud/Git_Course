using AutoMapper;
using ElBayt.Common.Infra.Common;
using ElBayt.Common.Infra.Mapping;
using ElBayt.Common.Core.Mapping;
using ElBayt.Core.Entities;
using ElBayt.Core.IRepositories;
using ElBayt.Infra.Context;
using ElBayt.Infra.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ElBayt.Infra.Repositories
{
    public class ClothCategoryRepository : GenericRepository<ClothCategoryEntity, ClothCategoryModel, Guid>, IClothCategoryRepository
    {
        private readonly ElBaytContext _dbContext;
        private readonly ITypeMapper _mapper;
        

        public ClothCategoryRepository(ElBaytContext dbContext, ITypeMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }

        public async Task<ClothCategoryEntity> GetClothCategoryByName(string Name, Guid Id)
        {
            var category = await _dbContext.ClothCategories
               .Where(c => c.Name.Trim() == Name && c.Id != Id).
               AsNoTracking().FirstOrDefaultAsync();
            return _mapper.Map<ClothCategoryModel, ClothCategoryEntity>(category);
        }

        public async Task UpdateClothCategory(ClothCategoryEntity clothCategory)
        {
            var Category = await _dbContext.ClothCategories.FindAsync(clothCategory.Id);
            Category.Name = clothCategory.Name;
            Category.ClothTypeId = clothCategory.ClothTypeId;
        }
    }
}
