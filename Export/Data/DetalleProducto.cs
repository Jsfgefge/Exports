using System;
using System.ComponentModel.DataAnnotations;
// This is the model for one row in the database table. You may need to make some adjustments.
namespace Export.Data {
    public class DetalleProducto {
        [Required]
        public int ProductoID { get; set; }
        [Required]
        public int InvoiceNo { get; set; }
        [Required]
        [StringLength(255)]
        public string CodigoHTS { get; set; }
        [Required]
        [StringLength(255)]
        public string Description { get; set; }
        [Required]
        [StringLength(255)]
        public string Categoria { get; set; }
        [Required]
        public double Cantidad { get; set; }
        [Required]
        public string Medida { get; set; }
        [Required]
        public decimal PricePerUnit { get; set; }

    }
}