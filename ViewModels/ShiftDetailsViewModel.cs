using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEServerTest.Models;

namespace TEServerTest.ViewModels
{
    public class ShiftDetailsViewModel
    {
        public IQueryable<UserShift> UserShifts { get; set; }
        public Shift Shift { get; set; }

    }
}
