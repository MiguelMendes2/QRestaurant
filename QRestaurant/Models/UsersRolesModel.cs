using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace QRestaurantMain.Models
{
    public class UsersRolesModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UsersRolesId {  get; set; }

        [Required]
        [ForeignKey("Company")]
        public string CompanyId { get; set; }

        [Required]
        public string RoleName { get; set; }

        [Required]
        public string Perms {  get; set; }

        public CompanyModel Company { get; set; }

        public ICollection<UsersCompanys> usersCompanys { get; set; }
    }
}
