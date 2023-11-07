// This is the Consignatarios Interface
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Export.Data {
    // Each item below provides an interface to a method in ConsignatariosServices.cs
    public interface IConsignatariosService {
        Task<bool> ConsignatariosInsert(Consignatarios consignatarios);
        Task<IEnumerable<Consignatarios>> ConsignatariosList();
        Task<IEnumerable<Consignatarios>> ConsignatariosSearch(string Param);
        Task<IEnumerable<Consignatarios>> ConsignatariosDateRange(DateTime @StartDate, DateTime @EndDate);
        Task<Consignatarios> Consignatarios_GetOne(int ConsignatarioID);
        Task<bool> ConsignatariosUpdate(Consignatarios consignatarios);
        Task<bool> ConsignatariosDelete(int ConsignatarioID);
    }
}
