using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TEServerTest.Models;
using TEServerTest.ViewModels;

namespace TEServerTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserShiftRepository userShiftRepository;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, IUserShiftRepository userShiftRepository)
        {
            _logger = logger;
            this.userManager = userManager;
            this.userShiftRepository = userShiftRepository;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if(user != null)
            {
                List<HomeViewModel> model = new List<HomeViewModel>();
                string userID = user.Id;
                var userShifts = userShiftRepository.GetUserShiftsForHomePage(userID);

                DateTime today = DateTime.Now.Date;

                var todaysShifts = userShifts.Where(x => x.UserStart.Date == today);
                var futureShifts = userShifts.Where(x => x.UserStart.Date > today);
                var pastShifts = userShifts.Where(x => x.UserStart.Date < today);

                model.Add(new HomeViewModel { UserShifts = todaysShifts, NoShiftsMessage = "No Shifts Today", Label = "Today's Shifts" });
                model.Add(new HomeViewModel { UserShifts = futureShifts, NoShiftsMessage = "No Upcoming Shifts", Label = "Future Shifts" });
                model.Add(new HomeViewModel { UserShifts = pastShifts, ShowShiftSwapButton = false, Label = "Past Shifts", NoShiftsMessage = "No Shifts Yet" });

                return View(model);
            }
            return View();
            
        }
    }
}
