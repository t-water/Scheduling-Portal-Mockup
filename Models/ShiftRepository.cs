using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TEServerTest.Data;

namespace TEServerTest.Models
{
    public class ShiftRepository : IShiftRepository
    {
        private readonly TEServerContext context;

        public ShiftRepository(TEServerContext context)
        {
            this.context = context;
        }

        public async Task Create(Shift shift)
        {
            context.Add(shift);
            await context.SaveChangesAsync();
        }

        public async Task<Shift> Update(Shift shift)
        {
            context.Shifts.Update(shift);
            await context.SaveChangesAsync();
            return shift;
        }

        public async Task<Shift> GetShiftAsync(int? id)
        {
            var shift = await context.Shifts.FirstOrDefaultAsync(x => x.ID == id);
            return shift;
        }

        public async Task<Shift> GetShiftWithVenueAsync(int? id)
        {
            var shift = await context.Shifts.Include(x => x.Venue).FirstOrDefaultAsync(x => x.ID == id);
            return shift;
        }

        public IEnumerable<Shift> GetShiftsAsync()
        {
            var shifts = context.Shifts.OrderBy(x => x.Start).Include(v => v.Venue).AsNoTracking();
            return shifts;
        }

        public IOrderedQueryable<Venue> GetVenuesDropdownQuery()
        {
            return from v in context.Venues orderby v.Name select v;
        }

        public IEnumerable<Shift> GetShiftsInDateRange(DateTime Start, DateTime End)
        {
            return context.Shifts.Where(x => x.Start.Date >= Start.Date).Where(x => x.Start.Date <= End.Date).OrderBy(x => x.Start);
        }

        public IEnumerable<Shift> GetUnstaffedShifts()
        {
            var shifts = context.Shifts.OrderBy(x => x.Start).Include(s => s.Venue).Include(s => s.UserShifts).AsNoTracking();
            var model = new List<Shift>();
            foreach(Shift shift in shifts)
            {
                if (!shift.UserShifts.Any())
                {
                    model.Add(shift);
                }
            }

            return model;
        }
    }
}
