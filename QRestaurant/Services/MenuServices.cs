using QRestaurantMain.Data;
using QRestaurantMain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QRestaurantMain.Services
{
    public class MenuServices : IMenuServices
    {
        private readonly QRestaurantDbContext AppDb;

        public MenuServices(QRestaurantDbContext Db)
        {
            AppDb = Db;
        }

        /// <summary>
        ///  Add New Category to Menu
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="CompanyId"></param>
        public void NewCategory(string Name, string CompanyId)
        {
            AppDb.Category.Add(new CategoryModel 
            { 
                CategoryId = Guid.NewGuid().ToString(),
                Name = Name, 
                CompanyId = CompanyId 
            });
            AppDb.SaveChanges();
        }

        /// <summary>
        /// Add New Sub Category to Menu
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="CompanyId"></param>
        /// <param name="CategoryId"></param>
        /// <returns> 
        ///     False -> if companyId or Category Id are invalid
        ///     True -> Sub Category Created with success
        /// </returns>
        public Boolean NewSubCategory(string Name, string CompanyId, string CategoryId)
        {
            if (AppDb.Category.FirstOrDefault(x => x.CategoryId == CategoryId && x.CompanyId == CompanyId) == null)
                return false;
            AppDb.SubCategory.Add(new SubCategoryModel
            {
                SubCategoryId = Guid.NewGuid().ToString(),
                Name = Name,
                CategoryId = CategoryId,
                CompanyId = CompanyId
            });
            return true;
        }

        /// <summary>
        /// Add New Product to Menu
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="CompanyId"></param>
        /// <param name="SubCategoryId"></param>
        /// <param name="Description"></param>
        /// <param name="AllergicWarn"></param>
        /// <param name="ImageUrl"></param>
        /// <param name="price"></param>
        /// <returns> 
        ///     False -> if companyId or Category Id are invalid
        ///     True -> Product Created with success
        /// </returns>
        public Boolean NewProduct(string Name, string CompanyId, string SubCategoryId, string Description, 
            string AllergicWarn, string ImageUrl, decimal price)
        {
            if (AppDb.SubCategory.FirstOrDefault(x => x.SubCategoryId == SubCategoryId && x.CompanyId == CompanyId) == null)
                return false;
            AppDb.Products.Add(new ProductsModel
            {
                ProductsId = Guid.NewGuid().ToString(),
                SubCategoryId = SubCategoryId,
                CompanyId = CompanyId,
                Name = Name,
                Description = Description,
                AllergicWarn = AllergicWarn,
                Available = true,
                ImageId = ImageUrl,
                Price = price
                
            });
            return true;
        }
    }
}
