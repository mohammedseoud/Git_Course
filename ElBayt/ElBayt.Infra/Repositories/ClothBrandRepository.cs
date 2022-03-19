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
using ElBayt.Common.Infra.Models;

namespace ElBayt.Infra.Repositories
{
    public class ClothBrandRepository : GenericRepository<ClothBrandEntity, ClothBrandModel, Guid>, IClothBrandRepository
    {
        private readonly ElBaytContext _dbContext;
        private readonly ITypeMapper _mapper;
        

        public ClothBrandRepository(ElBaytContext dbContext, ITypeMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }

        public async Task<ClothBrandEntity> GetClothBrandByName(string Name, Guid Id)
        {
            var brand = await _dbContext.ClothBrands
               .Where(c => c.Name.Trim() == Name && c.Id != Id).
               AsNoTracking().FirstOrDefaultAsync();
            return _mapper.Map<ClothBrandModel, ClothBrandEntity>(brand);
        }

        public async Task UpdateClothBrand(ClothBrandEntity clothBrand)
        {
            var ClothBrand = await _dbContext.ClothBrands.FindAsync(clothBrand.Id);
            ClothBrand.Name = clothBrand.Name;
            ClothBrand.BrandPic = clothBrand.BrandPic;
        }
    }
}
