using ElBayt.DTO.ELBaytDTO_s;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElBayt.Services.Contracts
{
    public interface IProductService
    {
        public Task AddNewProduct(ProductDTO product);
    }
}
