// This is the service for the PolizasCoexpo class.
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Export.Data {
    public class PolizasCoexpoService : IPolizasCoexpoService {
        // Database connection
        private readonly SqlConnectionConfiguration _configuration;
        public PolizasCoexpoService(SqlConnectionConfiguration configuration) {
            _configuration = configuration;
        }
        // Add (create) a PolizasCoexpo table row (SQL Insert)
        // This only works if you're already created the stored procedure.
        public async Task<bool> PolizasCoexpoInsert(PolizasCoexpo polizascoexpo) {
            using (var conn = new SqlConnection(_configuration.Value)) {
                var parameters = new DynamicParameters();
                parameters.Add("PolizaNo", polizascoexpo.PolizaNo, DbType.Int64);
                parameters.Add("CountryID", polizascoexpo.CountryID, DbType.Int64);
                parameters.Add("Amount", polizascoexpo.Amount, DbType.Decimal);
                parameters.Add("CoexpoID", polizascoexpo.CoexpoID, DbType.Int64);

                // Stored procedure method
                await conn.ExecuteAsync("spPolizasCoexpo_Insert", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }
        // Get a list of polizascoexpo rows (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<IEnumerable<PolizasCoexpo>> PolizasCoexpoList() {
            IEnumerable<PolizasCoexpo> polizascoexpos;
            using (var conn = new SqlConnection(_configuration.Value)) {
                polizascoexpos = await conn.QueryAsync<PolizasCoexpo>("spPolizasCoexpo_List", commandType: CommandType.StoredProcedure);
            }
            return polizascoexpos;
        }
        //Search for data (very generic...you may need to adjust.
        public async Task<IEnumerable<PolizasCoexpo>> PolizasCoexpoSearch(string @Param) {
            var parameters = new DynamicParameters();
            parameters.Add("@Param", Param, DbType.String);
            IEnumerable<PolizasCoexpo> polizascoexpos;
            using (var conn = new SqlConnection(_configuration.Value)) {
                polizascoexpos = await conn.QueryAsync<PolizasCoexpo>("spPolizasCoexpo_Search", parameters, commandType: CommandType.StoredProcedure);
            }
            return polizascoexpos;
        }
        // Search based on date range. Code generator makes wild guess, you 
        // will likely need to adjust field names
        public async Task<IEnumerable<PolizasCoexpo>> PolizasCoexpoDateRange(DateTime @StartDate, DateTime @EndDate) {
            var parameters = new DynamicParameters();
            parameters.Add("@StartDate", StartDate, DbType.Date);
            parameters.Add("@EndDate", EndDate, DbType.Date);
            IEnumerable<PolizasCoexpo> polizascoexpos;
            using (var conn = new SqlConnection(_configuration.Value)) {
                polizascoexpos = await conn.QueryAsync<PolizasCoexpo>("spPolizasCoexpo_DateRange", parameters, commandType: CommandType.StoredProcedure);
            }
            return polizascoexpos;
        }

        // Get one polizascoexpo based on its PolizasCoexpoID (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<PolizasCoexpo> PolizasCoexpo_GetOne(int @Id) {
            PolizasCoexpo polizascoexpo = new PolizasCoexpo();
            var parameters = new DynamicParameters();
            parameters.Add("@Id", Id, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value)) {
                polizascoexpo = await conn.QueryFirstOrDefaultAsync<PolizasCoexpo>("spPolizasCoexpo_GetOne", parameters, commandType: CommandType.StoredProcedure);
            }
            return polizascoexpo;
        }
        // Update one PolizasCoexpo row based on its PolizasCoexpoID (SQL Update)
        // This only works if you're already created the stored procedure.
        public async Task<bool> PolizasCoexpoUpdate(PolizasCoexpo polizascoexpo) {
            using (var conn = new SqlConnection(_configuration.Value)) {
                var parameters = new DynamicParameters();
                parameters.Add("Id", polizascoexpo.Id, DbType.Int64);

                parameters.Add("PolizaNo", polizascoexpo.PolizaNo, DbType.Int64);
                parameters.Add("CountryID", polizascoexpo.CountryID, DbType.Int64);
                parameters.Add("Amount", polizascoexpo.Amount, DbType.Decimal);
                parameters.Add("CoexpoID", polizascoexpo.CoexpoID, DbType.Int64);

                await conn.ExecuteAsync("spPolizasCoexpo_Update", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }

        // Physically delete one PolizasCoexpo row based on its PolizasCoexpoID (SQL Delete)
        // This only works if you're already created the stored procedure.
        public async Task<bool> PolizasCoexpoDelete(int Id) {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", Id, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value)) {
                await conn.ExecuteAsync("spPolizasCoexpo_Delete", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }
    }
}
