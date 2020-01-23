using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TEServerTest.Models;
using TEServerTest.ViewModels;

namespace TEServerTest.Controllers
{
    public class SchedulerController : Controller
    {
        private readonly IShiftRepository shiftRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAvailabilityRepository availabilityRepository;

        public SchedulerController(IShiftRepository shiftRepository,
               UserManager<ApplicationUser> userManager,
               IAvailabilityRepository availabilityRepository)
        {
            this.shiftRepository = shiftRepository;
            this.userManager = userManager;
            this.availabilityRepository = availabilityRepository;
        }

        [HttpGet]
        public IActionResult Index(DateTime Start, DateTime End)
        {
            var model = new SchedulerIndexViewModel
            {
                Start = Start == DateTime.MinValue ? DateTime.Today : Start,
                End = Start == DateTime.MinValue ? DateTime.Today : End
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(SchedulerIndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                var shifts = shiftRepository.GetShiftsInDateRange(model.Start, model.End).ToList();
                if (shifts == null)
                {
                    model.Shifts = new List<Shift>();
                }
                else
                {
                    model.Shifts = shifts;
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult IsAvailable(DateTime Start, DateTime End, string UserID)
        {
            var model = new SchedulerIsAvailableViewModel
            {
                Start = Start == DateTime.MinValue ? DateTime.Today : Start,
                End = Start == DateTime.MinValue ? DateTime.Today : End,
                UserID = String.IsNullOrEmpty(UserID) ? "" : UserID
            };

            PopulateUsersDropDownList();
            ViewBag.Message = model.UserID == "" ? "" : $"{CheckIfUserIsAvailable(UserID, Start, End)}";
            return View(model);
        }

        [HttpGet]
        public IActionResult HMTest(List<SchedulerHMTestViewModel> model)
        {
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HMTest(List<SchedulerIndexViewModel> model)
        {
            var viewModel = new List<SchedulerHMTestViewModel>();
            var users = userManager.Users;
            foreach (var shift in model)
            {
                var currentShift = await shiftRepository.GetShiftAsync(shift.ShiftID);
                var schedulerHMTestViewModel = new SchedulerHMTestViewModel
                {
                    Shift = currentShift,
                };
                foreach (var user in users)
                {
                    if (CheckIfUserIsAvailable(user.Id, currentShift.Start, currentShift.End))
                    {
                        schedulerHMTestViewModel.Users.Add(user);
                    }
                }
                viewModel.Add(schedulerHMTestViewModel);
            }
            return View(viewModel);
        }

        private void PopulateUsersDropDownList()
        {
            ViewBag.UserID = userManager.Users.Select(x => new SelectListItem { Text = x.FullName, Value = x.Id }).ToList().OrderBy(x => x.Text);
        }

        private bool CheckIfUserIsAvailable(string id, DateTime Start, DateTime End)
        {
            if (Start.Hour >= 0 && Start.Hour < 8)
            {
                Start = Start.AddHours(-Start.Hour);
                if (Start.Minute >= 30 && Start.Minute < 60)
                {
                    Start = Start.AddMinutes(-(Start.Minute - 29));
                }
            }
            if (Start.Minute > 0 && Start.Minute < 30)
            {
                Start = Start.AddMinutes(-1 * Start.Minute);
            }
            else if (Start.Minute > 30 && Start.Minute < 60)
            {
                Start = Start.AddMinutes(-(Start.Minute - 30));
            }
            for (DateTime date = Start; date < End; date = date.AddMinutes(30))
            {
                if (!availabilityRepository.CheckIfUserIsAvailable(id, date, date.DayOfWeek))
                {
                    return false;
                }

                if (date.TimeOfDay == DateTime.MinValue.TimeOfDay)
                {
                    date = date.AddHours(7).AddMinutes(30);
                }
            }

            if (!availabilityRepository.CheckIfUserIsNotOff(id, Start, End))
            {
                return false;
            }

            return true;
        }

        private void SupplyHMTestData()
        {

        }
    }
}