using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductManagementData.Entities;

namespace ProductManagementData.Context
{
    public class AppDbContext : IdentityDbContext<User, Role, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Product>().HasQueryFilter(p => !p.IsDeleted);

            builder.Entity<Role>().HasData(
                new Role
                {
                    Id = "1",
                    Name = "User",
                    NormalizedName = "USER",
                    RoleId = 1
                },
                new Role
                {
                Id = "2",
                Name = "Admin",
                NormalizedName = "ADMIN",
                RoleId = 2 
                }
            );
        }
    }
}