// This is the ExportHeader Interface
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Export.Data {
    // Each item below provides an interface to a method in ExportHeaderServices.cs
    public interface IExportHeaderService {
        Task<bool> ExportHeaderInsert(ExportHeader exportheader);
        Task<IEnumerable<ExportHeader>> ExportHeaderList();
        Task<IEnumerable<ExportHeader>> ExportHeaderSearch(string Param);
        Task<IEnumerable<ExportHeader>> ExportHeaderDateRange(DateTime @StartDate, DateTime @EndDate);
        Task<ExportHeader> ExportHeader_GetOne(int InvoiceID);
        Task<bool> ExportHeaderUpdate(ExportHeader exportheader);
        Task<bool> ExportHeaderDelete(int Headerid);
    }
}
