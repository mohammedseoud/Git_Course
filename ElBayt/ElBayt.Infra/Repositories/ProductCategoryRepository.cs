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
    public class ProductCategoryRepository : GenericRepository<ProductCategoryEntity, ProductCategoryModel, Guid>, IProductCategoryRepository
    {
        private readonly ElBaytContext _dbContext;
        private readonly ITypeMapper _mapper;
        

        public ProductCategoryRepository(ElBaytContext dbContext, ITypeMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }

        public async Task<ProductCategoryEntity> GetProductCategoryByName(string Name)
        {
            var category = await _dbContext.ProductCategories
               .Where(c => c.Name.Trim() == Name).
               AsNoTracking().FirstOrDefaultAsync();
            return _mapper.Map<ProductCategoryModel, ProductCategoryEntity>(category);
        }

        public async Task UpdateProductCategory(ProductCategoryEntity productCategory)
        {
            var Category = await _dbContext.ProductCategories.FindAsync(productCategory.Id);
            Category.Name = productCategory.Name;
            Category.ProductTypeId = productCategory.ProductTypeId;
        }
    }
}
