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

namespace ElBayt.Infra.Repositories
{
    public class ProductCategoryRepository : GenericRepository<ProductCategoryEntity, ProductCategoryModel>, IProductCategoryRepository
    {
        private readonly ElBaytContext _dbContext;
        private readonly ITypeMapper _mapper;
        

        public ProductCategoryRepository(ElBaytContext dbContext, ITypeMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }      
    }
}
