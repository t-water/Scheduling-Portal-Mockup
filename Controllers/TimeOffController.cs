using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TEServerTest.Models;

namespace TEServerTest.Controllers
{
    public class TimeOffController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITimeOffRepository timeOffRepository;

        public TimeOffController(UserManager<ApplicationUser> userManager, ITimeOffRepository timeOffRepository)
        {
            this.userManager = userManager;
            this.timeOffRepository = timeOffRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            string userID = user.Id;
            var model = new TimeOff
            {
                UserID = userID
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(TimeOff model)
        {
            if (ModelState.IsValid)
            {
                await timeOffRepository.Create(model);
                return RedirectToAction("myrequests", "timeoff");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> MyRequests()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            string userID = user.Id;
            return View(timeOffRepository.GetTimeOffRequestsByUserId(userID));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult ApproveRequests()
        {
            var requests = timeOffRepository.GetTimeOffRequestsByStatus(TimeOffStatus.Pending);
            return View(requests);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveRequests(int id)
        {
            var request = timeOffRepository.GetTimeOffRequestByTimeOffId(id);
            if(request == null)
            {
                return NotFound();
            }
            request.RequestStatus = TimeOffStatus.Approved;
            await timeOffRepository.Update(request);
            var requests = timeOffRepository.GetTimeOffRequestsByStatus(TimeOffStatus.Pending);
            return View(requests);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Details(int id)
        {
            var request = timeOffRepository.GetTimeOffRequestByTimeOffId(id);
            if(request == null)
            {
                return NotFound();
            }

            return View(request);
        }
    }
}