using System.Collections.Generic;
using System.Reflection.Emit;
using Lab7.DAL.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab7.DAL.EF;
    public class Lab7Context : DbContext
    {
        public Lab7Context(DbContextOptions<Lab7Context> options)
            : base(options)
        {
        }
        public Lab7Context()
        {
        }
    public DbSet<Admin> Admins { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Lab7;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasOne(r => r.Admin)
                .WithMany(t => t.Orders)
                .HasForeignKey(r => r.AdminId)
                .IsRequired();
        }
    }
