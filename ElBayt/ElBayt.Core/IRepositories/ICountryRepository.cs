using ElBayt.Infra.Entities;
using ElBayt.Core.GenericIRepository;
using System;
using ElBayt.Core.Models;

namespace ElBayt.Core.IRepositories
{
    public interface ICountryRepository : IGenericRepository<CountryModel, int>
    {
    }
}
