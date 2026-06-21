using GymManagment.DAL.Models;
using GymSystem.FluentConfigration;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GymSystem.Context
{
    public class GymDbcontext : DbContext
    {
        public GymDbcontext(DbContextOptions<GymDbcontext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Category> Trainers { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Catgory> Categories { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }

    }
}