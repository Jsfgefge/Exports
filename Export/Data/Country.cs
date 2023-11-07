using System;
using System.ComponentModel.DataAnnotations;
// This is the model for one row in the database table. You may need to make some adjustments.
namespace Export.Data {
    public class Country {
        [Required]
        public int CountryID { get; set; }
        [Required]
        public string CountryName { get; set; }
        [Required]
        public string CountryISO { get; set; }

    }
}
