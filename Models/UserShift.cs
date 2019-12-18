using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TEServerTest.Models
{
    public enum ShiftSwapStatus
    {
        Available,
        Pending,
        Taken,
        Denied
    }

    public class UserShift
    {
        public UserShift()
        {
            RequestStatus = 0;
        }

        public int UserShiftID { get; set; }

        [Required(ErrorMessage = "User must not be left blank")]
        public string UserID { get; set; }
        
        public int ShiftID { get; set; }
        
        public string PositionID { get; set; }

        public DateTime UserStart { get; set; }

        public DateTime UserEnd { get; set; }

        public string TakenByID { get; set; }
        
        public ShiftSwapStatus RequestStatus { get; set; }

        [ForeignKey("TakenByID")]
        public ApplicationUser TakenBy { get; set; }
        
        public ApplicationUser User { get; set; }
        
        public Shift Shift { get; set; }
        
        public IdentityRole Position { get; set; }
    }
}
