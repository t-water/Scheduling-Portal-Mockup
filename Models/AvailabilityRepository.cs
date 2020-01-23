using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEServerTest.Data;

namespace TEServerTest.Models
{
    public class AvailabilityRepository : IAvailabilityRepository
    {
        private readonly TEServerContext context;

        public AvailabilityRepository(TEServerContext context)
        {
            this.context = context;
        }

        public IEnumerable<Availability> GetAvailabilitiesByUserID(string id)
        {
            return context.Availabilities.Where(x => x.UserID == id).OrderBy(x => x.Start).ThenBy(x => x.Day).ToList();
        }

        public Availability GetAvailabilityByAvailabilityID(int id)
        {
            return context.Availabilities.FirstOrDefault(x => x.AvailabilityID == id);
        }

        public async Task Create(Availability availability)
        {
            context.Availabilities.Add(availability);
            await context.SaveChangesAsync();
        }

        public async Task Update(Availability availability)
        {
            context.Availabilities.Update(availability);
            await context.SaveChangesAsync();
        }

        public bool CheckIfUserIsAvailable(string id, DateTime Start, DayOfWeek Day)
        {
            var IsAvailable = context.Availabilities.Where(x => x.UserID == id)
                                                    .Where(x => x.Day == Day)
                                                    .Where(x => x.Start.TimeOfDay == Start.TimeOfDay)
                                                    .FirstOrDefault(x => x.IsSelected == true);

            if(IsAvailable != null)
            {
                return true;
            }

            return false;
        }

        public bool CheckIfUserIsNotOff(string id, DateTime Start, DateTime End)
        {
            if(!TimeIsInTimeOff(Start, id) && !TimeIsInTimeOff(End, id))
            {
                return true;
            }

            return false;
        }

        private bool TimeIsInTimeOff(DateTime Date, string id)
        {
            var timeOffCheck = context.TimeOff.Where(x => x.UserID == id)
                                              .Where(x => x.RequestStatus == TimeOffStatus.Approved)
                                              .Where(x => ((DateTime.Compare(Date, x.Start) > 0) && (DateTime.Compare(Date, x.End) < 0)));

            if(timeOffCheck.Count() == 0)
            {
                return false;
            }

            return true;
        }

        public bool CanBeScheduled(string id, DateTime Start, DateTime End)
        {
            if (Start.Hour >= 0 && Start.Hour < 8)
            {
                Start = Start.AddHours(-Start.Hour);
                if (Start.Minute >= 30 && Start.Minute < 60)
                {
                    Start = Start.AddMinutes(-(Start.Minute - 29));
                }
            }
            if (Start.Minute > 0 && Start.Minute < 30)
            {
                Start = Start.AddMinutes(-1 * Start.Minute);
            }
            else if (Start.Minute > 30 && Start.Minute < 60)
            {
                Start = Start.AddMinutes(-(Start.Minute - 30));
            }
            for (DateTime date = Start; date < End; date = date.AddMinutes(30))
            {
                if (!CheckIfUserIsAvailable(id, date, date.DayOfWeek))
                {
                    return false;
                }

                if (date.TimeOfDay == DateTime.MinValue.TimeOfDay)
                {
                    date = date.AddHours(7).AddMinutes(30);
                }
            }

            if (!CheckIfUserIsNotOff(id, Start, End))
            {
                return false;
            }

            return true;
        }
    }
}
