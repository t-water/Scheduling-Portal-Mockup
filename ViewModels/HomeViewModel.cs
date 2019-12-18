using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEServerTest.Models;

namespace TEServerTest.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            ShowShiftSwapButton = true;
        }

        public IQueryable<UserShift> UserShifts { get; set; }
        public bool ShowShiftSwapButton { get; set; }
        public string Label { get; set; }
        public string NoShiftsMessage { get; set; }
    }
}
