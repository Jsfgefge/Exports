// This is the service for the Country class.
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Export.Data {
    public class CountryService : ICountryService {
        // Database connection
        private readonly SqlConnectionConfiguration _configuration;
        public CountryService(SqlConnectionConfiguration configuration) {
            _configuration = configuration;
        }
        // Add (create) a Country table row (SQL Insert)
        // This only works if you're already created the stored procedure.
        public async Task<int> CountryInsert(string countryName, string countryISO) {
            int Success = 0;
            var parameters = new DynamicParameters();
            parameters.Add("countryName", countryName, DbType.String);
            parameters.Add("countryISO", countryISO, DbType.String);
            parameters.Add("@ResultValue", dbType:DbType.Int32, direction:ParameterDirection.ReturnValue);
            using (var conn = new SqlConnection(_configuration.Value)) {
                await conn.ExecuteAsync("spCountry_Insert", parameters, commandType: CommandType.StoredProcedure);
                Success = parameters.Get<int>("@ResultValue");
            }
            return Success;
        }
        // Get a list of country rows (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<IEnumerable<Country>> CountryList() {
            IEnumerable<Country> countries;
            using (var conn = new SqlConnection(_configuration.Value)) {
                countries = await conn.QueryAsync<Country>("spCountry_List", commandType: CommandType.StoredProcedure);
            }
            return countries;
        }

        // Get one country based on its CountryID (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<Country> Country_GetOne(int @CountryID) {
            Country country = new Country();
            var parameters = new DynamicParameters();
            parameters.Add("@CountryID", CountryID, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value)) {
                country = await conn.QueryFirstOrDefaultAsync<Country>("spCountry_GetOne", parameters, commandType: CommandType.StoredProcedure);
            }
            return country;
        }
        // Update one Country row based on its CountryID (SQL Update)
        // This only works if you're already created the stored procedure.
        public async Task<int> CountryUpdate(string countryName, string countryISO, int countryID) {
            int Success = 0;
            var parameters = new DynamicParameters();
            parameters.Add("countryName", countryName, DbType.String);
            parameters.Add("countryISO", countryISO, DbType.String);
            parameters.Add("countryID", countryID, DbType.Int32);
            parameters.Add("@ResultValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            using (var conn = new SqlConnection(_configuration.Value)) {
                await conn.ExecuteAsync("spCountry_Update", parameters, commandType:CommandType.StoredProcedure);
                Success = parameters.Get<int>("@ResultValue");
            }
            return Success;
        }
    }
}
