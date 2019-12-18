using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TEServerTest.Validation;

namespace TEServerTest.Models
{
    public class Shift
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime Start { get; set; }

        [StartBeforeEnd("Start")]
        public DateTime End { get; set; }

        [Display(Name= "Venue")]
        public int VenueID { get; set; }

        public Venue Venue { get; set; }
    }
}
