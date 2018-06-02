using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PrenditiDaBere.Data.Models;

namespace PrenditiDaBere.Data.Repositories
{

    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Bibita> Bibite { get; set; }
        public DbSet<Categoria> Categorie { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        public DbSet<Ordine> Ordini { get; set; }
        public DbSet<DettagliOrdine> DettagliOrdini { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //to avoid user  request
            //The entity type 'Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin' requires a key to be defined
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Bibita>().ToTable("Bibita");
            modelBuilder.Entity<Categoria>().ToTable("Categoria");
            modelBuilder.Entity<ShoppingCartItem>().ToTable("ShoppingCartItem");
            modelBuilder.Entity<Ordine>().ToTable("Ordine");
            modelBuilder.Entity<DettagliOrdine>().ToTable("DettagliOrdine");
        }

    }
}

//protected override void OnModelCreating(ModelBuilder modelBuilder)
//{
//    base.OnModelCreating(modelBuilder);
//}


