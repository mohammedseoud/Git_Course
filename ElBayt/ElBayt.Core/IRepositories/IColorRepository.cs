using ElBayt.Core.GenericIRepository;
using System.Threading.Tasks;
using ElBayt.Core.Models;

namespace ElBayt.Core.IRepositories
{
    public interface IColorRepository : IGenericRepository<ColorModel, int>
    {
        Task UpdateColor(ColorModel Color);
        Task<ColorModel> GetColorByName(string Name, int Id);
    }
}
