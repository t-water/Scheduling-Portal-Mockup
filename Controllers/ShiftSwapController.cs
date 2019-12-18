using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TEServerTest.Models;

namespace TEServerTest.Controllers
{
    public class ShiftSwapController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserShiftRepository userShiftRepository;

        public ShiftSwapController(UserManager<ApplicationUser> userManager, IUserShiftRepository userShiftRepository)
        {
            this.userManager = userManager;
            this.userShiftRepository = userShiftRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleCover(int id)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            var userShift = await userShiftRepository.GetUserShiftForToggleCover(id);

            if(user.Id != userShift.User.Id)
            {
                return NotFound();
            }

            if(userShift.RequestStatus == ShiftSwapStatus.Pending)
            {
                userShift.RequestStatus = ShiftSwapStatus.Available;
            }else if(userShift.RequestStatus == ShiftSwapStatus.Available)
            {
                userShift.RequestStatus = ShiftSwapStatus.Pending;
            }

            await userShiftRepository.Update(userShift);

            return RedirectToAction("index", "home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TakeShift(int id)
        {
            var userShift = userShiftRepository.GetUserShift(id);
            if(userShift == null)
            {
                return NotFound();
            }

            if(userShift.RequestStatus == ShiftSwapStatus.Pending)
            {
                userShift.RequestStatus = ShiftSwapStatus.Taken;
                var user = await userManager.GetUserAsync(HttpContext.User);
                userShift.TakenByID = user.Id;
                await userShiftRepository.Update(userShift);
                return RedirectToAction("pendingRequests", "shiftswap");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> PendingRequests()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if(user == null)
            {
                return NotFound();
            }

            var userShifts = userShiftRepository.GetUserShiftsByStatus(ShiftSwapStatus.Taken)
                                                .Where(x => x.TakenByID == user.Id);

            return View(userShifts);
        }

        [HttpGet]
        public async Task<IActionResult> AvailableShifts()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var availableShifts = userShiftRepository.GetUserShiftsByStatus(ShiftSwapStatus.Pending)
                                                     .Where(x => x.UserID != user.Id);
            return View(availableShifts);
        }

        [HttpGet]
        public IActionResult ApproveRequests()
        {
            var requests = userShiftRepository.GetUserShiftsByStatus(ShiftSwapStatus.Taken);
            return View(requests);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveRequests(int id)
        {
            var userShift = userShiftRepository.GetUserShift(id);

            if(userShift == null)
            {
                return NotFound();
            }

            string takenByID = userShift.TakenByID;
            userShift.UserID = takenByID;
            userShift.TakenByID = null;
            userShift.RequestStatus = ShiftSwapStatus.Available;

            await userShiftRepository.Update(userShift);

            var requests = userShiftRepository.GetUserShiftsByStatus(ShiftSwapStatus.Taken);
            return View(requests);
        }
    }
}