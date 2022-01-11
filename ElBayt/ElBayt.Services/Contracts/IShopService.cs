using ElBayt.DTO.ELBayt.DBDTOs;
using System;
using System.Threading.Tasks;

namespace ElBayt.Services.Contracts
{
    public interface IShopService
    {
        public Task<ProductDTO> GetShopData(Guid Id);
    }
}
