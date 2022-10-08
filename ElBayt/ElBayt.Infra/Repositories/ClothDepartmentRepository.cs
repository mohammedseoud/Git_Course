using AutoMapper;
using ElBayt.Common.Infra.Common;
using ElBayt.Common.Infra.Mapping;
using ElBayt.Common.Core.Mapping;
using ElBayt.Infra.Entities;
using ElBayt.Core.IRepositories;
using ElBayt.Infra.Context;
using ElBayt.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ElBayt.Common.Infra.Models;

namespace ElBayt.Infra.Repositories
{
    public class ClothDepartmentRepository : GenericRepository<ClothDepartmentModel, Guid>, IClothDepartmentRepository
    {
        private readonly ElBaytContext _dbContext;
        private readonly ITypeMapper _mapper;
        

        public ClothDepartmentRepository(ElBaytContext dbContext, ITypeMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }

        public async Task<ClothDepartmentModel> GetClothDepartmentByName(string Name, Guid Id)
        {
            var department = await _dbContext.ClothDepartments
               .Where(c => c.Name.Trim() == Name && c.Id != Id).
               AsNoTracking().FirstOrDefaultAsync();
            return department;
        }

        public async Task UpdateClothDepartment(ClothDepartmentModel clothDepartment)
        {
            var ClothDepartment = await _dbContext.ClothDepartments.FindAsync(clothDepartment.Id);

            if (ClothDepartment != null)
            {
                ClothDepartment.Name = clothDepartment.Name;
                ClothDepartment.DepartmentPic = clothDepartment.DepartmentPic;
            }
        }
    }
}
