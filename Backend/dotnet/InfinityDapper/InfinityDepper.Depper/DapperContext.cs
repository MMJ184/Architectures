using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using static Dapper.SqlMapper;

namespace InfinityDapper.Dapper
{
    public sealed class DapperContext : IDapperContext, IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        //private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();
            }
        }

        public List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<T>(sp, parms, commandType: commandType).ToList();
            }
        }

        public int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Execute(sp, parms, commandType: commandType);
            }
        }

        public T GetDynamic<T>(Expression<Func<T, bool>> predicate)
        {
            var query = DynamicQuery.GetDynamicQuery<T>(predicate);
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<T>(query.Sql, (object)query.Param).FirstOrDefault();
            }
        }

        public List<T> GetAllDynamic<T>(Expression<Func<T, bool>> predicate)
        {

            var query = DynamicQuery.GetDynamicQuery<T>(predicate);
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<T>(query.Sql, (object)query.Param).ToList();
            }
        }

        public int Insert<T>(T obj)
        {
            var query = DynamicQuery.GetInsertQuery<T>(obj);
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Execute(query, obj);
            }
        }

        public bool Update<T>(T obj)
        {
            bool ret = false;

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                ret = false;
                var query = DynamicQuery.GetUpdateQuery<T>(obj);
                var updated = db.Execute(query, obj);
                ret = (updated > 0);
            }
            return ret;
        }

        public bool Delete<T>(T obj)
        {
            bool ret = false;
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                ret = false;
                var query = DynamicQuery.GetDeleteQuery<T>(obj);
                var deleted = db.Execute(query, obj);
                ret = (deleted > 0);
            }
            return ret;
        }

        public T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();

                    using (var tran = db.BeginTransaction())
                    {
                        try
                        {
                            result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            throw ex;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (db.State == ConnectionState.Open)
                        db.Close();
                }

                return result;
            }
        }

        public T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                try
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();

                    using (var tran = db.BeginTransaction())
                    {
                        try
                        {
                            result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            throw ex;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (db.State == ConnectionState.Open)
                        db.Close();
                }

                return result;
            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>> GetMultiple<T1, T2>(string sql, object parameters,
                                        Func<GridReader, IEnumerable<T1>> func1,
                                        Func<GridReader, IEnumerable<T2>> func2)
        {
            var objs = getMultiple(sql, parameters, func1, func2);
            return Tuple.Create(objs[0] as IEnumerable<T1>, objs[1] as IEnumerable<T2>);
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> GetMultiple<T1, T2, T3>(string sql, object parameters,
                                        Func<GridReader, IEnumerable<T1>> func1,
                                        Func<GridReader, IEnumerable<T2>> func2,
                                        Func<GridReader, IEnumerable<T3>> func3)
        {
            var objs = getMultiple(sql, parameters, func1, func2, func3);
            return Tuple.Create(objs[0] as IEnumerable<T1>, objs[1] as IEnumerable<T2>, objs[2] as IEnumerable<T3>);
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>> GetMultiple<T1, T2, T3, T4, T5, T6>(string sql, DynamicParameters parameters,
                                    Func<GridReader, IEnumerable<T1>> func1,
                                    Func<GridReader, IEnumerable<T2>> func2,
                                    Func<GridReader, IEnumerable<T3>> func3,
                                    Func<GridReader, IEnumerable<T4>> func4,
                                    Func<GridReader, IEnumerable<T5>> func5,
                                    Func<GridReader, IEnumerable<T6>> func6)
        {
            var objs = getMultiple(sql, parameters, func1, func2, func3, func4, func5, func6);
            return Tuple.Create(objs[0] as IEnumerable<T1>, objs[1] as IEnumerable<T2>, objs[2] as IEnumerable<T3>, objs[3] as IEnumerable<T4>, objs[4] as IEnumerable<T5>, objs[5] as IEnumerable<T6>);
        }

        private List<object> getMultiple(string sql, object parameters, params Func<GridReader, object>[] readerFuncs)
        {
            var returnResults = new List<object>();
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var gridReader = db.QueryMultiple(sql, parameters, commandType: CommandType.StoredProcedure);

                foreach (var readerFunc in readerFuncs)
                {
                    var obj = readerFunc(gridReader);
                    returnResults.Add(obj);
                }
            }

            return returnResults;
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>> GetMultiple<T1, T2, T3, T4>(string sql, DynamicParameters parameters,
            Func<GridReader, IEnumerable<T1>> func1,
            Func<GridReader, IEnumerable<T2>> func2,
            Func<GridReader, IEnumerable<T3>> func3,
            Func<GridReader, IEnumerable<T4>> func4)
        {
            var objs = getMultiple(sql, parameters, func1, func2, func3, func4);
            return Tuple.Create(objs[0] as IEnumerable<T1>, objs[1] as IEnumerable<T2>, objs[2] as IEnumerable<T3>, objs[3] as IEnumerable<T4>);
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>> GetMultiple<T1, T2, T3, T4, T5>(string sql, DynamicParameters parameters,
            Func<GridReader, IEnumerable<T1>> func1,
            Func<GridReader, IEnumerable<T2>> func2,
            Func<GridReader, IEnumerable<T3>> func3,
            Func<GridReader, IEnumerable<T4>> func4,
            Func<GridReader, IEnumerable<T5>> func5)
        {
            var objs = getMultiple(sql, parameters, func1, func2, func3, func4, func5);
            return Tuple.Create(objs[0] as IEnumerable<T1>, objs[1] as IEnumerable<T2>, objs[2] as IEnumerable<T3>, objs[3] as IEnumerable<T4>, objs[4] as IEnumerable<T5>);
        }


        public Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> GetMultiple<T1, T2, T3>(string sql, DynamicParameters parameters, Func<GridReader, IEnumerable<T1>> func1, Func<GridReader, IEnumerable<T2>> func2, Func<GridReader, IEnumerable<T3>> func3)
        {
            var objs = getMultiple(sql, parameters, func1, func2, func3);
            return Tuple.Create(objs[0] as IEnumerable<T1>, objs[1] as IEnumerable<T2>, objs[2] as IEnumerable<T3>);
        }
        public Tuple<IEnumerable<T1>, IEnumerable<T2>> GetMultiple<T1, T2>(string sql, DynamicParameters parameters, Func<GridReader, IEnumerable<T1>> func1, Func<GridReader, IEnumerable<T2>> func2)
        {
            var objs = getMultiple(sql, parameters, func1, func2);
            return Tuple.Create(objs[0] as IEnumerable<T1>, objs[1] as IEnumerable<T2>);
        }

        public Tuple<IEnumerable<T1>> GetMultiple<T1>(string sql, DynamicParameters parameters, Func<GridReader, IEnumerable<T1>> func1)
        {
            var objs = getMultiple(sql, parameters, func1);
            return Tuple.Create(objs[0] as IEnumerable<T1>);
        }

        public DataTable GetDataTable(string spName, DynamicParameters parms)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var res = db.Query(sql: spName, param: parms, commandTimeout: 0, commandType: CommandType.StoredProcedure);
                return ToDataTable(res);
            }
        }

        public DataTable ToDataTable<dynamic>(IEnumerable<dynamic> items)
        {
            if (items == null) return null;
            var data = items.ToArray();
            if (data.Length == 0) return null;

            var dt = new DataTable();
            foreach (var pair in ((IDictionary<string, object>)data[0]))
            {
                dt.Columns.Add(pair.Key, (pair.Value ?? string.Empty).GetType());
            }
            foreach (var d in data)
            {
                dt.Rows.Add(((IDictionary<string, object>)d).Values.ToArray());
            }
            return dt;
        }
    }
}
