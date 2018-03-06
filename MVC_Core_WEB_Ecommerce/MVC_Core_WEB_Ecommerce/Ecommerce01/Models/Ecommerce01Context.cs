using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Ecommerce01.Models
{
    public class Ecommerce01Context : DbContext
    {
        //DefaultConnection: in Web.config
        public Ecommerce01Context() : base("DefaultConnection")
        {
            // db.Configuration.ProxyCreationEnabled = false;
            //Configuration.ProxyCreationEnabled = false;
            //Configuration.LazyLoadingEnabled = false;
        }

        //
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        //}
        public DbSet<Departament> Departaments { get; set; }
        
        public DbSet<Province> Provinces { get; set; }

        public DbSet<City> Cities { get; set; }


        public DbSet<Company> Companies { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<CompanyCustomer> CompanyCustomers { get; set; }

        public DbSet<Tax> Taxes { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Warehouse> Warehouses { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<Inventory> Inventories { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<CompanySupplier> CompanySuppliers { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //fondamentale
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
           

            //add
            modelBuilder.Entity<Departament>().ToTable("Departament");
            modelBuilder.Entity<Province>().ToTable("Province");
            modelBuilder.Entity<City>().ToTable("City");

            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Company>().ToTable("Company");

            modelBuilder.Entity<Tax>().ToTable("Tax");
            modelBuilder.Entity<Product>().ToTable("Product");

            modelBuilder.Entity<Warehouse>().ToTable("Warehouse");

            modelBuilder.Entity<Inventory>().ToTable("Inventory");

            modelBuilder.Entity<Customer>().ToTable("Customer");

            modelBuilder.Entity<CompanySupplier>().ToTable("CompanySupplier");

            modelBuilder.Entity<State>().ToTable("State");

            modelBuilder.Entity<Supplier>().ToTable("Supplier");

            modelBuilder.Entity<CompanyCustomer>().ToTable("CompanyCustomer");

            modelBuilder.Entity<User>().ToTable("User");

            //add for indexes

            //modelBuilder.Entity<Departament>()
            //  .HasKey(d => new { d.DepartamentId, d.Name });

            //modelBuilder.Entity<Province>()
            //   .HasKey(p => new { p.DepartamentId, p.ProvinceId, p.Name, });

            //modelBuilder.Entity<City>()
            //    .HasKey(c => new { c.DepartamentId, c.ProvinceId, c.CityId, c.Name });

            //for department
            modelBuilder.Entity<Departament>()
                .Property(d => d.Latitud)
                .HasPrecision(18, 6);
            modelBuilder.Entity<Departament>()
               .Property(d => d.Longitud)
               .HasPrecision(18, 6);

            //for province
            modelBuilder.Entity<Province>()
                     .Property(p => p.Latitud)
                     .HasPrecision(18, 6);
            modelBuilder.Entity<Province>()
                     .Property(p => p.Longitud)
                     .HasPrecision(18, 6);
            //for city
            modelBuilder.Entity<City>()
                    .Property(c => c.Latitud)
                    .HasPrecision(18, 6);
            modelBuilder.Entity<City>()
                     .Property(c => c.Longitud)
                     .HasPrecision(18, 6);

        }

       
    }
}