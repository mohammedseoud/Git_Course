using ElBayt.Core.Entities;
using ElBayt.Core.GenericIRepository;
using System;
using System.Threading.Tasks;

namespace ElBayt.Core.IRepositories
{
    public interface IColorRepository : IGenericRepository<ColorEntity, Guid>
    {
        Task UpdateColor(ColorEntity Color);
        Task<ColorEntity> GetColorByName(string Name, Guid Id);
    }
}
