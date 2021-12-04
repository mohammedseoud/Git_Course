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
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace ElBayt.Core.GenericIRepository
{
    public class SPRepository : ISPRepository
    {
        private readonly DbContext _dbContext;
        private static string ConnectionString = "";

        public SPRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            ConnectionString = _dbContext.Database.GetDbConnection().ConnectionString;
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void Execute<TEntity>(string ProcName, DynamicParameters parameters = null)
        {
            using (var SqlCon = new SqlConnection(ConnectionString))
            {
                SqlCon.Open();
                SqlCon.Execute(ProcName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public IEnumerable<TEntity> List<TEntity>(string ProcName, DynamicParameters parameters = null)
        {
            using (var SqlCon = new SqlConnection(ConnectionString))
            {
                SqlCon.Open();
                return SqlCon.Query<TEntity>(ProcName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public Tuple<IEnumerable<TEntity1>, IEnumerable<TEntity2>> List<TEntity1, TEntity2>(string ProcName, DynamicParameters parameters = null)
        {
            using (var SqlCon = new SqlConnection(ConnectionString))
            {
                SqlCon.Open();
                var result = SqlCon.QueryMultiple(ProcName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                var item1 = result.Read<TEntity1>().ToList();
                var item2 = result.Read<TEntity2>().ToList();

                return new Tuple<IEnumerable<TEntity1>, IEnumerable<TEntity2>>(item1, item2);
            }
        }

        public TEntity OneRecord<TEntity>(string ProcName, DynamicParameters parameters = null)
        {
            using (var SqlCon = new SqlConnection(ConnectionString))
            {
                SqlCon.Open();
                var value = SqlCon.Query<TEntity>(ProcName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return (TEntity)Convert.ChangeType(value.FirstOrDefault(), typeof(TEntity));
            }
        }

        public TEntity Single<TEntity>(string ProcName, DynamicParameters parameters = null)
        {
            using (var SqlCon = new SqlConnection(ConnectionString))
            {
                SqlCon.Open();
                var value = SqlCon.ExecuteScalar<TEntity>(ProcName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return (TEntity)Convert.ChangeType(value, typeof(TEntity));
            }
        }

        public async Task<TEntity> SingleAsnyc<TEntity>(string ProcName, DynamicParameters parameters = null)
        {
            using (var SqlCon = new SqlConnection(ConnectionString))
            {
                SqlCon.Open();
                var value = await SqlCon.ExecuteScalarAsync<TEntity>(ProcName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return (TEntity)Convert.ChangeType(value, typeof(TEntity));
            }
        }

        public async Task ExecuteAsnyc<TEntity>(string ProcName, DynamicParameters parameters = null)
        {
            using (var SqlCon = new SqlConnection(ConnectionString))
            {
                SqlCon.Open();
                await SqlCon.ExecuteAsync(ProcName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<TEntity> OneRecordAsnyc<TEntity>(string ProcName, DynamicParameters parameters = null)
        {
            using (var SqlCon = new SqlConnection(ConnectionString))
            {
                SqlCon.Open();
                var value = await SqlCon.QueryAsync<TEntity>(ProcName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return (TEntity)Convert.ChangeType(value.FirstOrDefault(), typeof(TEntity));
            }
        }

        public async Task<IEnumerable<TEntity>> ListAsnyc<TEntity>(string ProcName, DynamicParameters parameters = null)
        {
            using (var SqlCon = new SqlConnection(ConnectionString))
            {
                SqlCon.Open();
                return await SqlCon.QueryAsync<TEntity>(ProcName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<Tuple<IEnumerable<TEntity1>, IEnumerable<TEntity2>>> ListAsnyc<TEntity1, TEntity2>(string ProcName, DynamicParameters parameters = null)
        {
            using (var SqlCon = new SqlConnection(ConnectionString))
            {
                SqlCon.Open();
                var result = await SqlCon.QueryMultipleAsync(ProcName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                var item1 = result.Read<TEntity1>().ToList();
                var item2 = result.Read<TEntity2>().ToList();

                return new Tuple<IEnumerable<TEntity1>, IEnumerable<TEntity2>>(item1, item2);
            }
        }
    }
}
