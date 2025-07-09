using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NailSalon.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.DAL.Contexts
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Reservation>()
            //    .HasOne(r => r.NailType)
            //    .WithMany() // əgər Design tərəfdə Reservation siyahısı yoxdursa
            //    .HasForeignKey(r => r.NailTypeId)
            //    .OnDelete(DeleteBehavior.Restrict); // ya da .Cascade əgər avtomatik silmək istəyirsənsə

            modelBuilder.Entity<Reservation>()
        .HasOne(r => r.NailType)
        .WithMany() // əgər əlaqəli kolleksiya yoxdursa
        .HasForeignKey(r => r.NailTypeId)
        .OnDelete(DeleteBehavior.SetNull); // <- əsas hissə
        }
        public DbSet<Master> Masters { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<NailType> NailTypes { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Design> Designs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }

        public DbSet<ShopProduct> ShopProducts { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Sale> Sales { get; set; }

    }
}
