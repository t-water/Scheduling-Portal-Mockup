using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TEServerTest.Models
{
    public enum DaysOfTheWeek
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    public class Availability
    {
        public Availability()
        {
            IsSelected = false;
        }

        public int AvailabilityID { get; set; }
        public DayOfWeek Day { get; set; }

        [DataType(DataType.Time)]
        public DateTime Start { get; set; }

        [DataType(DataType.Time)]
        public DateTime End { get; set; }

        public bool IsSelected { get; set; }

        public string UserID { get; set; }
        public ApplicationUser User { get; set; }
    }
}
