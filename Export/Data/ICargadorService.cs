// This is the Cargador Interface
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Export.Data {
    // Each item below provides an interface to a method in CargadorServices.cs
    public interface ICargadorService {
        Task<int> CargadorInsert(string descripcion);
        Task<IEnumerable<Cargador>> CargadorList();
        Task<Cargador> Cargador_GetOne(int CargadorID);
        Task<int> CargadorUpdate(string descripcion, int cargadorID);
    }
}
