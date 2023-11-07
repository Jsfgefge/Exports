// This is the PolizaImportacion Interface
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Export.Data {
    // Each item below provides an interface to a method in PolizaImportacionServices.cs
    public interface IPolizaImportacionService {
        Task<bool> PolizaImportacionInsert(PolizaImportacion polizaimportacion);
        Task<IEnumerable<PolizaImportacion>> PolizaImportacionList();
        Task<IEnumerable<PolizaImportacion>> PolizaImportacionSearch(string Param);
        Task<IEnumerable<PolizaImportacion>> PolizaImportacionDateRange(DateTime @StartDate, DateTime @EndDate);
        Task<PolizaImportacion> PolizaImportacion_GetOne(int PolizaID);
        Task<bool> PolizaImportacionUpdate(PolizaImportacion polizaimportacion);
        Task<bool> PolizaImportacionDelete(int PolizaID);
    }
}