// This is the FacturaCoexpo Interface
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Export.Data {
    // Each item below provides an interface to a method in FacturaCoexpoServices.cs
    public interface IFacturaCoexpoService {
        Task<int> FacturaCoexpoInsert(int invoiceNo, string Proveedor, string tipoProveedor, string factura, decimal amount);
        Task<IEnumerable<FacturaCoexpo>> FacturaCoexpoList(int invoiceNo);
        Task<IEnumerable<FacturaCoexpo>> FacturaCoexpoSearch(string Param);
        Task<IEnumerable<FacturaCoexpo>> FacturaCoexpoDateRange(DateTime @StartDate, DateTime @EndDate);
        Task<FacturaCoexpo> FacturaCoexpo_GetOne(int CoexpoID);
        Task<int> FacturaCoexpoUpdate(FacturaCoexpo facturacoexpo);
        Task<bool> FacturaCoexpoDelete(int CoexpoID);
        Task<FacturaCoexpo> FacturaCoexpoGetLastRecord(int invoiceNo);
    }
}
