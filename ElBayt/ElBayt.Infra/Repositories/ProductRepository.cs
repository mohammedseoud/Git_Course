using ElBayt.Common.Infra.Common;
using ElBayt.Common.Core.Mapping;
using ElBayt.Core.IRepositories;
using ElBayt.Infra.Context;
using System;
using ElBayt.Common.Infra.Models;

namespace ElBayt.Infra.Repositories
{
    public class ProductRepository: GenericRepository<ProductModel, int>, IProductRepository
    {
        private readonly ElBaytContext _dbContext;
        private readonly ITypeMapper _mapper;
        

        public ProductRepository(ElBaytContext dbContext, ITypeMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }

        //public async Task AddProductImage(ProductImageEntity Image)
        //{
        //    var model = _mapper.Map<ProductImageEntity, ProductImageModel>(Image);
        //    await _dbContext.ProductImages.AddAsync(model);
        //}

        //public async Task UpdateProduct(ProductEntity product)
        //{
        //    var Product = await _dbContext.Products.FindAsync(product.Id);
        //    Product.Name = product.Name;
        //    Product.Description = product.Description;
        //    Product.Price = product.Price;
        //    Product.PriceAfterDiscount = product.PriceAfterDiscount;
        //    //Product.ProductCategoryId = product.ProductCategoryId;
        //}

        //public async Task<ProductEntity> GetProductByName(string Name,Guid Id)
        //{
        //    var product = await _dbContext.Products
        //        .Where(c => c.Name.Trim() == Name && c.Id != Id).
        //        AsNoTracking().FirstOrDefaultAsync();
        //    return _mapper.Map<ProductModel, ProductEntity>(product);
        //}
       
    }
}
