using ElBayt.Core.Entities;
using ElBayt.Core.GenericIRepository;
using System;

namespace ElBayt.Core.IRepositories
{
    public interface IServiceRepository : IGenericRepository<ServiceEntity,Guid>
    {

    }
}
