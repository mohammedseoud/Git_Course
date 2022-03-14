using ElBayt.Core.GenericIRepository;
using ElBayt.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;


namespace ElBayt.Core.IUnitOfWork
{
    public interface IELBaytUnitOfWork : Common.UnitOfWork.IUnitOfWork
    {
        IClothDepartmentRepository ClothDepartmentRepository { get; }
        IClothTypeRepository ClothTypeRepository { get; }
        IClothCategoryRepository ClothCategoryRepository { get; }
        IClothRepository ClothRepository { get; }
        IClothImageRepository ClothImageRepository { get; }
        IProductRepository ProductRepository { get; }
        IProductImageRepository ProductImageRepository { get; }
        IProductCategoryRepository ProductCategoryRepository { get; }
        IProductTypeRepository ProductTypeRepository { get; }
        IServiceRepository ServiceRepository { get; }
        IServiceDepartmentRepository ServiceDepartmentRepository { get; }
        ISPRepository SP { get; }
        
    }
}
