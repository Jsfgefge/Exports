using System;
using System.ComponentModel.DataAnnotations;
// This is the model for one row in the database table. You may need to make some adjustments.
namespace Export.Data {
    public class Aduanas {
        [Required]
        public int AduanasID { get; set; }
        [Required]
        public int NombreAduana { get; set; }
        [Required]
        public int AbreviacionAduana { get; set; }

    }
}
