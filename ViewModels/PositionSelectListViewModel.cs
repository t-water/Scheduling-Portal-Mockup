using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TEServerTest.Models;

namespace TEServerTest.ViewModels
{
    public class PositionSelectListViewModel
    {
        public string RoleName { get; set; }
        public int Count { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
        public string PositionID { get; set; }
    }
}
