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
        Task<Tuple<IEnumerable<TEntity1>, IEnumerable<TEntity2>, IEnumerable<TEntity3>>> ThreeListAsnyc<TEntity1, TEntity2, TEntity3>(string ProcName, DynamicParameters parameters = null);
        Task<Tuple<IEnumerable<TEntity1>, IEnumerable<TEntity2>, IEnumerable<TEntity3>, IEnumerable<TEntity4>>> FourListAsnyc<TEntity1, TEntity2, TEntity3, TEntity4>(string ProcName, DynamicParameters parameters = null);
        Task<Tuple<IEnumerable<TEntity1>, IEnumerable<TEntity2>, IEnumerable<TEntity3>, IEnumerable<TEntity4>, IEnumerable<TEntity5>>> FiveListAsnyc<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5>(string ProcName, DynamicParameters parameters = null);
        Task<Tuple<IEnumerable<TEntity1>, IEnumerable<TEntity2>, IEnumerable<TEntity3>, IEnumerable<TEntity4>, IEnumerable<TEntity5>, IEnumerable<TEntity6>>> SixListAsnyc<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6>(string ProcName, DynamicParameters parameters = null);
        Task<Tuple<IEnumerable<TEntity1>, IEnumerable<TEntity2>, IEnumerable<TEntity3>, IEnumerable<TEntity4>, IEnumerable<TEntity5>, IEnumerable<TEntity6>, IEnumerable<TEntity7>>> SevenListAsnyc<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7>(string ProcName, DynamicParameters parameters = null);
        Task<Tuple<IEnumerable<TEntity1>, IEnumerable<TEntity2>, IEnumerable<TEntity3>, IEnumerable<TEntity4>, IEnumerable<TEntity5>, IEnumerable<TEntity6>, IEnumerable<TEntity7>,IEnumerable<TEntity8>>> EightListAsnyc<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8>(string ProcName, DynamicParameters parameters = null);
     }
}
