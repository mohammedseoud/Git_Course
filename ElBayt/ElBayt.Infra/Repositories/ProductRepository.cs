using ElBayt.Common.Infra.Common;
using ElBayt.Common.Infra.Mapping;
using ElBayt.Common.Mapping;
using ElBayt.Core.Entities;
using ElBayt.Core.IRepositories;
using ElBayt.Infra.Context;
using ElBayt.Infra.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.Infra.Repositories
{
    public class ProductRepository: GenericRepository<ProductEntity>, IProductRepository
    {
        private readonly ElBaytContext _dbContext;
        private readonly ITypeMapper _mapper;

        public ProductRepository(ElBaytContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = new TypeMapper();
        }
    }
}
