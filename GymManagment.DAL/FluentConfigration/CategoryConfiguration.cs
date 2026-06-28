using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagment.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagment.DAL.FluentConfigration
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Catgory>
    {
        public void Configure(EntityTypeBuilder<Catgory> builder)
        {
            builder.Property(x => x.CatgoryName)
                .HasColumnType("varchar")
                .HasMaxLength(20);
            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
            builder.HasData(
                new Catgory { Id = 1, CatgoryName = "Cardio" },
                new Catgory { Id = 2, CatgoryName = "Strength" },
                new Catgory { Id = 3, CatgoryName = "Yoga" },
                new Catgory { Id = 4, CatgoryName = "Boxing" },
                new Catgory { Id = 5, CatgoryName = "CrossFit" }
            );
        }
    }
}
