using GymSystem.FluentConfigration;
using GymSystem.Models;
using Microsoft.EntityFrameworkCore;
 

namespace GymSystem.context
{
    public class GymDbcontext : DbContext
    {
       public GymDbcontext(DbContextOptions<GymDbcontext>option) :base (option){ 

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Plan>(new PlanConfigration());
        }
        public DbSet <Plan> plans { get; set; }

       
    }
}
