using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QRestaurantMain.Models
{
    public class CompanyModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Required]
        public string LicenseId { get; set; }

        [Required]
        public string OwnerId { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string CompanyLogoUrl { get; set; }
    }
}
