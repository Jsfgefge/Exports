// This is the Cargador Interface
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Export.Data {
    // Each item below provides an interface to a method in CargadorServices.cs
    public interface ICargadorService {
        Task<bool> CargadorInsert(Cargador cargador);
        Task<IEnumerable<Cargador>> CargadorList();
        Task<IEnumerable<Cargador>> CargadorSearch(string Param);
        Task<IEnumerable<Cargador>> CargadorDateRange(DateTime @StartDate, DateTime @EndDate);
        Task<Cargador> Cargador_GetOne(int CargadorID);
        Task<bool> CargadorUpdate(Cargador cargador);
        Task<bool> CargadorDelete(int CargadorID);
    }
}
