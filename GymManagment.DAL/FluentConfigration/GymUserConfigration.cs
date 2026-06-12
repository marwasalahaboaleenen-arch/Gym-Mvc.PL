//using GYMmangment.DAL.Models;
//using GymSystem.FluentConfigration;
//using Microsoft.EntityFrameworkCore;

using GYMmangment.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GYMmangment.DAL.FluentConfigration
{
    public class GymUserConfigration<T> : IEntityTypeConfiguration<T>
        where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Name)
                   .HasColumnType("varchar(50)");

            builder.Property(x => x.Email)
                   .HasColumnType("varchar(50)");
        }
    }
}