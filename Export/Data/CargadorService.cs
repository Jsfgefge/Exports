// This is the service for the Cargador class.
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        public async Task<int> CargadorInsert(string descripcion) {
            int Success = 0;
            var parameters = new DynamicParameters();
            parameters.Add("descripcion", descripcion, DbType.String);
            parameters.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            using (var conn = new SqlConnection(_configuration.Value)) {
                await conn.ExecuteAsync("spCargador_Insert", parameters, commandType: CommandType.StoredProcedure);
                Success = parameters.Get<int>("@ReturnValue");
            }
            return Success;
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
        public async Task<int> CargadorUpdate(string descripcion, int cargadorID) {
            int Success = 0;
            var parameters = new DynamicParameters();
            parameters.Add("descripcion", descripcion, DbType.String);
            parameters.Add("cargadorID", cargadorID, DbType.Int32);
            parameters.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            using (var conn = new SqlConnection(_configuration.Value)) {
                await conn.ExecuteAsync("spCargador_Update", parameters, commandType: CommandType.StoredProcedure);
                Success = parameters.Get<int>("@ReturnValue");
            }
            return Success;
        }

    }
}
