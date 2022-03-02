using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                return await SqlCon.QueryAsync<TEntity>(ProcName, parameters ,commandType: System.Data.CommandType.StoredProcedure);
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
        public async Task<Tuple<IEnumerable<TEntity1>, IEnumerable<TEntity2>, IEnumerable<TEntity3>>> ThreeListAsnyc<TEntity1, TEntity2, TEntity3>(string ProcName, DynamicParameters parameters = null)
        {
            using (var SqlCon = new SqlConnection(ConnectionString))
            {
                SqlCon.Open();
                var result = await SqlCon.QueryMultipleAsync(ProcName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                var item1 = result.Read<TEntity1>().ToList();
                var item2 = result.Read<TEntity2>().ToList();
                var item3 = result.Read<TEntity3>().ToList();

                return new Tuple<IEnumerable<TEntity1>, IEnumerable<TEntity2>, IEnumerable<TEntity3>>(item1, item2, item3);
            }
        }

        public async Task<Tuple<IEnumerable<TEntity1>, IEnumerable<TEntity2>, IEnumerable<TEntity3>, IEnumerable<TEntity4>>> FourListAsnyc<TEntity1, TEntity2, TEntity3, TEntity4>(string ProcName, DynamicParameters parameters = null)
        {
            using (var SqlCon = new SqlConnection(ConnectionString))
            {
                SqlCon.Open();
                var result = await SqlCon.QueryMultipleAsync(ProcName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                var item1 = result.Read<TEntity1>().ToList();
                var item2 = result.Read<TEntity2>().ToList();
                var item3 = result.Read<TEntity3>().ToList();
                var item4 = result.Read<TEntity4>().ToList();

                return new Tuple<IEnumerable<TEntity1>, IEnumerable<TEntity2>, IEnumerable<TEntity3>, IEnumerable<TEntity4>>(item1, item2, item3, item4);
            }
        }

        public async Task<Tuple<IEnumerable<TEntity1>, IEnumerable<TEntity2>, IEnumerable<TEntity3>, IEnumerable<TEntity4>, IEnumerable<TEntity5>>> FiveListAsnyc<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5>(string ProcName, DynamicParameters parameters = null)
        {
            using (var SqlCon = new SqlConnection(ConnectionString))
            {
                SqlCon.Open();
                var result = await SqlCon.QueryMultipleAsync(ProcName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                var item1 = result.Read<TEntity1>().ToList();
                var item2 = result.Read<TEntity2>().ToList();
                var item3 = result.Read<TEntity3>().ToList();
                var item4 = result.Read<TEntity4>().ToList();
                var item5 = result.Read<TEntity5>().ToList();

                return new Tuple<IEnumerable<TEntity1>, IEnumerable<TEntity2>, IEnumerable<TEntity3>, IEnumerable<TEntity4>, IEnumerable<TEntity5>>(item1, item2, item3, item4, item5);
            }
        }

        public async Task<Tuple<IEnumerable<TEntity1>, IEnumerable<TEntity2>, IEnumerable<TEntity3>, IEnumerable<TEntity4>, IEnumerable<TEntity5>, IEnumerable<TEntity6>>> SixListAsnyc<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6>(string ProcName, DynamicParameters parameters = null)
        {
            using (var SqlCon = new SqlConnection(ConnectionString))
            {
                SqlCon.Open();
                var result = await SqlCon.QueryMultipleAsync(ProcName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                var item1 = result.Read<TEntity1>().ToList();
                var item2 = result.Read<TEntity2>().ToList();
                var item3 = result.Read<TEntity3>().ToList();
                var item4 = result.Read<TEntity4>().ToList();
                var item5 = result.Read<TEntity5>().ToList();
                var item6 = result.Read<TEntity6>().ToList();

                return new Tuple<IEnumerable<TEntity1>, IEnumerable<TEntity2>, IEnumerable<TEntity3>, IEnumerable<TEntity4>, IEnumerable<TEntity5>, IEnumerable<TEntity6>>(item1, item2, item3, item4, item5, item6);
            }
        }

        public async Task<Tuple<IEnumerable<TEntity1>, IEnumerable<TEntity2>, IEnumerable<TEntity3>, IEnumerable<TEntity4>, IEnumerable<TEntity5>, IEnumerable<TEntity6>, IEnumerable<TEntity7>>> SevenListAsnyc<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7>(string ProcName, DynamicParameters parameters = null)
        {
            using (var SqlCon = new SqlConnection(ConnectionString))
            {
                SqlCon.Open();
                var result = await SqlCon.QueryMultipleAsync(ProcName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                var item1 = result.Read<TEntity1>().ToList();
                var item2 = result.Read<TEntity2>().ToList();
                var item3 = result.Read<TEntity3>().ToList();
                var item4 = result.Read<TEntity4>().ToList();
                var item5 = result.Read<TEntity5>().ToList();
                var item6 = result.Read<TEntity6>().ToList();
                var item7 = result.Read<TEntity7>().ToList();

                return new Tuple<IEnumerable<TEntity1>, IEnumerable<TEntity2>, IEnumerable<TEntity3>, IEnumerable<TEntity4>, IEnumerable<TEntity5>, IEnumerable<TEntity6>, IEnumerable<TEntity7>>(item1, item2, item3, item4, item5, item6, item7);
            }
        }

        public async Task<Tuple<IEnumerable<TEntity1>, IEnumerable<TEntity2>, IEnumerable<TEntity3>, IEnumerable<TEntity4>, IEnumerable<TEntity5>, IEnumerable<TEntity6>, IEnumerable<TEntity7>, IEnumerable<TEntity8>>> EightListAsnyc<TEntity1, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8>(string ProcName, DynamicParameters parameters = null)
        {
            using (var SqlCon = new SqlConnection(ConnectionString))
            {
                SqlCon.Open();
                var result = await SqlCon.QueryMultipleAsync(ProcName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                var item1 = result.Read<TEntity1>().ToList();
                var item2 = result.Read<TEntity2>().ToList();
                var item3 = result.Read<TEntity3>().ToList();
                var item4 = result.Read<TEntity4>().ToList();
                var item5 = result.Read<TEntity5>().ToList();
                var item6 = result.Read<TEntity6>().ToList();
                var item7 = result.Read<TEntity7>().ToList();
                var item8 = result.Read<TEntity8>().ToList();

                return new Tuple<IEnumerable<TEntity1>, IEnumerable<TEntity2>, IEnumerable<TEntity3>, IEnumerable<TEntity4>, IEnumerable<TEntity5>, IEnumerable<TEntity6>, IEnumerable<TEntity7>, IEnumerable<TEntity8>>(item1, item2, item3, item4, item5, item6, item7, item8);
            }
        }
    }
}
