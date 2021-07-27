using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QRestaurantMain.Models
{
    public class UsersActionsModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime ExpDate { get; set; }

        [Required]
        public string Data { get; set; }
    }
}
