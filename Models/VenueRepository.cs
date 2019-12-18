using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TEServerTest.Data;

namespace TEServerTest.Models
{
    public class VenueRepository : IVenueRepository
    {
        private readonly TEServerContext context;

        public VenueRepository(TEServerContext context)
        {
            this.context = context;
        }

        public async Task<Venue> Create(Venue venue)
        {
            context.Venues.Add(venue);
            await context.SaveChangesAsync();
            return venue;
        }

        public async Task<Venue> Delete(Venue venue)
        {
            context.Venues.Remove(venue);
            await context.SaveChangesAsync();
            return venue;
        }

        public async Task<Venue> GetVenueAsync(int? id)
        {
            var venue = await context.Venues
                .FirstOrDefaultAsync(v => v.ID == id);
            return venue;
        }

        public async Task<IEnumerable<Venue>> GetVenuesAsync()
        {
            var venues = await context.Venues.ToListAsync();
            return venues;
        }

        public async Task<Venue> Update(Venue venue)
        {
            context.Update(venue);
            await context.SaveChangesAsync();
            return venue;
        }
    }
}
