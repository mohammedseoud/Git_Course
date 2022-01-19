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
using System.Linq;
using System.Linq.Expressions;
using ElBayt.Common.Common;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ElBayt.Infra.Repositories
{
    public class ProductDepartmentRepository : GenericRepository<ProductDepartmentEntity, ProductDepartmentModel, Guid>, IProductDepartmentRepository
    {
        private readonly ElBaytContext _dbContext;
        private readonly ITypeMapper _mapper;
        

        public ProductDepartmentRepository(ElBaytContext dbContext, ITypeMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }

        public async Task<ProductDepartmentEntity> GetProductDepartmentByName(string Name)
        {
            var department = await _dbContext.ProductDepartments
                .Where(c => c.Name.Trim() == Name).
                AsNoTracking().FirstOrDefaultAsync();
            return _mapper.Map<ProductDepartmentModel, ProductDepartmentEntity>(department);
        }

        public async Task UpdateProductDepartment(ProductDepartmentEntity productDepartment)
        {
            var Department = await _dbContext.ProductDepartments.FindAsync(productDepartment.Id);
            Department.Name = productDepartment.Name;
        }
    }
}
