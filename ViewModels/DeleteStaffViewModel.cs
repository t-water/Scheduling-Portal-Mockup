using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEServerTest.Models;

namespace TEServerTest.ViewModels
{
    public class DeleteStaffViewModel
    {
        public int UserShiftID { get; set; }
        public string FullName { get; set; }
        public string PositionName { get; set; }
        public bool IsSelected { get; set; }
    }
}
