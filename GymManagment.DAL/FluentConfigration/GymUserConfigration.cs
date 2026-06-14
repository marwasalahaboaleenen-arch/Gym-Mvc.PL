//using GYMmangment.DAL.Models;
//using GymSystem.FluentConfigration;
//using Microsoft.EntityFrameworkCore;

using GymManagment.DAL.Models;
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

            builder.HasIndex(X => X.Email).IsUnique();
            builder.HasIndex(X => X.Phone).IsUnique();

            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("Email Like", "Email Like '_%@_%._%'");
                tb.HasCheckConstraint("PhoneCheck", "Phone Like '010%' or Phone Like '011% or Phone Like '012%''");
            });

            builder.OwnsOne(X => X.address, address =>
            {
                address.Property(X => X.Street).HasColumnName("Street").HasColumnType("varchar").HasMaxLength(30);
                address.Property(X => X.city).HasColumnName("City").HasColumnType("varchar").HasMaxLength(30);

            });
        }

        
    }
}