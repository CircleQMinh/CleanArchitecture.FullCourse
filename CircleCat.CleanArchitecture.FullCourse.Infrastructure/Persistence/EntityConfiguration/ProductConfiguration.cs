using CircleCat.CleanArchitecture.FullCourse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleCat.CleanArchitecture.FullCourse.Infrastructure.Persistence.EntityConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                 new Product
                 {
                     Id = 1,
                     Name = "Banana",
                     Description = "Description1",
                     Price= 10000,
                     CreatedBy = "Seed",
                     CreatedDate = DateTime.Now,
                     UpdatedBy = "Seed",
                     UpdatedDate = DateTime.Now,
                     CategoryId = 1,
                 },
                 new Product
                 {
                     Id = 2,
                     Name = "Watermelon",
                     Description = "Description2",
                     Price = 20000,
                     CreatedBy = "Seed",
                     CreatedDate = DateTime.Now,
                     UpdatedBy = "Seed",
                     UpdatedDate = DateTime.Now,
                     CategoryId = 1,
                 },
                 new Product
                 {
                     Id = 3,
                     Name = "Mango",
                     Description = "Description3",
                     Price = 30000,
                     CreatedBy = "Seed",
                     CreatedDate = DateTime.Now,
                     UpdatedBy = "Seed",
                     UpdatedDate = DateTime.Now,
                     CategoryId = 1,
                 },
                 new Product
                 {
                     Id = 4,
                     Name = "Apple",
                     Description = "Description4",
                     Price = 40000,
                     CreatedBy = "Seed",
                     CreatedDate = DateTime.Now,
                     UpdatedBy = "Seed",
                     UpdatedDate = DateTime.Now,
                     CategoryId = 1,
                 },



                 new Product
                 {
                     Id = 5,
                     Name = "Candy",
                     Description = "Description5",
                     Price = 10000,
                     CreatedBy = "Seed",
                     CreatedDate = DateTime.Now,
                     UpdatedBy = "Seed",
                     UpdatedDate = DateTime.Now,
                     CategoryId = 2
                 },
                 new Product
                 {
                     Id = 6,
                     Name = "Lolipop",
                     Description = "Description6",
                     Price = 20000,
                     CreatedBy = "Seed",
                     CreatedDate = DateTime.Now,
                     UpdatedBy = "Seed",
                     UpdatedDate = DateTime.Now,
                     CategoryId = 2
                 },
                 new Product
                 {
                     Id = 7,
                     Name = "Ice cream",
                     Description = "Description7",
                     Price = 30000,
                     CreatedBy = "Seed",
                     CreatedDate = DateTime.Now,
                     UpdatedBy = "Seed",
                     UpdatedDate = DateTime.Now,
                     CategoryId = 2
                 },
                 new Product
                 {
                     Id = 8,
                     Name = "Yogurt",
                     Description = "Description8",
                     Price = 40000,
                     CreatedBy = "Seed",
                     CreatedDate = DateTime.Now,
                     UpdatedBy = "Seed",
                     UpdatedDate = DateTime.Now,
                     CategoryId = 2
                 }
            );
        }
    }
}
