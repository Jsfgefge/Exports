using System;
using System.ComponentModel.DataAnnotations;
// This is the model for one row in the database table. You may need to make some adjustments.
namespace Export.Data {
    public class Cargador {
        [Required]
        public int CargadorID { get; set; }
        [Required]
        [StringLength(255)]
        public string Descripcion { get; set; }

    }
}
