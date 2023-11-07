using System;
using System.ComponentModel.DataAnnotations;
// This is the model for one row in the database table. You may need to make some adjustments.
namespace Export.Data {
    public class Consignatarios {
        [Required]
        public int ConsignatarioID { get; set; }
        [Required]
        public int NombreConsignatario { get; set; }

    }
}
