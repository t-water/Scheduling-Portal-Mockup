using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TEServerTest.Models
{
    public interface IUserShiftRepository
    {
        IQueryable<UserShift> GetUserShiftsByShiftID(int id);
        IQueryable<UserShift> GetUserShiftsByUserID(string id);
        IQueryable<UserShift> GetUserShiftsForHomePage(string id);
        Task<UserShift> GetUserShiftForToggleCover(int id);
        IQueryable<UserShift> GetUserShiftsByStatus(ShiftSwapStatus status);
        double GetHoursScheduledInDateRange(string id, DateTime Start, DateTime End);
        Task Create(UserShift us);
        Task Update(UserShift us);
        UserShift GetUserShift(int id);
        Task DeleteUserShiftByID(int id);
    }
}
