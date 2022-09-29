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
    public class ColorRepository : GenericRepository<ColorEntity, ColorModel, Guid>, IColorRepository
    {
        private readonly ElBaytContext _dbContext;
        private readonly ITypeMapper _mapper;
        

        public ColorRepository(ElBaytContext dbContext, ITypeMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }

        public async Task<ColorEntity> GetColorByName(string Name, Guid Id)
        {
            var size = await _dbContext.Colors.Value
               .Where(c => c.Name.Trim() == Name && c.Id != Id).
               AsNoTracking().FirstOrDefaultAsync();
            return _mapper.Map<ColorModel, ColorEntity>(size);
        }

     
        

        public async Task UpdateColor(ColorEntity Color)
        {
            var color = await _dbContext.Colors.Value.FindAsync(Color.Id);
           
            if (color != null)
            {
                color.Name = Color.Name;
            }
        }
    }
}
