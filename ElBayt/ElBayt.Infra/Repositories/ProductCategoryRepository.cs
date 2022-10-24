using ElBayt.Common.Infra.Common;
using ElBayt.Common.Core.Mapping;
using ElBayt.Core.IRepositories;
using ElBayt.Infra.Context;
using System;
using ElBayt.Common.Infra.Models;

namespace ElBayt.Infra.Repositories
{
    public class ProductCategoryRepository : GenericRepository<ProductCategoryModel, int>, IProductCategoryRepository
    {
        private readonly ElBaytContext _dbContext;
        private readonly ITypeMapper _mapper;
        

        public ProductCategoryRepository(ElBaytContext dbContext, ITypeMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }

        //public async Task<ProductCategoryEntity> GetProductCategoryByName(string Name,Guid Id)
        //{
        //    var category = await _dbContext.ProductCategories
        //       .Where(c => c.Name.Trim() == Name && c.Id != Id).
        //       AsNoTracking().FirstOrDefaultAsync();
        //    return _mapper.Map<ProductCategoryModel, ProductCategoryEntity>(category);
        //}

        //public async Task UpdateProductCategory(ProductCategoryEntity productCategory)
        //{
        //    var Category = await _dbContext.ProductCategories.FindAsync(productCategory.Id);
        //    Category.Name = productCategory.Name;
        //    //Category.ProductTypeId = productCategory.ProductTypeId;
        //}
    }
}
