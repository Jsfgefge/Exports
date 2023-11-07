// This is the PolizasCoexpo Interface
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Export.Data {
    // Each item below provides an interface to a method in PolizasCoexpoServices.cs
    public interface IPolizasCoexpoService {
        Task<bool> PolizasCoexpoInsert(PolizasCoexpo polizascoexpo);
        Task<IEnumerable<PolizasCoexpo>> PolizasCoexpoList();
        Task<IEnumerable<PolizasCoexpo>> PolizasCoexpoSearch(string Param);
        Task<IEnumerable<PolizasCoexpo>> PolizasCoexpoDateRange(DateTime @StartDate, DateTime @EndDate);
        Task<PolizasCoexpo> PolizasCoexpo_GetOne(int Id);
        Task<bool> PolizasCoexpoUpdate(PolizasCoexpo polizascoexpo);
        Task<bool> PolizasCoexpoDelete(int Id);
    }
}
