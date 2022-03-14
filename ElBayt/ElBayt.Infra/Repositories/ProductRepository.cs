using ElBayt.Common.Infra.Common;
using ElBayt.Common.Core.Mapping;
using ElBayt.Core.Entities;
using ElBayt.Core.IRepositories;
using ElBayt.Infra.Context;
using ElBayt.Infra.Models;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ElBayt.Common.Entities;
using ElBayt.Common.Infra.Models;

namespace ElBayt.Infra.Repositories
{
    public class ProductRepository: GenericRepository<ProductEntity,ProductModel, Guid>, IProductRepository
    {
        private readonly ElBaytContext _dbContext;
        private readonly ITypeMapper _mapper;
        

        public ProductRepository(ElBaytContext dbContext, ITypeMapper mapper) : base(dbContext, mapper)
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
