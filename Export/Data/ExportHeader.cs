using System;
using System.ComponentModel.DataAnnotations;
// This is the model for one row in the database table. You may need to make some adjustments.
namespace Export.Data {
    public class ExportHeader {
        [Required]
        public int Headerid { get; set; }
        [Required]
        public int InvoiceNo { get; set; }
        [Required]
        public DateTime InvoiceDate { get; set; }
        [StringLength(255)]
        public string SerialNo { get; set; }
        [Required]
        public int ConsignatarioID { get; set; }
        [Required]
        public int AduanasID { get; set; }
        [Required]
        public int CountryID { get; set; }
        [Required]
        public int CargadorID { get; set; }
        [Required]
        public DateTime BoardingDate { get; set; }
        [Required]
        public decimal ExchangeRate { get; set; }
        [Required]
        [StringLength(255)]
        public string Description { get; set; }
        [Required]
        public int DocTypeID { get; set; }
        public int DuaSimplificada { get; set; }
        [StringLength(255)]
        public string Complementaria { get; set; }
        [Required]
        public int IncotermID { get; set; }
        [Required]
        public bool Closed { get; set; }
        public int HandlerID { get; set; }
        public int SupervisorID { get; set; }
        public int AnulledDate { get; set; }
        [Required]
        public bool PrimaImportada { get; set; }

    }
}
