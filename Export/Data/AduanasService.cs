// This is the service for the Aduanas class.
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Export.Data {
    public class AduanasService : IAduanasService {
        // Database connection
        private readonly SqlConnectionConfiguration _configuration;
        public AduanasService(SqlConnectionConfiguration configuration) {
            _configuration = configuration;
        }
        // Add (create) a Aduanas table row (SQL Insert)
        // This only works if you're already created the stored procedure.
        public async Task<bool> AduanasInsert(Aduanas aduanas) {
            using (var conn = new SqlConnection(_configuration.Value)) {
                var parameters = new DynamicParameters();
                parameters.Add("NombreAduana", aduanas.NombreAduana, DbType.Int64);
                parameters.Add("AbreviacionAduana", aduanas.AbreviacionAduana, DbType.Int64);

                // Stored procedure method
                await conn.ExecuteAsync("spAduanas_Insert", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }
        // Get a list of aduanas rows (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<IEnumerable<Aduanas>> AduanasList() {
            IEnumerable<Aduanas> aduanass;
            using (var conn = new SqlConnection(_configuration.Value)) {
                aduanass = await conn.QueryAsync<Aduanas>("spAduanas_List", commandType: CommandType.StoredProcedure);
            }
            return aduanass;
        }

        // Get one aduanas based on its AduanasID (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<Aduanas> Aduanas_GetOne(int @AduanasID) {
            Aduanas aduanas = new Aduanas();
            var parameters = new DynamicParameters();
            parameters.Add("@AduanasID", AduanasID, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value)) {
                aduanas = await conn.QueryFirstOrDefaultAsync<Aduanas>("spAduanas_GetOne", parameters, commandType: CommandType.StoredProcedure);
            }
            return aduanas;
        }
        // Update one Aduanas row based on its AduanasID (SQL Update)
        // This only works if you're already created the stored procedure.
        public async Task<bool> AduanasUpdate(Aduanas aduanas) {
            using (var conn = new SqlConnection(_configuration.Value)) {
                var parameters = new DynamicParameters();
                parameters.Add("AduanasID", aduanas.AduanasID, DbType.Int64);

                parameters.Add("NombreAduana", aduanas.NombreAduana, DbType.Int64);
                parameters.Add("AbreviacionAduana", aduanas.AbreviacionAduana, DbType.Int64);

                await conn.ExecuteAsync("spAduanas_Update", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }
    }
}
