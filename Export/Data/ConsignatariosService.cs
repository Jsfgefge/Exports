// This is the service for the Consignatarios class.
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Export.Data {
    public class ConsignatariosService : IConsignatariosService {
        // Database connection
        private readonly SqlConnectionConfiguration _configuration;
        public ConsignatariosService(SqlConnectionConfiguration configuration) {
            _configuration = configuration;
        }
        // Add (create) a Consignatarios table row (SQL Insert)
        // This only works if you're already created the stored procedure.
        public async Task<bool> ConsignatariosInsert(Consignatarios consignatarios) {
            using (var conn = new SqlConnection(_configuration.Value)) {
                var parameters = new DynamicParameters();
                parameters.Add("NombreConsignatario", consignatarios.NombreConsignatario, DbType.Int64);

                // Stored procedure method
                await conn.ExecuteAsync("spConsignatarios_Insert", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }
        // Get a list of consignatarios rows (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<IEnumerable<Consignatarios>> ConsignatariosList() {
            IEnumerable<Consignatarios> consignatarioss;
            using (var conn = new SqlConnection(_configuration.Value)) {
                consignatarioss = await conn.QueryAsync<Consignatarios>("spConsignatarios_List", commandType: CommandType.StoredProcedure);
            }
            return consignatarioss;
        }
        //Search for data (very generic...you may need to adjust.
        public async Task<IEnumerable<Consignatarios>> ConsignatariosSearch(string @Param) {
            var parameters = new DynamicParameters();
            parameters.Add("@Param", Param, DbType.String);
            IEnumerable<Consignatarios> consignatarioss;
            using (var conn = new SqlConnection(_configuration.Value)) {
                consignatarioss = await conn.QueryAsync<Consignatarios>("spConsignatarios_Search", parameters, commandType: CommandType.StoredProcedure);
            }
            return consignatarioss;
        }
        // Search based on date range. Code generator makes wild guess, you 
        // will likely need to adjust field names
        public async Task<IEnumerable<Consignatarios>> ConsignatariosDateRange(DateTime @StartDate, DateTime @EndDate) {
            var parameters = new DynamicParameters();
            parameters.Add("@StartDate", StartDate, DbType.Date);
            parameters.Add("@EndDate", EndDate, DbType.Date);
            IEnumerable<Consignatarios> consignatarioss;
            using (var conn = new SqlConnection(_configuration.Value)) {
                consignatarioss = await conn.QueryAsync<Consignatarios>("spConsignatarios_DateRange", parameters, commandType: CommandType.StoredProcedure);
            }
            return consignatarioss;
        }

        // Get one consignatarios based on its ConsignatariosID (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<Consignatarios> Consignatarios_GetOne(int @ConsignatarioID) {
            Consignatarios consignatarios = new Consignatarios();
            var parameters = new DynamicParameters();
            parameters.Add("@ConsignatarioID", ConsignatarioID, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value)) {
                consignatarios = await conn.QueryFirstOrDefaultAsync<Consignatarios>("spConsignatarios_GetOne", parameters, commandType: CommandType.StoredProcedure);
            }
            return consignatarios;
        }
        // Update one Consignatarios row based on its ConsignatariosID (SQL Update)
        // This only works if you're already created the stored procedure.
        public async Task<bool> ConsignatariosUpdate(Consignatarios consignatarios) {
            using (var conn = new SqlConnection(_configuration.Value)) {
                var parameters = new DynamicParameters();
                parameters.Add("ConsignatarioID", consignatarios.ConsignatarioID, DbType.Int64);

                parameters.Add("NombreConsignatario", consignatarios.NombreConsignatario, DbType.Int64);

                await conn.ExecuteAsync("spConsignatarios_Update", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }

        // Physically delete one Consignatarios row based on its ConsignatariosID (SQL Delete)
        // This only works if you're already created the stored procedure.
        public async Task<bool> ConsignatariosDelete(int ConsignatarioID) {
            var parameters = new DynamicParameters();
            parameters.Add("@ConsignatarioID", ConsignatarioID, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value)) {
                await conn.ExecuteAsync("spConsignatarios_Delete", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }
    }
}
