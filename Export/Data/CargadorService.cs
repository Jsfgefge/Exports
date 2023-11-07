// This is the service for the Cargador class.
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Export.Data {
    public class CargadorService : ICargadorService {
        // Database connection
        private readonly SqlConnectionConfiguration _configuration;
        public CargadorService(SqlConnectionConfiguration configuration) {
            _configuration = configuration;
        }
        // Add (create) a Cargador table row (SQL Insert)
        // This only works if you're already created the stored procedure.
        public async Task<bool> CargadorInsert(Cargador cargador) {
            using (var conn = new SqlConnection(_configuration.Value)) {
                var parameters = new DynamicParameters();
                parameters.Add("Descripcion", cargador.Descripcion, DbType.String);

                // Stored procedure method
                await conn.ExecuteAsync("spCargador_Insert", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }
        // Get a list of cargador rows (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<IEnumerable<Cargador>> CargadorList() {
            IEnumerable<Cargador> cargadors;
            using (var conn = new SqlConnection(_configuration.Value)) {
                cargadors = await conn.QueryAsync<Cargador>("spCargador_List", commandType: CommandType.StoredProcedure);
            }
            return cargadors;
        }
        //Search for data (very generic...you may need to adjust.
        public async Task<IEnumerable<Cargador>> CargadorSearch(string @Param) {
            var parameters = new DynamicParameters();
            parameters.Add("@Param", Param, DbType.String);
            IEnumerable<Cargador> cargadors;
            using (var conn = new SqlConnection(_configuration.Value)) {
                cargadors = await conn.QueryAsync<Cargador>("spCargador_Search", parameters, commandType: CommandType.StoredProcedure);
            }
            return cargadors;
        }
        // Search based on date range. Code generator makes wild guess, you 
        // will likely need to adjust field names
        public async Task<IEnumerable<Cargador>> CargadorDateRange(DateTime @StartDate, DateTime @EndDate) {
            var parameters = new DynamicParameters();
            parameters.Add("@StartDate", StartDate, DbType.Date);
            parameters.Add("@EndDate", EndDate, DbType.Date);
            IEnumerable<Cargador> cargadors;
            using (var conn = new SqlConnection(_configuration.Value)) {
                cargadors = await conn.QueryAsync<Cargador>("spCargador_DateRange", parameters, commandType: CommandType.StoredProcedure);
            }
            return cargadors;
        }

        // Get one cargador based on its CargadorID (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<Cargador> Cargador_GetOne(int @CargadorID) {
            Cargador cargador = new Cargador();
            var parameters = new DynamicParameters();
            parameters.Add("@CargadorID", CargadorID, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value)) {
                cargador = await conn.QueryFirstOrDefaultAsync<Cargador>("spCargador_GetOne", parameters, commandType: CommandType.StoredProcedure);
            }
            return cargador;
        }
        // Update one Cargador row based on its CargadorID (SQL Update)
        // This only works if you're already created the stored procedure.
        public async Task<bool> CargadorUpdate(Cargador cargador) {
            using (var conn = new SqlConnection(_configuration.Value)) {
                var parameters = new DynamicParameters();
                parameters.Add("CargadorID", cargador.CargadorID, DbType.Int64);

                parameters.Add("Descripcion", cargador.Descripcion, DbType.String);

                await conn.ExecuteAsync("spCargador_Update", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }

        // Physically delete one Cargador row based on its CargadorID (SQL Delete)
        // This only works if you're already created the stored procedure.
        public async Task<bool> CargadorDelete(int CargadorID) {
            var parameters = new DynamicParameters();
            parameters.Add("@CargadorID", CargadorID, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value)) {
                await conn.ExecuteAsync("spCargador_Delete", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }
    }
}
