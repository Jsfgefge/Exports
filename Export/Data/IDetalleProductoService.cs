// This is the DetalleProducto Interface
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Export.Data {
    // Each item below provides an interface to a method in DetalleProductoServices.cs
    public interface IDetalleProductoService {
        Task<int> DetalleProductoInsert(int invoiceNo, 
                                        string codigoHTS, 
                                        string description, 
                                        string categoria, 
                                        double cantidad, 
                                        string medida, 
                                        decimal pricePerUnit);
        Task<IEnumerable<DetalleProducto>> DetalleProductoList(int invoiceNo);
        Task<IEnumerable<DetalleProducto>> DetalleProductoSearch(string Param);
        Task<IEnumerable<DetalleProducto>> DetalleProductoDateRange(DateTime @StartDate, DateTime @EndDate);
        Task<DetalleProducto> DetalleProducto_GetOne(int ProductoID);
        Task<bool> DetalleProductoUpdate(DetalleProducto detalleproducto);
        Task<bool> DetalleProductoDelete(int ProductoID);
        Task<DetalleProducto> DetalleProductoGetLastRecord(int invoiceNo);
    }
}
