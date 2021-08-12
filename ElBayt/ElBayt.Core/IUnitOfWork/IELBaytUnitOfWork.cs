using ElBayt.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;


namespace ElBayt.Core.IUnitOfWork
{
    public interface IELBaytUnitOfWork : Common.UnitOfWork.IUnitOfWork
    {
         IProductRepository ProductRepository { get; }
    }
}
