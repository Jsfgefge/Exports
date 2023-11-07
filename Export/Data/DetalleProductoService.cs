// This is the service for the DetalleProducto class.
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Export.Data {
    public class DetalleProductoService : IDetalleProductoService {
        // Database connection
        private readonly SqlConnectionConfiguration _configuration;
        public DetalleProductoService(SqlConnectionConfiguration configuration) {
            _configuration = configuration;
        }
        // Add (create) a DetalleProducto table row (SQL Insert)
        // This only works if you're already created the stored procedure.
        public async Task<bool> DetalleProductoInsert(DetalleProducto detalleproducto) {
            using (var conn = new SqlConnection(_configuration.Value)) {
                var parameters = new DynamicParameters();
                parameters.Add("InvoiceNo", detalleproducto.InvoiceNo, DbType.Int64);
                parameters.Add("CodigoHTS", detalleproducto.CodigoHTS, DbType.String);
                parameters.Add("Description", detalleproducto.Description, DbType.String);
                parameters.Add("Categoria", detalleproducto.Categoria, DbType.String);
                parameters.Add("Cantidad", detalleproducto.Cantidad, DbType.Int64);
                parameters.Add("Medida", detalleproducto.Medida, DbType.Int64);
                parameters.Add("PricePerUnit", detalleproducto.PricePerUnit, DbType.Int64);

                // Stored procedure method
                await conn.ExecuteAsync("spDetalleProducto_Insert", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }
        // Get a list of detalleproducto rows (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<IEnumerable<DetalleProducto>> DetalleProductoList() {
            IEnumerable<DetalleProducto> detalleproductos;
            using (var conn = new SqlConnection(_configuration.Value)) {
                detalleproductos = await conn.QueryAsync<DetalleProducto>("spDetalleProducto_List", commandType: CommandType.StoredProcedure);
            }
            return detalleproductos;
        }
        //Search for data (very generic...you may need to adjust.
        public async Task<IEnumerable<DetalleProducto>> DetalleProductoSearch(string @Param) {
            var parameters = new DynamicParameters();
            parameters.Add("@Param", Param, DbType.String);
            IEnumerable<DetalleProducto> detalleproductos;
            using (var conn = new SqlConnection(_configuration.Value)) {
                detalleproductos = await conn.QueryAsync<DetalleProducto>("spDetalleProducto_Search", parameters, commandType: CommandType.StoredProcedure);
            }
            return detalleproductos;
        }
        // Search based on date range. Code generator makes wild guess, you 
        // will likely need to adjust field names
        public async Task<IEnumerable<DetalleProducto>> DetalleProductoDateRange(DateTime @StartDate, DateTime @EndDate) {
            var parameters = new DynamicParameters();
            parameters.Add("@StartDate", StartDate, DbType.Date);
            parameters.Add("@EndDate", EndDate, DbType.Date);
            IEnumerable<DetalleProducto> detalleproductos;
            using (var conn = new SqlConnection(_configuration.Value)) {
                detalleproductos = await conn.QueryAsync<DetalleProducto>("spDetalleProducto_DateRange", parameters, commandType: CommandType.StoredProcedure);
            }
            return detalleproductos;
        }

        // Get one detalleproducto based on its DetalleProductoID (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<DetalleProducto> DetalleProducto_GetOne(int @ProductoID) {
            DetalleProducto detalleproducto = new DetalleProducto();
            var parameters = new DynamicParameters();
            parameters.Add("@ProductoID", ProductoID, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value)) {
                detalleproducto = await conn.QueryFirstOrDefaultAsync<DetalleProducto>("spDetalleProducto_GetOne", parameters, commandType: CommandType.StoredProcedure);
            }
            return detalleproducto;
        }
        // Update one DetalleProducto row based on its DetalleProductoID (SQL Update)
        // This only works if you're already created the stored procedure.
        public async Task<bool> DetalleProductoUpdate(DetalleProducto detalleproducto) {
            using (var conn = new SqlConnection(_configuration.Value)) {
                var parameters = new DynamicParameters();
                parameters.Add("ProductoID", detalleproducto.ProductoID, DbType.Int64);

                parameters.Add("InvoiceNo", detalleproducto.InvoiceNo, DbType.Int64);
                parameters.Add("CodigoHTS", detalleproducto.CodigoHTS, DbType.String);
                parameters.Add("Description", detalleproducto.Description, DbType.String);
                parameters.Add("Categoria", detalleproducto.Categoria, DbType.String);
                parameters.Add("Cantidad", detalleproducto.Cantidad, DbType.Int64);
                parameters.Add("Medida", detalleproducto.Medida, DbType.Int64);
                parameters.Add("PricePerUnit", detalleproducto.PricePerUnit, DbType.Int64);

                await conn.ExecuteAsync("spDetalleProducto_Update", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }

        // Physically delete one DetalleProducto row based on its DetalleProductoID (SQL Delete)
        // This only works if you're already created the stored procedure.
        public async Task<bool> DetalleProductoDelete(int ProductoID) {
            var parameters = new DynamicParameters();
            parameters.Add("@ProductoID", ProductoID, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value)) {
                await conn.ExecuteAsync("spDetalleProducto_Delete", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }
    }
}
