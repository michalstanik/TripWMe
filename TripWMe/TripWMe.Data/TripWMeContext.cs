
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TripWMe.Domain;

namespace TripWMe.Data
{
    public class TripWMeContext : IdentityDbContext<TUser>
    {
        public TripWMeContext(DbContextOptions<TripWMeContext> options) : base(options)
        {
            
        }

        public DbSet<Trip> Trip { get; set; }
        public DbSet<Stop> Stop { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Country> Country { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTrip>().HasKey(s => new { s.TripId, s.TUserId});
            //modelBuilder.Entity<UserTrip>().HasOne(sj => sj.TripUser).WithMany(u => u.UserTrips)
            //    .HasForeignKey(ujr => ujr.TripUserId).OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<UserTrip>().HasOne(sj => sj.Trip).WithMany(u => u.UserTrips)
            //    .HasForeignKey(ujr => ujr.TripId).OnDelete(DeleteBehavior.Cascade);


            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.Name).Property<DateTime>(ShadowPropertiesHelper.Created);
                modelBuilder.Entity(entityType.Name).Property<DateTime>(ShadowPropertiesHelper.LastModified);
            }

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            TrackShadowProperties();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            TrackShadowProperties();
            return (await base.SaveChangesAsync(true, cancellationToken));
        }

        private void TrackShadowProperties()
        {
            ChangeTracker.DetectChanges();
            var timestamp = DateTime.Now;
            foreach (var entry in ChangeTracker.Entries().Where(
                e => e.State == EntityState.Added || e.State == EntityState.Modified
                && !e.Metadata.IsQueryType ))
            {
                entry.Property(ShadowPropertiesHelper.LastModified).CurrentValue = timestamp;

                if (entry.State == EntityState.Added)
                {
                    entry.Property(ShadowPropertiesHelper.Created).CurrentValue = timestamp;
                }
            }
        }
    }
}
