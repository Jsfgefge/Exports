using System;
using System.ComponentModel.DataAnnotations;
// This is the model for one row in the database table. You may need to make some adjustments.
namespace Export.Data {
    public class PolizaImportacion {
        [Required]
        public int PolizaID { get; set; }
        [Required]
        public int InvoiceNo { get; set; }
        [Required]
        public int PolizaNo { get; set; }
        [Required]
        public int CountryID { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public int Imp { get; set; }
        [Required]
        public int Linea { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public string countryISO { get; set; }

    }
}
