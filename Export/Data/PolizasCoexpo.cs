using System;
using System.ComponentModel.DataAnnotations;
// This is the model for one row in the database table. You may need to make some adjustments.
namespace Export.Data {
    public class PolizasCoexpo {
        [Required]
        public int Id { get; set; }
        [Required]
        public int PolizaNo { get; set; }
        [Required]
        public int CountryID { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public int CoexpoID { get; set; }

    }
}
