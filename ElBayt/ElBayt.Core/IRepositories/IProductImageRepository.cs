using ElBayt.Common.Entities;
using ElBayt.Infra.Entities;
using ElBayt.Core.GenericIRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElBayt.Common.Infra.Models;

namespace ElBayt.Core.IRepositories
{
    public interface IProductImageRepository : IGenericRepository<ProductImageModel, Guid>
    {
        //object GetProductImages(Guid ProductId);
        //bool DeleteByURL(string URL);
    }
}
