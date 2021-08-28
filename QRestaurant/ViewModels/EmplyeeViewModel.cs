using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QRestaurantMain.ViewModels
{
    [NotMapped]
    public class EmplyeeViewModel
    {
        [Key]
        public string userId { get; set; }

        public string userName { get; set; }

        public string perms { get; set; }
    }
}
