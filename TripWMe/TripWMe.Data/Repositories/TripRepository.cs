﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripWMe.Data.RepositoryInterfaces;
using TripWMe.Domain;
using TripWMe.Domain.Trips;
using TripWMe.Domain.User;

namespace TripWMe.Data.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly TripWMeContext _context;
        private readonly ILogger<TripRepository> _logger;
        private readonly UserManager<TUser> _userManager;

        public TripRepository(TripWMeContext context, ILogger<TripRepository> logger, UserManager<TUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }
        public void Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
            _context.Add(entity);
        }

        public async Task<ICollection<Trip>> GetAllTripsAsync(bool includeStops = false, bool includeUsers = false)
        {
            _logger.LogInformation($"Getting all Projects");

            IQueryable<Trip> query = _context.Trip;

            if (includeStops && !includeUsers)
            {
                query = query
                    .Include(c => c.Stops)
                        .ThenInclude(l => l.Location)
                        .ThenInclude(c => c.Country)
                        .ThenInclude(r => r.Region)
                    .Include(c => c.Stops)
                        .ThenInclude(l => l.Location)
                        .ThenInclude(lt => lt.LocationType);
            }
            else if (!includeStops && includeUsers)
            {
                query = query.Include(c => c.UserTrips)
                    .ThenInclude(pc => pc.TUser);
            }
            else if (includeStops && includeUsers)
            {
                query = query
                    .Include(i => i.Stops)
                        .ThenInclude(l => l.Location)
                        .ThenInclude(c => c.Country)
                        .ThenInclude(r => r.Region)
                    .Include(c => c.Stops)
                        .ThenInclude(l => l.Location)
                        .ThenInclude(lt => lt.LocationType)
                    .Include(c => c.UserTrips)
                        .ThenInclude(pc => pc.TUser);
            }

            _logger.LogInformation($"Getting all Projects. Returned: {query.Count()}");

            return await query.ToArrayAsync();
        }

        public async Task<ICollection<Trip>> GetAllTripsWithStats()
        {
            IQueryable<Trip> query = _context.Trip;

            query = query
               .Include(i => i.Stops)
                .ThenInclude(l => l.Location)
                .ThenInclude(c => c.Country)
                .ThenInclude(r => r.Region)
            .Include(c => c.Stops)
                .ThenInclude(l => l.Location)
                .ThenInclude(lt => lt.LocationType)
            .Include(c => c.UserTrips)
                .ThenInclude(pc => pc.TUser);
           //  .Include(s => s.TripStats);


            foreach (var stat in query)
            {
                stat.TripStats.LocationCount = stat.Stops.Distinct().Count();
                stat.TripStats.CountryCount = stat.Stops.Select(c => c.Location).Select(c => c.CountryId).Distinct().Count();
                stat.TripStats.UserCount = stat.UserTrips.Select(u => u.TUserId).Distinct().Count();
            }

            return await query.ToArrayAsync();
        }

        public async Task<ICollection<Trip>> GetTripsByUserAsync(string userName)
        {
            var user = new TUser();
            try
            {
                user = await _userManager.FindByNameAsync(userName);
            }
            catch (Exception ex)
            {

                throw new Exception("User was not found", ex);
            }

            IQueryable<Trip> query = _context.Trip;

            query = query
                .Include(i => i.Stops)
                    .ThenInclude(l => l.Location)
                    .ThenInclude(c => c.Country)
                    .ThenInclude(r => r.Region)
                .Include(c => c.Stops)
                    .ThenInclude(l => l.Location)
                    .ThenInclude(lt => lt.LocationType)
                .Include(c => c.UserTrips)
                    .ThenInclude(pc => pc.TUser);
                    //.Where(u => u.UserExist(user));


            return await query.ToArrayAsync();
        }

        public async Task<Trip> GetTripByCode(int tripCode)
        {
            IQueryable<Trip> query = _context.Trip.Where(t => t.Id == tripCode);

            query = query
                .Include(i => i.Stops)
                    .ThenInclude(l => l.Location)
                    .ThenInclude(c => c.Country)
                     .ThenInclude(r => r.Region)
                .Include(c => c.Stops)
                    .ThenInclude(l => l.Location)
                    .ThenInclude(lt => lt.LocationType)
                .Include(c => c.UserTrips)
                    .ThenInclude(pc => pc.TUser);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            _logger.LogInformation($"Attempitng to save the changes in the context");

            // Only return success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
