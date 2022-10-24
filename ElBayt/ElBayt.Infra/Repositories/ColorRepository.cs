﻿using ElBayt.Common.Infra.Common;
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
    public class ColorRepository : GenericRepository< ColorModel, int>, IColorRepository
    {
        private readonly ElBaytContext _dbContext;
        private readonly ITypeMapper _mapper;
        

        public ColorRepository(ElBaytContext dbContext, ITypeMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }

        public async Task<ColorModel> GetColorByName(string Name, int Id)
        {
            var Color = await _dbContext.Colors
               .Where(c => c.Name.Trim() == Name && c.Id != Id).
               AsNoTracking().FirstOrDefaultAsync();
            return Color;
        }

     
        

        public async Task UpdateColor(ColorModel Color)
        {
            var color = await _dbContext.Colors.FindAsync(Color.Id);
           
            if (color != null)
            {
                color.Name = Color.Name;
            }
        }
    }
}
