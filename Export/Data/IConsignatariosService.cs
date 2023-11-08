// This is the Consignatarios Interface
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Export.Data {
    // Each item below provides an interface to a method in ConsignatariosServices.cs
    public interface IConsignatariosService {
        Task<int> ConsignatariosInsert(string consignatarioName);
        Task<IEnumerable<Consignatarios>> ConsignatariosList();
        Task<Consignatarios> Consignatarios_GetOne(int ConsignatarioID);
        Task<int> ConsignatariosUpdate(string nombreConsignatario, int consignatarioID);
    }
}
