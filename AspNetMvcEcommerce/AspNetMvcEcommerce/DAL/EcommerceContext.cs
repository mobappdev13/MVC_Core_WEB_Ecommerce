using System;
using System.Collections;
using System.Data.Entity;
using AspNetMvcEcommerce.Models;

namespace AspNetMvcEcommerce.DAL
{
    public class EcommerceContext : DbContext
    {
        public EcommerceContext() : base("DefaultConnection")
        {

        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<CustomerOrder> CustomerOrders { get; set; }

        public DbSet<OrderedProduct> Orderedproducts { get; set; }

        public DbSet<Cart> Carts { get; set; }
    }
}

