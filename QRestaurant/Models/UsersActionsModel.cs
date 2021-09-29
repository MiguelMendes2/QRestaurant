using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QRestaurantMain.Models
{
    public class UsersActionsModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UsersActionsId { get; set; }

        [Required]
        [ForeignKey("Users")]
        public string UserId { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime ExpDate { get; set; }

        [Required]
        public string Data { get; set; }

        public UsersModel Users { get; set; }
    }
}
