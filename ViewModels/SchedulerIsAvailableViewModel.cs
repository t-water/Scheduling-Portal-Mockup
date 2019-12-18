using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TEServerTest.Validation;

namespace TEServerTest.ViewModels
{
    public class SchedulerIsAvailableViewModel
    {
        public DateTime Start { get; set; }

        [StartBeforeEnd("Start")]
        public DateTime End { get; set; }

        [Display(Name = "User")]
        public string UserID { get; set; }
    }
}
