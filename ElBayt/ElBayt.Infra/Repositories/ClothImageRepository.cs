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
using System.Data.Entity;

namespace ElBayt.Infra.Repositories
{
    public class ClothImageRepository : GenericRepository<ClothImageEntity,ClothImageModel, Guid>, IClothImageRepository   
    { 
       
        private readonly ElBaytContext _dbContext;
        private readonly ITypeMapper _mapper;
        

        public ClothImageRepository(ElBaytContext dbContext, ITypeMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }

        public object GetClothImages(Guid ClothId)
        {
            var models = _dbContext.Set<ClothImageModel>().
                Where(c => c.ClothId == ClothId);

            var res = models.AsNoTracking().AsEnumerable();

            return models;

        }

        public bool DeleteByURL(string URL)
        {
            var image = _dbContext.ClothImages
               .Where(c => c.URL == URL).
               AsNoTracking().ToList().FirstOrDefault();
            try
            {
                _dbContext.ClothImages.Remove(image);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
