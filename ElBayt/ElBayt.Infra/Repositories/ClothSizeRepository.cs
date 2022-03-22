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
    public class ClothSizeRepository : GenericRepository<ClothSizeEntity, ClothSizeModel, Guid>, IClothSizeRepository
    {
        private readonly ElBaytContext _dbContext;
        private readonly ITypeMapper _mapper;
        

        public ClothSizeRepository(ElBaytContext dbContext, ITypeMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }

        public async Task<ClothSizeEntity> GetClothSizeByName(string Name, Guid Id)
        {
            var size = await _dbContext.ClothSizes
               .Where(c => c.Name.Trim() == Name && c.Id != Id).
               AsNoTracking().FirstOrDefaultAsync();
            return _mapper.Map<ClothSizeModel, ClothSizeEntity>(size);
        }

        public async Task UpdateClothSize(ClothSizeEntity clothSize)
        {
            var Size = await _dbContext.ClothSizes.FindAsync(clothSize.Id);
            Size.Name = clothSize.Name;
            Size.Width = clothSize.Width;
            Size.Height= clothSize.Height;
        }
    }
}
