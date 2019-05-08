﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TripWMe.Data.DataAuditFuture;
using TripWMe.Domain.Admin;
using TripWMe.Domain.Stops;
using TripWMe.Domain.Trips;
using TripWMe.Domain.User;
using TripWMe.Domain.WorldHeritage;

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
        public DbSet<LocationType> LocationType { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<Continent> Continent { get; set; }
        public DbSet<AuditLog> AuditLog { get; set; }
        public DbSet<TripStats> TripStats { get; set; }

        //WorldHeritage
        public DbSet<WorldHeritage> WorldHeritage { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTrip>().HasKey(s => new { s.TripId, s.TUserId });
            modelBuilder.Entity<TripStats>().HasKey(s => new { s.TripId });

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {

                //modelBuilder.Entity(entityType.Name).Property<DateTime>(ShadowPropertiesHelper.Created);
                //modelBuilder.Entity(entityType.Name).Property<DateTime>(ShadowPropertiesHelper.LastModified);
            }
            modelBuilder.Entity<Trip>().Property(p => p.TripCode).HasComputedColumnSql("'TR-' + CONVERT(varchar(10),[Id])");
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            //TrackShadowProperties();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            //TrackShadowProperties();
            var auditEntries = OnBeforeSaveChanges();

            var result = await base.SaveChangesAsync(true, cancellationToken);
            await OnAfterSaveChanges(auditEntries);
            return result;

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

        private List<AuditEntry> OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Metadata.Relational().TableName;
                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        // value will be generated by the database, get the value after saving
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }
            // Save audit entities that have all the modifications
            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
            {
                AuditLog.Add(auditEntry.ToAudit());
            }

            // keep a list of entries where the value of some properties are unknown at this step
            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }

        private Task OnAfterSaveChanges(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;

            foreach (var auditEntry in auditEntries)
            {
                // Get the final value of the temporary properties
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }

                // Save the Audit entry
                AuditLog.Add(auditEntry.ToAudit());
            }

            return SaveChangesAsync();
        }

    }
}
