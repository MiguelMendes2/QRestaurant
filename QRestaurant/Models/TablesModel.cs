using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QRestaurantMain.Models
{
    public class TablesModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string TablesId { get; set; }

        [Required]
        [ForeignKey("Company")]
        public string CompanyId { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public int State { get; set; }

        [Required]
        public string APIKEY { get; set; }

        public CompanyModel company { get; set; }

        public ICollection<TablesModel> tables { get; set; }
    }
}
