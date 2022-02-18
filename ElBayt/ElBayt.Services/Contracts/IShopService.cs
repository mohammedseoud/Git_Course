using ElBayt.DTO.ELBayt.DBDTOs;
using ElBayt.DTO.ELBayt.DTOs;
using System;
using System.Threading.Tasks;

namespace ElBayt.Services.Contracts
{
    public interface IShopService
    {
        public Task<ShopDataDTO> GetShopData(string DepartmentName);
    }
}
