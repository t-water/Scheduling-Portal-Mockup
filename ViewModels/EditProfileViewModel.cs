using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TEServerTest.ViewModels
{
    public class EditProfileViewModel
    {
        public EditProfileViewModel()
        {
            Roles = new List<string>();
        }

        [Required]
        [Display(Name = "First Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First Name must contain only letters")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last Name must contain only letters")]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Display(Name = "Work Study Allowance")]
        [Range(0, 10000)]
        [Required]
        public int WorkStudyAllowance { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [Phone]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone Number must be in format 1234567890")]
        public string PhoneNumber { get; set; }

        public string ID { get; set; }
        public string Email { get; set; }

        public IList<string> Roles { get; set; }

    }
}
