
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TripWMe.CoreHelpers.Attributes;
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
        public DbSet<AuditLog> AuditLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTrip>().HasKey(s => new { s.TripId, s.TUserId });

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.Name).Property<DateTime>(ShadowPropertiesHelper.Created);
                modelBuilder.Entity(entityType.Name).Property<DateTime>(ShadowPropertiesHelper.LastModified);
            }

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            AuditChanges();
            TrackShadowProperties();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            TrackShadowProperties();
            AuditChanges();
            
            return (await base.SaveChangesAsync(true, cancellationToken));
        }

        private void TrackShadowProperties()
        {
            ChangeTracker.DetectChanges();
            var timestamp = DateTime.Now;
            foreach (var entry in ChangeTracker.Entries().Where(
                e => e.State == EntityState.Added || e.State == EntityState.Modified
                && !e.Metadata.IsQueryType))
            {
                entry.Property(ShadowPropertiesHelper.LastModified).CurrentValue = timestamp;

                if (entry.State == EntityState.Added)
                {
                    entry.Property(ShadowPropertiesHelper.Created).CurrentValue = timestamp;
                }
            }
        }

        private void AuditChanges()
        {
            ChangeTracker.DetectChanges();
            var auditEntriesList = new List<AuditLog>();
            foreach (var entry in ChangeTracker.Entries().Where(
                e => e.State == EntityState.Modified
                && !e.Metadata.IsQueryType
                && IsAuditable(e)
                ))

            {
                foreach (var property in entry.Properties)
                {
                    if (!property.IsTemporary)
                    {
                        var auditEntry = new AuditLog()
                        {
                            EntityName = entry.Metadata.Relational().TableName,
                            ColumnName = property.Metadata.Name,
                            NewValues = property.CurrentValue.ToString(),
                            OldValues = property.OriginalValue.ToString()
                        };
                        auditEntriesList.Add(auditEntry);
                    }

                }
            }
            if (auditEntriesList.Count != 0)
            {
                AuditLog.AddRange(auditEntriesList);
                base.SaveChanges();
            }
        }
        private bool IsAuditable(EntityEntry e)
        {
          
            return true;
        }
    }
}
