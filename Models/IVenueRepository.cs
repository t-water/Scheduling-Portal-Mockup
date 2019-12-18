using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TEServerTest.Models
{
    public interface IVenueRepository
    {
        Task<Venue> GetVenueAsync(int? id);
        Task<IEnumerable<Venue>> GetVenuesAsync();
        Task<Venue> Create(Venue venue);
        Task<Venue> Update(Venue updatedVenue);
        Task<Venue> Delete(Venue venue);
    }
}
