using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TEServerTest.Models
{
    public interface ITimeOffRepository
    {
        Task Create(TimeOff timeOff);
        IQueryable<TimeOff> GetTimeOffRequestsByUserId(string id);
        TimeOff GetTimeOffRequestByTimeOffId(int id);
        IQueryable<TimeOff> GetTimeOffRequestsByStatus(TimeOffStatus status);
        Task Update(TimeOff timeOff);
    }
}
