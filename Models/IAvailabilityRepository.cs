using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TEServerTest.Models
{
    public interface IAvailabilityRepository
    {
        bool CheckIfUserIsAvailable(string id, DateTime Start, DayOfWeek Day);
        bool CheckIfUserIsNotOff(string id, DateTime Start, DateTime End);
        Task Create(Availability availability);
        Task Update(Availability availability);
        IEnumerable<Availability> GetAvailabilitiesByUserID(string id);
        Availability GetAvailabilityByAvailabilityID(int id);
        bool CanBeScheduled(string id, DateTime Start, DateTime End);
    }
}
