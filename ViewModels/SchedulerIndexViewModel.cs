using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TEServerTest.Models;
using TEServerTest.Validation;

namespace TEServerTest.ViewModels
{
    public class SchedulerIndexViewModel
    {
        public SchedulerIndexViewModel()
        {
            Shifts = new List<Shift>();
        }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [StartBeforeEnd("Start", true)]
        public DateTime End { get; set; }

        public List<Shift> Shifts { get; set; }

        public int ShiftID { get; set; }
    }
}
