using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QRestaurantMain.Models
{
    public class LicenseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string LicenseId { get; set; }

        [Required]
        public DateTime ExpDate { get; set; }

        public ICollection<CompanyModel> company { get; set; }
    }
}
