using ElBayt.Common.Entities;
using ElBayt.Common.Infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ElBayt.Common.Common;
using Dapper;

namespace ElBayt.Core.GenericIRepository
{
    public interface ISPRepository : IDisposable
    {
        TEntity Single<TEntity>(string ProcName, DynamicParameters parameters = null);
        Task<TEntity> SingleAsnyc<TEntity>(string ProcName, DynamicParameters parameters = null);
        void Execute<TEntity>(string ProcName, DynamicParameters parameters = null);
        Task ExecuteAsnyc<TEntity>(string ProcName, DynamicParameters parameters = null);
        TEntity OneRecord<TEntity>(string ProcName, DynamicParameters parameters = null);
        Task<TEntity> OneRecordAsnyc<TEntity>(string ProcName, DynamicParameters parameters = null);
        IEnumerable<TEntity> List<TEntity>(string ProcName, DynamicParameters parameters = null);
        Task<IEnumerable<TEntity>> ListAsnyc<TEntity>(string ProcName, DynamicParameters parameters = null);
        Tuple<IEnumerable<TEntity1>, IEnumerable<TEntity2>> List<TEntity1, TEntity2>(string ProcName, DynamicParameters parameters = null);
        Task<Tuple<IEnumerable<TEntity1>, IEnumerable<TEntity2>>> ListAsnyc<TEntity1, TEntity2>(string ProcName, DynamicParameters parameters = null);
    }
}
