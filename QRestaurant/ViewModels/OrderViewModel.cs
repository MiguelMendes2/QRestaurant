using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QRestaurantMain.ViewModels
{
    public class OrderViewModel
    {
        [Key]
        public string ProductId { get; set; }

        [Required]
        public Boolean Delivered { get; set; }
    }
}
