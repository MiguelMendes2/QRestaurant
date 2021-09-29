using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QRestaurantMain.Models
{
    public class SubCategoryModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string SubCategoryId { get; set; }

        [Required]
        [ForeignKey("Company")]
        public string CompanyId { get; set; }     

        [Required]
        [ForeignKey("Category")]
        public string CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        public CompanyModel Company { get; set; }

        public CategoryModel Category { get; set; }
    }
}
