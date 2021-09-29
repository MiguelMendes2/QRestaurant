using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QRestaurantMain.Models
{
    public class CategoryModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CategoryId { get; set; }

        [Required]
        [ForeignKey("Company")]
        public string CompanyId { get; set; }

        public CompanyModel Company {  get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<SubCategoryModel> SubCategories { get; set; }

    }
}
