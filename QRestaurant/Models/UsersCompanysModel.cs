using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QRestaurantMain.Models
{
    public class UsersCompanys
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Users")]
        public string UserId {  get; set; }

        [Required]
        [ForeignKey("Company")]
        public string CompanyId { get; set; }

        [Required]
        [ForeignKey("UsersRoles")]
        public string RoleId { get; set; }

        public UsersModel users { get; set; }

        public CompanyModel company { get; set; }

        public UsersRolesModel usersRoles { get; set; }
    }
}
