// This is the service for the FacturaCoexpo class.
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Export.Data {
    public class FacturaCoexpoService : IFacturaCoexpoService {
        // Database connection
        private readonly SqlConnectionConfiguration _configuration;
        public FacturaCoexpoService(SqlConnectionConfiguration configuration) {
            _configuration = configuration;
        }
        // Add (create) a FacturaCoexpo table row (SQL Insert)
        // This only works if you're already created the stored procedure.
        public async Task<bool> FacturaCoexpoInsert(FacturaCoexpo facturacoexpo) {
            using (var conn = new SqlConnection(_configuration.Value)) {
                var parameters = new DynamicParameters();
                parameters.Add("InvoiceNo", facturacoexpo.InvoiceNo, DbType.Int64);
                parameters.Add("Proveedor", facturacoexpo.Proveedor, DbType.String);
                parameters.Add("TipoProveedor", facturacoexpo.TipoProveedor, DbType.Int64);
                parameters.Add("Factura", facturacoexpo.Factura, DbType.String);
                parameters.Add("Amount", facturacoexpo.Amount, DbType.Decimal);

                // Stored procedure method
                await conn.ExecuteAsync("spFacturaCoexpo_Insert", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }
        // Get a list of facturacoexpo rows (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<IEnumerable<FacturaCoexpo>> FacturaCoexpoList() {
            IEnumerable<FacturaCoexpo> facturacoexpos;
            using (var conn = new SqlConnection(_configuration.Value)) {
                facturacoexpos = await conn.QueryAsync<FacturaCoexpo>("spFacturaCoexpo_List", commandType: CommandType.StoredProcedure);
            }
            return facturacoexpos;
        }
        //Search for data (very generic...you may need to adjust.
        public async Task<IEnumerable<FacturaCoexpo>> FacturaCoexpoSearch(string @Param) {
            var parameters = new DynamicParameters();
            parameters.Add("@Param", Param, DbType.String);
            IEnumerable<FacturaCoexpo> facturacoexpos;
            using (var conn = new SqlConnection(_configuration.Value)) {
                facturacoexpos = await conn.QueryAsync<FacturaCoexpo>("spFacturaCoexpo_Search", parameters, commandType: CommandType.StoredProcedure);
            }
            return facturacoexpos;
        }
        // Search based on date range. Code generator makes wild guess, you 
        // will likely need to adjust field names
        public async Task<IEnumerable<FacturaCoexpo>> FacturaCoexpoDateRange(DateTime @StartDate, DateTime @EndDate) {
            var parameters = new DynamicParameters();
            parameters.Add("@StartDate", StartDate, DbType.Date);
            parameters.Add("@EndDate", EndDate, DbType.Date);
            IEnumerable<FacturaCoexpo> facturacoexpos;
            using (var conn = new SqlConnection(_configuration.Value)) {
                facturacoexpos = await conn.QueryAsync<FacturaCoexpo>("spFacturaCoexpo_DateRange", parameters, commandType: CommandType.StoredProcedure);
            }
            return facturacoexpos;
        }

        // Get one facturacoexpo based on its FacturaCoexpoID (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<FacturaCoexpo> FacturaCoexpo_GetOne(int @CoexpoID) {
            FacturaCoexpo facturacoexpo = new FacturaCoexpo();
            var parameters = new DynamicParameters();
            parameters.Add("@CoexpoID", CoexpoID, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value)) {
                facturacoexpo = await conn.QueryFirstOrDefaultAsync<FacturaCoexpo>("spFacturaCoexpo_GetOne", parameters, commandType: CommandType.StoredProcedure);
            }
            return facturacoexpo;
        }
        // Update one FacturaCoexpo row based on its FacturaCoexpoID (SQL Update)
        // This only works if you're already created the stored procedure.
        public async Task<bool> FacturaCoexpoUpdate(FacturaCoexpo facturacoexpo) {
            using (var conn = new SqlConnection(_configuration.Value)) {
                var parameters = new DynamicParameters();
                parameters.Add("CoexpoID", facturacoexpo.CoexpoID, DbType.Int64);

                parameters.Add("InvoiceNo", facturacoexpo.InvoiceNo, DbType.Int64);
                parameters.Add("Proveedor", facturacoexpo.Proveedor, DbType.String);
                parameters.Add("TipoProveedor", facturacoexpo.TipoProveedor, DbType.Int64);
                parameters.Add("Factura", facturacoexpo.Factura, DbType.String);
                parameters.Add("Amount", facturacoexpo.Amount, DbType.Decimal);

                await conn.ExecuteAsync("spFacturaCoexpo_Update", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }

        // Physically delete one FacturaCoexpo row based on its FacturaCoexpoID (SQL Delete)
        // This only works if you're already created the stored procedure.
        public async Task<bool> FacturaCoexpoDelete(int CoexpoID) {
            var parameters = new DynamicParameters();
            parameters.Add("@CoexpoID", CoexpoID, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value)) {
                await conn.ExecuteAsync("spFacturaCoexpo_Delete", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }
    }
}
