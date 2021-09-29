using QRestaurantMain.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QRestaurantMain.Models
{
    public class CompanyModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CompanyId { get; set; }

        [Required]
        [ForeignKey("License")]
        public string LicenseId { get; set; }

        [Required]
        public string OwnerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public string Schedule { get; set; }

        [RegularExpression(@"^[0-9]\d{8}$", ErrorMessage = "Nif inválido")]
        [Required]
        public int Nif { get; set; }

        [Required]
        [MaxLength(100)]
        public string Andress { get; set; }

        [Required]
        [MaxLength(50)]
        public string Region { get; set; }

        public LicenseModel License { get; set; }

        public ICollection<CategoryModel> categories { get; set; }

        public ICollection<SubCategoryModel> subCategories { get; set; }

        public ICollection<ProductsModel> products { get; set; }

        public ICollection<TablesModel> tables { get; set; }

        public ICollection<UsersRolesModel> userRoles { get; set; }

        public ICollection<ZonesModel> zones { get; set; }

        public ICollection<UsersCompanys> usersCompanys { get; set; }


    }
}
