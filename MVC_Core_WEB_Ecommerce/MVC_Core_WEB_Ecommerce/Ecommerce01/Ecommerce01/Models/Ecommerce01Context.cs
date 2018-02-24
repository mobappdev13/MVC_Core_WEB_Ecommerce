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

        public DbSet<Departament> Departaments { get; set; }
        
        public DbSet<Province> Provinces { get; set; }

        public DbSet<City> Cities { get; set; }


        public DbSet<Company> Companies { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<CompanyCustomer> CompanyCustomers { get; set; }

        //public object Users { get; internal set; }

        //public DbSet<CompanyCustomer> CompanyCustomers { get; set; }
        //public DbSet<Customer> Customers { get; set; }

        //add
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //add
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
            //modelBuilder.Entity<Customer>().ToTable("Customer");
            // modelBuilder.Entity<CompanyCustomer>().ToTable("CompanyCustomer");



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

        public DbSet<Tax> Taxes { get; set; }

        public DbSet<Product> Products { get; set; }

        public System.Data.Entity.DbSet<Ecommerce01.Models.Warehouse> Warehouses { get; set; }
    }
}