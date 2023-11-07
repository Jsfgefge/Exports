// This is the FacturaCoexpo Interface
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Export.Data {
    // Each item below provides an interface to a method in FacturaCoexpoServices.cs
    public interface IFacturaCoexpoService {
        Task<bool> FacturaCoexpoInsert(FacturaCoexpo facturacoexpo);
        Task<IEnumerable<FacturaCoexpo>> FacturaCoexpoList();
        Task<IEnumerable<FacturaCoexpo>> FacturaCoexpoSearch(string Param);
        Task<IEnumerable<FacturaCoexpo>> FacturaCoexpoDateRange(DateTime @StartDate, DateTime @EndDate);
        Task<FacturaCoexpo> FacturaCoexpo_GetOne(int CoexpoID);
        Task<bool> FacturaCoexpoUpdate(FacturaCoexpo facturacoexpo);
        Task<bool> FacturaCoexpoDelete(int CoexpoID);
    }
}
