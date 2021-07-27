using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QRestaurantMain.ViewModels
{
    public class CompanySelectorViewModel
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string CompanyLogo { get; set; }
    }
}
