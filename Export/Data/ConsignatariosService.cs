// This is the service for the Consignatarios class.
using Dapper;
using Microsoft.Data.SqlClient;
using Syncfusion.Blazor.Inputs;
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
        public async Task<int> ConsignatariosInsert(string consignatarioName) {
            int Success = 0;
            var parameters = new DynamicParameters();
            parameters.Add("nombreConsignatario", consignatarioName, DbType.String);
            parameters.Add("@ReturnValue", dbType:DbType.Int32, direction:ParameterDirection.ReturnValue);
            using (var conn = new SqlConnection(_configuration.Value)) {
                await conn.ExecuteAsync("spConsignatarios_Insert", parameters, commandType: CommandType.StoredProcedure);
                Success = parameters.Get<int>("@ReturnValue");
            }
            return Success;
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
        public async Task<int> ConsignatariosUpdate(string nombreConsignatario, int consignatarioID) {
            int Success = 0;
            var parameters = new DynamicParameters();
            parameters.Add("nombreConsignatario", nombreConsignatario, DbType.String);
            parameters.Add("consignatarioID", consignatarioID, DbType.Int32);
            parameters.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            using (var conn = new SqlConnection(_configuration.Value)) {
                await conn.ExecuteAsync("spConsignatarios_Update", parameters, commandType: CommandType.StoredProcedure);
                Success = parameters.Get<int>("@ReturnValue");
            }
            return Success;
        }
    }
}
