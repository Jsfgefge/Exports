// This is the service for the ExportHeader class.
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Export.Data
{
    public class ExportHeaderService : IExportHeaderService
    {
        // Database connection
        private readonly SqlConnectionConfiguration _configuration;
        public ExportHeaderService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }
        // Add (create) a ExportHeader table row (SQL Insert)
        // This only works if you're already created the stored procedure.
        public async Task<bool> ExportHeaderInsert(ExportHeader exportheader)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("InvoiceNo", exportheader.InvoiceNo, DbType.Int64);
                parameters.Add("InvoiceDate", exportheader.InvoiceDate, DbType.Date);
                parameters.Add("SerialNo", exportheader.SerialNo, DbType.String);
                parameters.Add("ConsignatarioID", exportheader.ConsignatarioID, DbType.Int64);
                parameters.Add("AduanasID", exportheader.AduanasID, DbType.Int64);
                parameters.Add("CountryID", exportheader.CountryID, DbType.Int64);
                parameters.Add("CargadorID", exportheader.CargadorID, DbType.Int64);
                parameters.Add("BoardingDate", exportheader.BoardingDate, DbType.Date);
                parameters.Add("ExchangeRate", exportheader.ExchangeRate, DbType.Decimal);
                parameters.Add("Description", exportheader.Description, DbType.String);
                parameters.Add("DocTypeID", exportheader.DocTypeID, DbType.Int64);
                parameters.Add("DuaSimplificada", exportheader.DuaSimplificada, DbType.Int32);
                parameters.Add("Complementaria", exportheader.Complementaria, DbType.String);
                parameters.Add("IncotermID", exportheader.IncotermID, DbType.Int32);
                parameters.Add("Closed", exportheader.Closed, DbType.Boolean);
                parameters.Add("HandlerID", exportheader.HandlerID, DbType.Int64);
                parameters.Add("SupervisorID", exportheader.SupervisorID, DbType.Int64);
                parameters.Add("AnulledDate", exportheader.AnulledDate, DbType.Date);
                parameters.Add("PrimaImportada", exportheader.PrimaImportada, DbType.Boolean);

                // Stored procedure method
                await conn.ExecuteAsync("spExportHeader_Insert", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }
        // Get a list of exportheader rows (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<IEnumerable<ExportHeader>> ExportHeaderList()
        {
            IEnumerable<ExportHeader> exportheaders;
            using (var conn = new SqlConnection(_configuration.Value))
            {
                exportheaders = await conn.QueryAsync<ExportHeader>("spExportHeader_List", commandType: CommandType.StoredProcedure);
            }
            return exportheaders;
        }
        //Search for data (very generic...you may need to adjust.
        public async Task<IEnumerable<ExportHeader>> ExportHeaderSearch(string @Param)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Param", Param, DbType.String);
            IEnumerable<ExportHeader> exportheaders;
            using (var conn = new SqlConnection(_configuration.Value))
            {
                exportheaders = await conn.QueryAsync<ExportHeader>("spExportHeader_Search", parameters, commandType: CommandType.StoredProcedure);
            }
            return exportheaders;
        }
        // Search based on date range. Code generator makes wild guess, you 
        // will likely need to adjust field names
        public async Task<IEnumerable<ExportHeader>> ExportHeaderDateRange(DateTime @StartDate, DateTime @EndDate)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@StartDate", StartDate, DbType.Date);
            parameters.Add("@EndDate", EndDate, DbType.Date);
            IEnumerable<ExportHeader> exportheaders;
            using (var conn = new SqlConnection(_configuration.Value))
            {
                exportheaders = await conn.QueryAsync<ExportHeader>("spExportHeader_DateRange", parameters, commandType: CommandType.StoredProcedure);
            }
            return exportheaders;
        }

        // Get one exportheader based on its ExportHeaderID (SQL Select)
        // This only works if you're already created the stored procedure.
        public async Task<ExportHeader> ExportHeader_GetOne(int @InvoiceNo)
        {
            ExportHeader exportheader = new ExportHeader();
            var parameters = new DynamicParameters();
            parameters.Add("@invoiceNo", InvoiceNo, DbType.Int32);
            using (var conn = new SqlConnection(_configuration.Value))
            {
                exportheader = await conn.QueryFirstOrDefaultAsync<ExportHeader>("spExportHeader_GetOne", parameters, commandType: CommandType.StoredProcedure);
            }
            return exportheader;
        }
        // Update one ExportHeader row based on its ExportHeaderID (SQL Update)
        // This only works if you're already created the stored procedure.
        public async Task<int> ExportHeaderUpdate(int Headerid,
                                                  DateTime InvoiceDate,
                                                  string SerialNo,
                                                  int ConsignatarioID,
                                                  int AduanasID,
                                                  int CountryID,
                                                  int CargadorID,
                                                  DateTime BoardingDate,
                                                  decimal ExchangeRate,
                                                  string Description,
                                                  int DocTypeID,
                                                  int DuaSimplificada,
                                                  int Complementaria,
                                                  int IncotermID,
                                                  bool Closed)
        {
            int Success;
            var parameters = new DynamicParameters();
            parameters.Add("Headerid", Headerid, DbType.Int64);

            parameters.Add("InvoiceDate", InvoiceDate, DbType.Date);
            parameters.Add("SerialNo", SerialNo, DbType.String);
            parameters.Add("ConsignatarioID", ConsignatarioID, DbType.Int64);
            parameters.Add("AduanasID", AduanasID, DbType.Int64);
            parameters.Add("CountryID", CountryID, DbType.Int64);
            parameters.Add("CargadorID", CargadorID, DbType.Int64);
            parameters.Add("BoardingDate", BoardingDate, DbType.Date);
            parameters.Add("ExchangeRate", ExchangeRate, DbType.Decimal);
            parameters.Add("Description", Description, DbType.String);
            parameters.Add("DocTypeID", DocTypeID, DbType.Int64);
            parameters.Add("DuaSimplificada", DuaSimplificada, DbType.Int32);
            parameters.Add("Complementaria", Complementaria, DbType.Int32);
            parameters.Add("IncotermID", IncotermID, DbType.Int32);
            parameters.Add("Closed", Closed, DbType.Boolean);
            parameters.Add("@ResultValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            using (var conn = new SqlConnection(_configuration.Value))
            {
                await conn.ExecuteAsync("spExportHeader_Update", parameters, commandType: CommandType.StoredProcedure);
                Success = parameters.Get<int>("@ResultValue");
            }
            return Success;
        }

        // Physically delete one ExportHeader row based on its ExportHeaderID (SQL Delete)
        // This only works if you're already created the stored procedure.
        public async Task<bool> ExportHeaderDelete(int invoiceNo, string descripcion, DateTime annulDate)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@@invoiceNo", invoiceNo, DbType.Int32);
            parameters.Add("@@description", descripcion, DbType.String);
            parameters.Add("@annuledDate", annulDate, DbType.DateTime);
            using (var conn = new SqlConnection(_configuration.Value))
            {
                await conn.ExecuteAsync("spExportHeader_AnnulExport", parameters, commandType: CommandType.StoredProcedure);
            }
            return true;
        }
    }
}
