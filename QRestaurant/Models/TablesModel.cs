using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QRestaurantMain.Models
{
    public class TablesModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Required]
        public string CompanyId { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string API_KEY { get; set; }

        [Required]
        public int State { get; set; }

        [Required]
        public string Pedido { get; set; }
    }
}
