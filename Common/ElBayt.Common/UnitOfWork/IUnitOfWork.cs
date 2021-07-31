using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElBayt.Common.UnitOfWork
{
    public interface IUnitOfWork
    {
        int Save();

        Task<int> SaveAsync();

        Task<int> SaveAsync(CancellationToken cancellationToken);

        void BeginTransaction();

        void Commit();

        void RollBack();
    }
}
