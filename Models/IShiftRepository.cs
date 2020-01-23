using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TEServerTest.Models
{
    public interface IShiftRepository
    {
        IOrderedQueryable<Venue> GetVenuesDropdownQuery();
        IQueryable<Shift> GetShiftsAsync();
        IQueryable<Shift> GetFutureShifts();
        IQueryable<Shift> GetPastShifts();
        Task<Shift> GetShiftAsync(int? id);
        Task<Shift> GetShiftWithVenueAsync(int? id);
        Task Create(Shift shift);
        Task<Shift> Update(Shift shift);
        IEnumerable<Shift> GetShiftsInDateRange(DateTime Start, DateTime End);
        IEnumerable<Shift> GetUnstaffedShifts();
    }
}
