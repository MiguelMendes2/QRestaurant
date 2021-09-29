using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QRestaurantMain.Models
{
    public class ZonesModel
    {
        [Key]
        public string ZoneId { get; set; }

        [Required]
        [ForeignKey("Company")]
        public string CompanyId { get; set; }

        public string Name { get; set; }

        public CompanyModel company { get; set; }

        public ICollection<ProductsModel> products { get; set; }
    }
}
