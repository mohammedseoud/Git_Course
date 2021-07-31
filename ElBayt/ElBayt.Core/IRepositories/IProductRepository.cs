using ElBayt.Common.Infra.Common;
using System;
using System.Collections.Generic;
using System.Text;
using ElBayt.;
using ElBayt.Core.Entities;

namespace ElBayt.Core.IRepositories
{
    public interface IProductRepository : IGenericRepository<ProductEntity>
    {
    }
}
