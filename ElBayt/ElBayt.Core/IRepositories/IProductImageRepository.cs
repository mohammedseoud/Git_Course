﻿using ElBayt.Core.Entities;
using ElBayt.Core.GenericIRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElBayt.Core.IRepositories
{
    public interface IProductImageRepository : IGenericRepository<ProductImageEntity, Guid>
    {
        object GetProductImages(Guid ProductId);
    }
}
