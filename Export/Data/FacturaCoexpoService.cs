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
        public async Task<int> FacturaCoexpoInsert(int invoiceNo, string Proveedor, string tipoProveedor, string factura, decimal amount) {
            int Success = 0;

            var parameters = new DynamicParameters();
            parameters.Add("InvoiceNo", invoiceNo, DbType.Int64);
            parameters.Add("Proveedor", Proveedor, DbType.String);
            parameters.Add("TipoProveedor", tipoProveedor, DbType.String);
            parameters.Add("Factura", factura, DbType.String);
            parameters.Add("Amount", amount, DbType.Decimal);
            parameters.Add("@ResultValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            using (var conn = new SqlConnection(_configuration.Value)) {

                // Stored procedure method
                await conn.ExecuteAsync("spFacturaCoexpo_Insert", parameters, commandType: CommandType.StoredProcedure);
                Success = parameters.Get<int>("@ResultValue");
            }
            return Success;
        }
        // Get a list of facturacoexpo rows (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<IEnumerable<FacturaCoexpo>> FacturaCoexpoList(int invoiceNo) {
            var parameters = new DynamicParameters();
            parameters.Add("@invoiceNo", invoiceNo, DbType.Int32);
            IEnumerable<FacturaCoexpo> facturacoexpos;
            using (var conn = new SqlConnection(_configuration.Value)) {
                facturacoexpos = await conn.QueryAsync<FacturaCoexpo>("spFacturaCoexpo_List", parameters,commandType: CommandType.StoredProcedure);
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
        public async Task<int> FacturaCoexpoUpdate(FacturaCoexpo facturacoexpo) {
            int Success = 0;

            var parameters = new DynamicParameters();
            parameters.Add("CoexpoID", facturacoexpo.CoexpoID, DbType.Int64);

            parameters.Add("InvoiceNo", facturacoexpo.InvoiceNo, DbType.Int64);
            parameters.Add("Proveedor", facturacoexpo.Proveedor, DbType.String);
            parameters.Add("TipoProveedor", facturacoexpo.TipoProveedor, DbType.String);
            parameters.Add("Factura", facturacoexpo.Factura, DbType.String);
            parameters.Add("Amount", facturacoexpo.Amount, DbType.Decimal);
            parameters.Add("@ResultValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            using (var conn = new SqlConnection(_configuration.Value)) {

                await conn.ExecuteAsync("spFacturaCoexpo_Update", parameters, commandType: CommandType.StoredProcedure);
                Success = parameters.Get<int>("@ResultValue");
            }
            return Success;
        }

        public async Task<FacturaCoexpo> FacturaCoexpoGetLastRecord(int invoiceNo)
        {
            FacturaCoexpo facturaCoexpo = new FacturaCoexpo();
            var parameters = new DynamicParameters();
            parameters.Add("@invoiceNo", invoiceNo, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value))
            {
                facturaCoexpo = await conn.QueryFirstOrDefaultAsync<FacturaCoexpo>("spFacturaCoexpo_GetLastRecord", parameters, commandType: CommandType.StoredProcedure);
            }
            return facturaCoexpo;
        }

        // Physically delete one FacturaCoexpo row based on its FacturaCoexpoID (SQL Delete)
        // This only works if you're already created the stored procedure.
        public async Task<bool> FacturaCoexpoDelete(int CoexpoID) {
            var parameters = new DynamicParameters();
            parameters.Add("@coexpoID", CoexpoID, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value)) {
                await conn.ExecuteAsync("spfacturaCoexpo_DeleteRecord", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }
    }
}
