using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEServerTest.Models;

namespace TEServerTest.ViewModels
{
    public class SchedulerHMTestViewModel
    {
        public SchedulerHMTestViewModel()
        {
            Users = new List<ApplicationUser>();
        }
        public Shift Shift { get; set; }
        public List<ApplicationUser> Users { get; set; }
    }
}
