using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEServerTest.Data;

namespace TEServerTest.Models
{
    public class UserShiftRepository : IUserShiftRepository
    {
        private readonly TEServerContext context;

        public UserShiftRepository(TEServerContext context)
        {
            this.context = context;
        }

        public IQueryable<UserShift> GetUserShiftsByShiftID(int id)
        {
            return context.UsersShifts.Where(x => x.ShiftID == id);
        }

        public IQueryable<UserShift> GetUserShiftsForHomePage(string id)
        {
            return context.UsersShifts.Where(x => x.UserID == id)
                                      .Include(x => x.User)
                                      .Include(x => x.Position)
                                      .Include(x => x.Shift)
                                      .ThenInclude(shift => shift.Venue);
        }

        public IQueryable<UserShift> GetUserShiftsByUserID(string id)
        {
            return context.UsersShifts.Where(x => x.UserID == id);
        }

        public UserShift GetUserShift(int id)
        {
            return context.UsersShifts.FirstOrDefault(x => x.UserShiftID == id);
        }

        public async Task<UserShift> GetUserShiftForToggleCover(int id) {
            return await context.UsersShifts.Where(x => x.UserShiftID == id)
                                            .Include(x => x.User)
                                            .FirstOrDefaultAsync();
        }

        public IQueryable<UserShift> GetUserShiftsByStatus(ShiftSwapStatus status)
        {
            return context.UsersShifts.Where(x => x.RequestStatus == status)
                                      .Include(x => x.User)
                                      .Include(x => x.TakenBy)
                                      .Include(x => x.Shift)
                                      .ThenInclude(shift => shift.Venue)
                                      .Include(x => x.Position);
        }

        public double GetHoursScheduledInDateRange(string id, DateTime Start, DateTime End)
        {
            double hoursScheduled = 0;
            var shifts = context.UsersShifts.Where(x => x.UserID == id).Where(x => x.UserStart >= Start).Where(x => x.UserEnd < End.AddDays(1));
            foreach(var shift in shifts)
            {
                TimeSpan elapsedTime = shift.UserEnd.Subtract(shift.UserStart);
                hoursScheduled += elapsedTime.TotalHours;
            }

            return hoursScheduled;
        }

        public async Task Create(UserShift us)
        {
            context.Add(us);
            await context.SaveChangesAsync();
        }

        public async Task DeleteUserShiftByID(int id)
        {
            UserShift us = GetUserShift(id);
            context.Remove(us);
            await context.SaveChangesAsync();
        }

        public async Task Update(UserShift us)
        {
            context.Update(us);
            await context.SaveChangesAsync();
        }
        
    }
}
