﻿using AutoMapper;
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
    public class ProductTypeRepository : GenericRepository<ProductTypeEntity, ProductTypeModel, Guid>, IProductTypeRepository
    {
        private readonly ElBaytContext _dbContext;
        private readonly ITypeMapper _mapper;
        

        public ProductTypeRepository(ElBaytContext dbContext, ITypeMapper mapper) : base(dbContext, mapper)
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
