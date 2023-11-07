// This is the DetalleProducto Interface
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Export.Data {
    // Each item below provides an interface to a method in DetalleProductoServices.cs
    public interface IDetalleProductoService {
        Task<bool> DetalleProductoInsert(DetalleProducto detalleproducto);
        Task<IEnumerable<DetalleProducto>> DetalleProductoList();
        Task<IEnumerable<DetalleProducto>> DetalleProductoSearch(string Param);
        Task<IEnumerable<DetalleProducto>> DetalleProductoDateRange(DateTime @StartDate, DateTime @EndDate);
        Task<DetalleProducto> DetalleProducto_GetOne(int ProductoID);
        Task<bool> DetalleProductoUpdate(DetalleProducto detalleproducto);
        Task<bool> DetalleProductoDelete(int ProductoID);
    }
}
