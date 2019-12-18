using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TEServerTest.Validation;

namespace TEServerTest.Models
{
    public enum TimeOffStatus
    {
        Pending,
        Approved,
        Denied
    }

    public class TimeOff
    {
        public TimeOff()
        {
            Start = DateTime.Today;
            End = DateTime.Today;
            RequestStatus = 0;
        }

        public int TimeOffID { get; set; }

        public DateTime Start { get; set; }

        [StartBeforeEnd("Start")]
        public DateTime End { get; set; }


        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Additional Info")]
        public string AdditionalInformation { get; set; }

        [Display(Name = "Request Status")]
        public TimeOffStatus RequestStatus { get; set; }

        [Required]
        public string UserID { get; set; }
        public ApplicationUser User { get; set; }

        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Reason For Denial")]
        public string ReasonForDenial { get; set; }
    }
}
