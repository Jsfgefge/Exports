using System;
using System.ComponentModel.DataAnnotations;
// This is the model for one row in the database table. You may need to make some adjustments.
namespace Export.Data {
    public class FacturaCoexpo {
        [Required]
        public int CoexpoID { get; set; }
        [Required]
        public int InvoiceNo { get; set; }
        [Required]
        [StringLength(255)]
        public string Proveedor { get; set; }
        [Required]
        public int TipoProveedor { get; set; }
        [Required]
        [StringLength(255)]
        public string Factura { get; set; }
        [Required]
        public decimal Amount { get; set; }

    }
}