using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TEServerTest.Models;

namespace TEServerTest.Controllers
{
    public class AvailabilityController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAvailabilityRepository availabilityRepository;

        public AvailabilityController(UserManager<ApplicationUser> userManager, IAvailabilityRepository availabilityRepository)
        {
            this.userManager = userManager;
            this.availabilityRepository = availabilityRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            if (user.Id != id)
            {
                return Forbid();
            }

            if(user == null)
            {
                return NotFound();
            }

            var model = ReturnModelForPreExistingAvailability(id);

            ViewBag.Disabled = true;
            ViewBag.Action = "";
            ViewBag.UserID = id;

            if (model.Any())
            {
                return View(model);
            }

            model = ReturnModelForNoPreExistingAvailability();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            if (user.Id != id)
            {
                return Forbid();
            }

            if (user == null)
            {
                return NotFound();
            }

            var model = ReturnModelForPreExistingAvailability(id);

            
            ViewBag.UserID = id;

            if (model.Any())
            {
                ViewBag.Action = "edit";
                return View(model);
            }

            model = ReturnModelForNoPreExistingAvailability();
            ViewBag.Action = "create";

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<Availability> model, string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user.Id != id)
            {
                return Forbid();
            }

            if (user == null)
            {
                return NotFound();
            }

            foreach (var m in model)
            {
                m.UserID = id;
                await availabilityRepository.Create(m);
            }

            return RedirectToAction("index", "availability",  new { id = id});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(List<Availability> model, string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user.Id != id)
            {
                return Forbid();
            }

            if (user == null)
            {
                return NotFound();
            }

            foreach (var m in model)
            {
                var databaseAvailability = availabilityRepository.GetAvailabilityByAvailabilityID(m.AvailabilityID);
                if (databaseAvailability != null)
                {
                    if(m.IsSelected && !databaseAvailability.IsSelected)
                    {
                        databaseAvailability.IsSelected = true;
                        await availabilityRepository.Update(databaseAvailability);
                    }
                    else if(!m.IsSelected && databaseAvailability.IsSelected)
                    {
                        databaseAvailability.IsSelected = false;
                        await availabilityRepository.Update(databaseAvailability);
                    }
                }
            }

            return RedirectToAction("index", "availability", new { id = id });
        }

        private IEnumerable<Availability> ReturnModelForPreExistingAvailability(string id)
        {
           return availabilityRepository.GetAvailabilitiesByUserID(id);
        }

        private IEnumerable<Availability> ReturnModelForNoPreExistingAvailability()
        {
            var emptyModel = new List<Availability>();

            DateTime time = DateTime.MinValue;
            foreach (DayOfWeek d in Enum.GetValues(typeof(DayOfWeek)))
            {
                Availability availability = new Availability
                {
                    Start = time,
                    End = time.AddHours(8),
                    Day = d
                };
                emptyModel.Add(availability);
            }

            time = time.AddHours(8);
            while (time.Date == DateTime.MinValue.Date)
            {
                foreach (DayOfWeek d in Enum.GetValues(typeof(DayOfWeek)))
                {
                    Availability availability = new Availability
                    {
                        Start = time,
                        End = time.AddMinutes(30),
                        Day = d
                    };
                    emptyModel.Add(availability);
                }
                time = time.AddMinutes(30);
            }

            emptyModel = emptyModel.OrderBy(x => x.Start).ThenBy(x => x.Day).ToList();

            return emptyModel;
        }
    }
}