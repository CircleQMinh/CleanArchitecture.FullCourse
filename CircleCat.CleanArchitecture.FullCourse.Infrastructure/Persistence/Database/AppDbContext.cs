using CircleCat.CleanArchitecture.FullCourse.Domain.Common;
using CircleCat.CleanArchitecture.FullCourse.Domain.Entities;
using CircleCat.CleanArchitecture.FullCourse.Infrastructure.Persistence.EntityConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CircleCat.CleanArchitecture.FullCourse.Infrastructure.Persistence.Database
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //modelBuilder.ApplyConfiguration(typeof(AppDbContext).Assembly);
            base.OnModelCreating(builder);
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (!String.IsNullOrEmpty(tableName))
                {
                    if (tableName.StartsWith("AspNet"))
                    {
                        entityType.SetTableName(tableName.Substring(6));
                    }
                }

            }
            builder.ApplyConfiguration(new CategoryConfiguration());
            //builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
        }

        public virtual Task<int> SaveChangesAsync(string username = "")
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                entry.Entity.UpdatedDate = DateTime.Now;
                entry.Entity.UpdatedBy = username;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate= DateTime.Now;
                    entry.Entity.CreatedBy = username;
                }
            }
            return base.SaveChangesAsync();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
