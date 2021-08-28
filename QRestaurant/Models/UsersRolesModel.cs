using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QRestaurantMain.Models
{
    public class UsersRolesModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Required]
        public string RoleName { get; set; }

        [Required]
        public string Perms { get; set; }

        [Required]
        public string CompanyId { get; set; }

        [Required]
        public string Zone { get; set; }
    }
}
