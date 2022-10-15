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
    public class ClothSizeRepository : GenericRepository<ClothSizeModel, int>, IClothSizeRepository
    {
        private readonly ElBaytContext _dbContext;
        private readonly ITypeMapper _mapper;
        

        public ClothSizeRepository(ElBaytContext dbContext, ITypeMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }

        public async Task<ClothSizeModel> GetClothSizeByName(string Name, int Id)
        {
            var size = await _dbContext.ClothSizes
               .Where(c => c.Name.Trim() == Name && c.Id != Id).
               AsNoTracking().FirstOrDefaultAsync();
            return size;
        }

        public async Task<List<ClothSizeModel>> GetClothSizes(int ClothId)
        {
            return await _dbContext.ClothSizes
                          .Where(c =>  c.ClothId == ClothId)
                          .ToListAsync();  
        }

        

        public async Task UpdateClothSize(ClothSizeModel clothSize)
        {
            var Size = await _dbContext.ClothSizes.FindAsync(clothSize.Id);
            if (Size != null)
            {
                Size.Name = clothSize.Name;
                Size.Width = clothSize.Width;
                Size.Height = clothSize.Height;
            }
        }
    }
}
