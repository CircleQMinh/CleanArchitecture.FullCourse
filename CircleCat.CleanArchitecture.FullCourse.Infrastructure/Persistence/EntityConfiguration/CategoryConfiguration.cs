using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using CircleCat.CleanArchitecture.FullCourse.Domain.Entities;

namespace CircleCat.CleanArchitecture.FullCourse.Infrastructure.Persistence.EntityConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                 new Category
                 {
                     Id = 1,
                     Name = "Fruit",
                     Description= "Description",
                     CreatedBy = "Seed",
                     CreatedDate= DateTime.Now,
                     UpdatedBy = "Seed",
                     UpdatedDate= DateTime.Now,
                 },
                new Category
                {
                    Id = 2,
                    Name = "Snack",
                    Description = "Description",
                    CreatedBy = "Seed",
                    CreatedDate = DateTime.Now,
                    UpdatedBy = "Seed",
                    UpdatedDate = DateTime.Now,
                }
            );
        }
    }
}
