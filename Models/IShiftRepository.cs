using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TEServerTest.Models
{
    public interface IShiftRepository
    {
        IOrderedQueryable<Venue> GetVenuesDropdownQuery();
        IEnumerable<Shift> GetShiftsAsync();
        Task<Shift> GetShiftAsync(int? id);
        Task<Shift> GetShiftWithVenueAsync(int? id);
        Task Create(Shift shift);
        Task<Shift> Update(Shift shift);
        IEnumerable<Shift> GetShiftsInDateRange(DateTime Start, DateTime End);
    }
}
