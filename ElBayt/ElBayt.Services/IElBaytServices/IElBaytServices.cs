using ElBayt.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.Services.IElBaytServices
{
    public interface IElBaytServices
    {
        IProductService ProductService { get; }
        IService_Service Service_Service { get; }
    }
}
