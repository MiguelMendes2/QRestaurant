using QRestaurantMain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QRestaurantMain.ViewModels;

namespace QRestaurantMain.Data
{
    public class QRestaurantDbContext : DbContext
    {
        public QRestaurantDbContext(DbContextOptions<QRestaurantDbContext> options) : base(options)
        {

        }

        public DbSet<UsersModel> Users { get; set; }

        public DbSet<UsersActionsModel> UsersActions { get; set; }

        public DbSet<CompanyModel> Company { get; set; }

        public DbSet<TablesModel> Tables { get; set; }

        public DbSet<UsersRolesModel> UsersRoles { get; set; }


        // ----  Digital Menu  ----


        public DbSet<CategoryModel> Category { get; set; }

        public DbSet<SubCategoryModel> SubCategory { get; set; }

        public DbSet<ProductsModel> Products { get; set; }
    }
}
