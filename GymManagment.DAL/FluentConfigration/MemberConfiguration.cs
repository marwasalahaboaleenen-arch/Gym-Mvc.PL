using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagment.DAL.Models;
using GYMmangment.DAL.FluentConfigration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagment.DAL.FluentConfigration
{
    internal class MemberConfiguration : GymUserConfigration<member>, IEntityTypeConfiguration<member>
    {
        public new void Configure(EntityTypeBuilder<member> builder)
        {
            builder.Property(x => x.CreatedAt)
                .HasColumnName("JoinDate")
                .HasDefaultValueSql("GETDATE()");
            base.Configure(builder);
        }
    }
