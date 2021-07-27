using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QRestaurantMain.Models
{
    public class ProductsModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Required]
        public string CompanyId { get; set; }

        [Required]
        public string SubCategoryId { get; set; }

        public string ImageId { get; set; }

        [Required]
        public Boolean Available { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string AllergicWarn { get; set; }

        public string Price { get; set; }
    }
}
