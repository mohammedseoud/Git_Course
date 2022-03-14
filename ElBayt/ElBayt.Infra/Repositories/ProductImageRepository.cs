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
using ElBayt.Common.Entities;
using ElBayt.Common.Infra.Models;

namespace ElBayt.Infra.Repositories
{
    public class ProductImageRepository : GenericRepository<ProductImageEntity,ProductImageModel, Guid>, IProductImageRepository   
    { 
       
        private readonly ElBaytContext _dbContext;
        private readonly ITypeMapper _mapper;
        

        public ProductImageRepository(ElBaytContext dbContext, ITypeMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }

        //public object GetProductImages(Guid ProductId)
        //{
        //    var models =  _dbContext.Set<ProductImageModel>().
        //        Where(c => c.ProductId == ProductId);

        //    var res =  models.AsNoTracking().AsEnumerable();

        //    return models;

        //}

        //public bool DeleteByURL(string URL)
        //{
        //    var image = _dbContext.ProductImages
        //       .Where(c => c.URL == URL).
        //       AsNoTracking().ToList().FirstOrDefault();
        //    try
        //    {
        //        _dbContext.ProductImages.Remove(image);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
    }
}
