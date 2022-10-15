
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
using System.Collections.Generic;

namespace ElBayt.Infra.Repositories
{
    public class ClothInfoRepository : GenericRepository<ClothInfoModel, int>, IClothInfoRepository
    {
        private readonly ElBaytContext _dbContext;
        private readonly ITypeMapper _mapper;
        

        public ClothInfoRepository(ElBaytContext dbContext, ITypeMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }

        public async Task<List<ClothInfoModel>> GetClothInfo(int ClothId)
        {
            return await _dbContext.ClothInfo.Where(c => c.ClothId == ClothId).ToListAsync();
        }

        public async Task<List<ClothInfoModel>> GetClothInfo(int SizeId, int? ColorId, int? BrandId)
        {
            return await _dbContext.ClothInfo.Where(c => c.SizeId == SizeId
            && c.BrandId == BrandId && c.ColorId == ColorId).ToListAsync();
        }

        public async Task UpdateInfo(ClothInfoModel Info)
        {
            var _Info = await _dbContext.ClothInfo.FindAsync(Info.Id);
            if (_Info != null)
            {
                _Info.Amount = Info.Amount;
                _Info.Price = Info.Price;
                _Info.PriceAfterDiscount = Info.PriceAfterDiscount;
            }
        }

       

    }
}
