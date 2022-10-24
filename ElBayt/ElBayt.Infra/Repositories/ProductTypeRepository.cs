using ElBayt.Common.Infra.Common;
using ElBayt.Common.Core.Mapping;
using ElBayt.Core.IRepositories;
using ElBayt.Infra.Context;
using System;
using ElBayt.Common.Infra.Models;

namespace ElBayt.Infra.Repositories
{
    public class ProductTypeRepository : GenericRepository<ProductTypeModel, int>, IProductTypeRepository
    {
        private readonly ElBaytContext _dbContext;
        private readonly ITypeMapper _mapper;
        

        public ProductTypeRepository(ElBaytContext dbContext, ITypeMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }

        //public async Task<ProductTypeEntity> GetProductTypeByName(string Name,Guid Id)
        //{
        //    var department = await _dbContext.ProductTypes
        //       .Where(c => c.Name.Trim() == Name && c.Id != Id).
        //       AsNoTracking().FirstOrDefaultAsync();
        //    return _mapper.Map<ProductTypeModel, ProductTypeEntity>(department);
        //}

        //public async Task UpdateProductType(ProductTypeEntity productType)
        //{
        //    var Type = await _dbContext.ProductTypes.FindAsync(productType.Id);
        //    Type.Name = productType.Name;
        //    //Type.DepartmentId = productType.DepartmentId;
        //}
    }
}
