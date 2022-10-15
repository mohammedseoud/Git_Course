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
using System.Linq;
using System.Linq.Expressions;
using ElBayt.Common.Common;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ElBayt.Infra.Repositories
{
    public class ClothTypeRepository : GenericRepository<ClothTypeModel, int>, IClothTypeRepository
    {
        private readonly ElBaytContext _dbContext;
        private readonly ITypeMapper _mapper;
        

        public ClothTypeRepository(ElBaytContext dbContext, ITypeMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }

        public async Task<ClothTypeModel> GetClothTypeByName(string Name, int Id)
        {
            var clothtype = await _dbContext.ClothTypes
                .Where(c => c.Name.Trim() == Name && c.Id != Id).
                AsNoTracking().FirstOrDefaultAsync();
            return clothtype;
        }

        public async Task UpdateClothType(ClothTypeModel clothType)
        {
            var ClothType = await _dbContext.ClothTypes.FindAsync(clothType.Id);

            if (ClothType != null)
            {
                ClothType.Name = clothType.Name;
                ClothType.ClothDepartmentId = clothType.ClothDepartmentId;
            }
        }
    }
}
