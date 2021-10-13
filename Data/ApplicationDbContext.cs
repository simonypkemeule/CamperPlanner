using CamperPlanner.Data.Migrations;
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
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Voertuigen> Voertuigen { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
    