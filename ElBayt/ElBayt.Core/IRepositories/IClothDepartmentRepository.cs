using ElBayt.Core.GenericIRepository;
using System.Threading.Tasks;
using ElBayt.Core.Models;

namespace ElBayt.Core.IRepositories
{
    public interface IClothDepartmentRepository : IGenericRepository<ClothDepartmentModel, int>
    {
        Task UpdateClothDepartment(ClothDepartmentModel clothDepartment);
        Task<ClothDepartmentModel> GetClothDepartmentByName(string Name, int Id);
    }
}
