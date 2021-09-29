using QRestaurantMain.Models;
using Microsoft.EntityFrameworkCore;

namespace QRestaurantMain.Data
{
    public class QRestaurantDbContext : DbContext
    {
        public QRestaurantDbContext(DbContextOptions<QRestaurantDbContext> options) : base(options)
        {

        }

        public DbSet<UsersModel> Users { get; set; }

        public DbSet<UsersActionsModel> UsersActions { get; set; }

        public DbSet<UsersCompanys> UsersCompany { get; set; }

        public DbSet<LicenseModel> License { get; set; }

        public DbSet<CompanyModel> Company { get; set; }

        public DbSet<UsersRolesModel> UsersRoles { get; set; }

        // ---- Orders Gest ---

        public DbSet<TablesModel> Tables { get; set; }

        public DbSet<QRCodesModel> QRCodes { get; set; }

        public DbSet<OrdersModel> Orders { get; set; }


        // ----  Digital Menu  ----


        public DbSet<CategoryModel> Category { get; set; }

        public DbSet<SubCategoryModel> SubCategory { get; set; }

        public DbSet<ProductsModel> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.UseCollation("Latin1_General_CI_AI_WS");

            // ---- Users ----
            modelBuilder.Entity<UsersModel>();

            modelBuilder.Entity<UsersActionsModel>();

            // ---- Roles ----

            modelBuilder.Entity<UsersRolesModel>();

            // ---- Company ----

            modelBuilder.Entity<CompanyModel>();
            modelBuilder.Entity<UsersCompanys>();

            // ---- Orders Gest ---

            modelBuilder.Entity<TablesModel>();

            modelBuilder.Entity<QRCodesModel>();

            modelBuilder.Entity<OrdersModel>();

            // ----  Digital Menu  ----

            modelBuilder.Entity<CategoryModel>();

            modelBuilder.Entity<SubCategoryModel>();

            modelBuilder.Entity<ProductsModel>();
        }
    }
}
