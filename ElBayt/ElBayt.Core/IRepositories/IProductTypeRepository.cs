﻿using ElBayt.Core.Entities;
using ElBayt.Core.GenericIRepository;
using System;
using System.Threading.Tasks;

namespace ElBayt.Core.IRepositories
{
    public interface IProductTypeRepository : IGenericRepository<ProductTypeEntity, Guid>
    {
        Task UpdateProductType(ProductTypeEntity productType);
        Task<ProductTypeEntity> GetProductTypeByName(string Name);
    }
}
