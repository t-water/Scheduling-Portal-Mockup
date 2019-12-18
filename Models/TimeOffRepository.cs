using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEServerTest.Data;

namespace TEServerTest.Models
{
    public class TimeOffRepository : ITimeOffRepository
    {
        private readonly TEServerContext context;

        public TimeOffRepository(TEServerContext context)
        {
            this.context = context;
        }

        public async Task Create(TimeOff timeOff)
        {
            context.TimeOff.Add(timeOff);
            await context.SaveChangesAsync();
        }

        public TimeOff GetTimeOffRequestByTimeOffId(int id)
        {
            return context.TimeOff.FirstOrDefault(x => x.TimeOffID == id);
        }

        public IQueryable<TimeOff> GetTimeOffRequestsByUserId(string id)
        {
            return context.TimeOff.Where(x => x.UserID == id).OrderBy(x => x.Start);
        }

        public IQueryable<TimeOff> GetTimeOffRequestsByStatus(TimeOffStatus status)
        {
            return context.TimeOff.Where(x => x.RequestStatus == status)
                                  .Include(x => x.User);
        }

        public async Task Update(TimeOff timeOff)
        {
            context.Update(timeOff);
            await context.SaveChangesAsync();
        }
    }
}
