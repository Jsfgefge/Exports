// This is the service for the PolizaImportacion class.
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Export.Data {
    public class PolizaImportacionService : IPolizaImportacionService {
        // Database connection
        private readonly SqlConnectionConfiguration _configuration;
        public PolizaImportacionService(SqlConnectionConfiguration configuration) {
            _configuration = configuration;
        }
        // Add (create) a PolizaImportacion table row (SQL Insert)
        // This only works if you're already created the stored procedure.
        public async Task<bool> PolizaImportacionInsert(PolizaImportacion polizaimportacion) {
            using (var conn = new SqlConnection(_configuration.Value)) {
                var parameters = new DynamicParameters();
                parameters.Add("InvoiceNo", polizaimportacion.InvoiceNo, DbType.Int64);
                parameters.Add("PolizaNo", polizaimportacion.PolizaNo, DbType.Int64);
                parameters.Add("CountryID", polizaimportacion.CountryID, DbType.Int64);
                parameters.Add("Quantity", polizaimportacion.Quantity, DbType.Decimal);
                parameters.Add("Amount", polizaimportacion.Amount, DbType.Decimal);
                parameters.Add("Imp", polizaimportacion.Imp, DbType.Int64);
                parameters.Add("Linea", polizaimportacion.Linea, DbType.Int64);
                parameters.Add("Date", polizaimportacion.Date, DbType.DateTime);

                // Stored procedure method
                await conn.ExecuteAsync("spPolizaImportacion_Insert", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }
        // Get a list of polizaimportacion rows (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<IEnumerable<PolizaImportacion>> PolizaImportacionList(int invoiceNo) {
            var parameters = new DynamicParameters();
            parameters.Add("@invoiceNo", invoiceNo, DbType.Int32);
            IEnumerable<PolizaImportacion> polizaimportacions;
            using (var conn = new SqlConnection(_configuration.Value)) {
                polizaimportacions = await conn.QueryAsync<PolizaImportacion>("spPolizaImportacion_List", parameters, commandType: CommandType.StoredProcedure);
            }
            return polizaimportacions;
        }
        //Search for data (very generic...you may need to adjust.
        public async Task<IEnumerable<PolizaImportacion>> PolizaImportacionSearch(string @Param) {
            var parameters = new DynamicParameters();
            parameters.Add("@Param", Param, DbType.String);
            IEnumerable<PolizaImportacion> polizaimportacions;
            using (var conn = new SqlConnection(_configuration.Value)) {
                polizaimportacions = await conn.QueryAsync<PolizaImportacion>("spPolizaImportacion_Search", parameters, commandType: CommandType.StoredProcedure);
            }
            return polizaimportacions;
        }
        // Search based on date range. Code generator makes wild guess, you 
        // will likely need to adjust field names
        public async Task<IEnumerable<PolizaImportacion>> PolizaImportacionDateRange(DateTime @StartDate, DateTime @EndDate) {
            var parameters = new DynamicParameters();
            parameters.Add("@StartDate", StartDate, DbType.Date);
            parameters.Add("@EndDate", EndDate, DbType.Date);
            IEnumerable<PolizaImportacion> polizaimportacions;
            using (var conn = new SqlConnection(_configuration.Value)) {
                polizaimportacions = await conn.QueryAsync<PolizaImportacion>("spPolizaImportacion_DateRange", parameters, commandType: CommandType.StoredProcedure);
            }
            return polizaimportacions;
        }

        // Get one polizaimportacion based on its PolizaImportacionID (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<PolizaImportacion> PolizaImportacion_GetOne(int @PolizaID) {
            PolizaImportacion polizaimportacion = new PolizaImportacion();
            var parameters = new DynamicParameters();
            parameters.Add("@PolizaID", PolizaID, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value)) {
                polizaimportacion = await conn.QueryFirstOrDefaultAsync<PolizaImportacion>("spPolizaImportacion_GetOne", parameters, commandType: CommandType.StoredProcedure);
            }
            return polizaimportacion;
        }
        // Update one PolizaImportacion row based on its PolizaImportacionID (SQL Update)
        // This only works if you're already created the stored procedure.
        public async Task<bool> PolizaImportacionUpdate(PolizaImportacion polizaimportacion) {
            using (var conn = new SqlConnection(_configuration.Value)) {
                var parameters = new DynamicParameters();
                parameters.Add("PolizaID", polizaimportacion.PolizaID, DbType.Int64);

                parameters.Add("InvoiceNo", polizaimportacion.InvoiceNo, DbType.Int64);
                parameters.Add("PolizaNo", polizaimportacion.PolizaNo, DbType.Int64);
                parameters.Add("CountryID", polizaimportacion.CountryID, DbType.Int64);
                parameters.Add("Quantity", polizaimportacion.Quantity, DbType.Decimal);
                parameters.Add("Amount", polizaimportacion.Amount, DbType.Decimal);
                parameters.Add("Imp", polizaimportacion.Imp, DbType.Int64);
                parameters.Add("Linea", polizaimportacion.Linea, DbType.Int64);
                parameters.Add("Date", polizaimportacion.Date, DbType.DateTime);

                await conn.ExecuteAsync("spPolizaImportacion_Update", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }

        // Physically delete one PolizaImportacion row based on its PolizaImportacionID (SQL Delete)
        // This only works if you're already created the stored procedure.
        public async Task<bool> PolizaImportacionDelete(int PolizaID) {
            var parameters = new DynamicParameters();
            parameters.Add("@PolizaID", PolizaID, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value)) {
                await conn.ExecuteAsync("spPolizaImportacion_Delete", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }
    }
}
