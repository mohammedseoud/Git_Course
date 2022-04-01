
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
using System.Collections.Generic;

namespace ElBayt.Infra.Repositories
{
    public class ClothInfoRepository : GenericRepository<ClothInfoEntity,ClothInfoModel, Guid>, IClothInfoRepository
    {
        private readonly ElBaytContext _dbContext;
        private readonly ITypeMapper _mapper;
        

        public ClothInfoRepository(ElBaytContext dbContext, ITypeMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }

        public async Task<object> GetClothInfo(Guid ClothId)
        {
            return await _dbContext.ClothInfo.Where(c => c.ClothId == ClothId).ToListAsync();
        }

        public async Task<object> GetClothInfo(Guid SizeId, Guid? ColorId, Guid? BrandId)
        {
            return await _dbContext.ClothInfo.Where(c => c.SizeId == SizeId
            && c.BrandId == BrandId && c.ColorId == ColorId).ToListAsync();
        }

        public async Task UpdateInfo(ClothInfoEntity Info)
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
