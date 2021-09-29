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
        public string ProductsId { get; set; }

        [Required]
        [ForeignKey("Company")]
        public string CompanyId { get; set; }

        [Required]
        [ForeignKey("SubCategory")]
        public string SubCategoryId { get; set; }

        [Required]
        public bool Available { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0, 1000.00)]
        [Required]
        public decimal Price { get; set; }

        public string ImageId { get; set; }

        [MaxLength(600)]
        public string Description { get; set; }

        [MaxLength(450)]
        public string AllergicWarn { get; set; }

        public string Ingredients {  get; set; }

        [ForeignKey("Zones")]
        public int Zone {  get; set; }

        public ZonesModel zones { get; set; }

        public SubCategoryModel subCategory { get; set; }

        public CompanyModel company { get; set; }  
    }
}
