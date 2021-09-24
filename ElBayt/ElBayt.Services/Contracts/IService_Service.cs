using ElBayt.DTO.ELBaytDTO_s;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElBayt.Services.Contracts
{
    public interface IService_Service
    {
        public Task AddNewService(ServiceDTO service);
        public Task AddNewServiceDepartment(ServiceDepartmentDTO serviceDepartment);
       
    }
}
