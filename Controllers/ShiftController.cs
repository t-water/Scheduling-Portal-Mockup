using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TEServerTest.Models;
using TEServerTest.Data;
using Microsoft.AspNetCore.Identity;
using TEServerTest.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace TEServerTest.Controllers
{
    public class ShiftController : Controller
    {
        private readonly IShiftRepository shiftRepository;
        private readonly IUserShiftRepository userShiftRepository;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public ShiftController(IShiftRepository shiftRepository, IUserShiftRepository userShiftRepository, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.shiftRepository = shiftRepository;
            this.userShiftRepository = userShiftRepository;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var shifts = shiftRepository.GetShiftsAsync();
            return View(shifts);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ViewResult Create()
        {
            PopulateVenuesDropDownList();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Shift shift)
        {
            if (ModelState.IsValid)
            {
                await shiftRepository.Create(shift);
                return RedirectToAction("index");
            }
            PopulateVenuesDropDownList(shift.VenueID);
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var model = await shiftRepository.GetShiftWithVenueAsync(id);

            if(model == null)
            {
                return NotFound();
            }

            PopulateVenuesDropDownList(model.VenueID);
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Shift shift)
        {
            if (ModelState.IsValid)
            {
                await shiftRepository.Update(shift);
                return RedirectToAction("index");
            }

            return View(shift);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var shift = await shiftRepository.GetShiftWithVenueAsync(id);
            if(shift == null)
            {
                return NotFound();
            }
           
            var userShifts = userShiftRepository.GetUserShiftsByShiftID(id).Include("User").Include("Position");

            var model = new ShiftDetailsViewModel
            {
                UserShifts = userShifts,
                Shift = shift
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> AddStaff(int id)
        {
            var shift = await shiftRepository.GetShiftWithVenueAsync(id);
            if (shift == null)
            {
                return NotFound();
            }

            var model = new List<UserShift>();
            var usersShifts = userShiftRepository.GetUserShiftsByShiftID(id).Include("User");
            foreach(var uS in usersShifts)
            {
                model.Add(uS);
            }
            ViewBag.Shift = shift;

            var roles = roleManager.Roles.Where(x => x.Name != "Web Master");
            ViewBag.Roles = roles;

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStaff(List<UserShift> model, int id)
        {
            var shift = await shiftRepository.GetShiftWithVenueAsync(id);
            if (shift == null)
            {
                return NotFound();
            }
            foreach (var m in model){
                m.ShiftID = shift.ID;
                await userShiftRepository.Create(m);
            }

            return RedirectToAction("details", "shift", new { id = id });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            var shift = await shiftRepository.GetShiftAsync(id);
            if(shift == null)
            {
                return NotFound();
            }

            var userShifts = userShiftRepository.GetUserShiftsByShiftID(id).Include("User").Include("Position");

            var model = new List<DeleteStaffViewModel>();

            foreach(UserShift us in userShifts)
            {
                DeleteStaffViewModel deleteStaffViewModel = new DeleteStaffViewModel
                {
                    UserShiftID = us.UserShiftID,
                    FullName = us.User.FullName,
                    PositionName = us.Position.Name,
                    IsSelected = false
                };
                model.Add(deleteStaffViewModel);
            }

            ViewBag.Shift = shift;
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteStaff(int id, List<DeleteStaffViewModel> model)
        {
            var shift = await shiftRepository.GetShiftAsync(id);
            if(shift == null)
            {
                return NotFound();
            }

            var entriesToDelete = model.Where(x => x.IsSelected == true);

            foreach(var entry in entriesToDelete)
            {
                await userShiftRepository.DeleteUserShiftByID(entry.UserShiftID);
            }

            return RedirectToAction("details", "shift", new { id = id });
        }

        private void PopulateVenuesDropDownList(object selectedVenue = null)
        {
            var venuesQuery = shiftRepository.GetVenuesDropdownQuery();
            ViewBag.VenueID = new SelectList(venuesQuery.AsNoTracking(), "ID", "Name", selectedVenue);
        }

        [HttpGet]
        public async Task<IActionResult> ReturnRoleDropdownPartial(string roleName, int count)
        {
            var users = await userManager.GetUsersInRoleAsync(roleName);
            var position = await roleManager.FindByNameAsync(roleName);
            string positionID = position.Id;
            var model = new PositionSelectListViewModel
            {
                RoleName = roleName,
                Users = users,
                PositionID = positionID,
                Count = count
            };
            return PartialView("~/views/Shift/_PositionSelectList.cshtml", model);
        }

        [Authorize(Roles="Admin")]
        public IActionResult Unstaffed()
        {
            var model = shiftRepository.GetUnstaffedShifts();
            return View(model);
        }
    }
}