using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QRestaurantMain.ViewModels
{
    [NotMapped]
    [Keyless]
    public class RolePermsViewModel
    {
        public bool orderGest { get; set; }

        public bool changeWorkZone { get; set; }

        public bool changeProductStatus { get; set; }

        public bool menuGest { get; set; }

        public bool employeeGest { get; set; }

        public bool statistics { get; set; }

        public bool BacklogAcess { get; set; }
    }
}
