using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TEServerTest.Models;
using TEServerTest.ViewModels;

namespace TEServerTest.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Availability()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

/*        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Birthday = model.Birthday,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }*/

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Profile(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if(user == null)
            {
                return NotFound();
            }

            var userRoles = await userManager.GetRolesAsync(user);

            EditProfileViewModel editProfileViewModel = new EditProfileViewModel
            {
                Birthday = user.Birthday,
                WorkStudyAllowance = user.WorkStudyAllowance,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                ID = user.Id,
                Roles = userRoles
            };

            return View(editProfileViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(string id, EditProfileViewModel model)
        {
            if(id != model.ID)
            {
                return NotFound();
            }

            var user = await userManager.FindByIdAsync(model.ID);

            if(user == null)
            {
                return NotFound();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.WorkStudyAllowance = model.WorkStudyAllowance;
                    user.Birthday = model.Birthday;
                    user.PhoneNumber = model.PhoneNumber;

                    var result = await userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        EditProfileViewModel editProfileViewModel = new EditProfileViewModel
                        {
                            Birthday = user.Birthday,
                            WorkStudyAllowance = user.WorkStudyAllowance,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                            ID = user.Id
                        };

                        return View(editProfileViewModel);
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(model);
                }
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ManageRoles(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if(user == null)
            {
                return NotFound();
            }

            var roles = roleManager.Roles;

            var model = new List<ManageRolesViewModel>();

            foreach(IdentityRole role in roles)
            {
                ManageRolesViewModel manageRolesViewModel = new ManageRolesViewModel
                {
                    RoleID = role.Id,
                    RoleName = role.Name,
                };

                if(await userManager.IsInRoleAsync(user, role.Name))
                {
                    manageRolesViewModel.IsSelected = true;
                }
                else
                {
                    manageRolesViewModel.IsSelected = false;
                }

                model.Add(manageRolesViewModel);
            }

            ViewBag.FullName = user.FullName;
            ViewBag.UserID = user.Id;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRoles(List<ManageRolesViewModel> model, string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if(user == null)
            {
                return NotFound();
            }

            for(int i=0; i<model.Count; i++)
            {
                IdentityResult result = null;

                if(model[i].IsSelected && !(await userManager.IsInRoleAsync(user, model[i].RoleName)))
                {
                    result = await userManager.AddToRoleAsync(user, model[i].RoleName);
                }
                else if(!model[i].IsSelected && await userManager.IsInRoleAsync(user, model[i].RoleName))
                {
                    result = await userManager.RemoveFromRoleAsync(user, model[i].RoleName);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if(i < model.Count - 1)
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("Profile", new { id = user.Id });
                    }
                }
            }

            return RedirectToAction("Profile", new { id = user.Id });
        }

        [HttpGet]
        public async Task<IActionResult> ListUsers(int? pageNumber)
        {
            int pageSize = 10;
            var users = userManager.Users.OrderBy(x => x.LastName);
            return View(await PaginatedList<ApplicationUser>.CreateAsync(users, pageNumber ?? 1, pageSize));
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}