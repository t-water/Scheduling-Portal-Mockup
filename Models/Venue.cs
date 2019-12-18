using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TEServerTest.Models
{
    public class Venue
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(5)]
        [RegularExpression(@"^[A-Z]+$", ErrorMessage = "Abbreviation Must Be All Caps")]
        public string Abbreviation { get; set; }

        [Range(0, 100)]
        [Display(Name = "Executive House Managers")]
        public int DefaultExecutiveHouseManagers { get; set; }

        [Range(0, 100)]
        [Display(Name = "House Managers")]
        public int DefaultHouseManagers { get; set; }

        [Range(0, 100)]
        [Display(Name = "Floor Managers")]
        public int DefaultFloorManagers { get; set; }

        [Range(0, 100)]
        [Display(Name = "Ushers")]
        public int DefaultEventUshers { get; set; }

        [Range(0, 100)]
        [Display(Name = "Concessions Managers")]
        public int DefaultConcessionsManagers { get; set; }

        [Range(0, 100)]
        [Display(Name = "UV Liaisons")]
        public int DefaultCommunityRoomLiaisons { get; set; }

    }
}
