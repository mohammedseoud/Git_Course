using ElBayt.Infra.Entities;
using ElBayt.Core.GenericIRepository;
using System;
using System.Threading.Tasks;
using ElBayt.Core.Models;

namespace ElBayt.Core.IRepositories
{
    public interface IColorRepository : IGenericRepository<ColorModel, Guid>
    {
        Task UpdateColor(ColorModel Color);
        Task<ColorModel> GetColorByName(string Name, Guid Id);
    }
}
