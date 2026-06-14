using GymManagment.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace GymSystem.FluentConfigration
{
    public class PlanConfigration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(p => p.Name)
                .HasColumnType("varchar")
                .HasMaxLength(200);

            builder.Property(p => p.Describtion)
                .HasMaxLength (30);

            builder.Property(p => p.Price)
                .HasPrecision(10, 2);

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();
            builder.ToTable(TB =>
            {
                TB.HasCheckConstraint("PlanDurationCheck", "DurationDay Between 1 and 365");
            }
                );

        }
    }
}
