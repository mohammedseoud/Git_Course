using ElBayt.Core.GenericIRepository;
using ElBayt.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;


namespace ElBayt.Core.IUnitOfWork
{
    public interface IELBaytUnitOfWork : Common.UnitOfWork.IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        IProductCategoryRepository ProductCategoryRepository { get; }
        IProductTypeRepository ProductTypeRepository { get; }
        IProductDepartmentRepository ProductDepartmentRepository { get; }
        IServiceRepository ServiceRepository { get; }
        IServiceDepartmentRepository ServiceDepartmentRepository { get; }
        ISPRepository SP { get; }
        
    }
}
