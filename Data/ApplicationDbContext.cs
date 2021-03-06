//using CamperPlanner.Data.Migrations;
using CamperPlanner.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CamperPlanner.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Voertuigen> Voertuigen { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Contracten> Contracten { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Voertuigen>()
                .HasOne(p => p.ApplicationUser)
                .WithMany(b => b.Voertuigens)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Contracten>()
                .HasOne<Voertuigen>(p => p.Voertuig)
                .WithOne(b => b.Contract)
                .HasForeignKey<Contracten>(p => p.VoertuigId);
        }
        }
}
    