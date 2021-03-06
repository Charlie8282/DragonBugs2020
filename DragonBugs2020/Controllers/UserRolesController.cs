﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonBugs2020.Data;
using DragonBugs2020.Models;
using DragonBugs2020.Models.ViewModels;
using DragonBugs2020.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DragonBugs2020.Controllers
{
    [Authorize]
    public class UserRolesController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IBTRolesService _rolesService;
        private readonly UserManager<BTUser> _userManager;

        public UserRolesController(ApplicationDbContext context, IBTRolesService rolesService, UserManager<BTUser> userManager)
        {
            _context = context;
            _rolesService = rolesService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin, ProjectManager")]
        public async Task<IActionResult> ManageUserRoles()
        {
            List<ManageUserRolesViewModel> model = new List<ManageUserRolesViewModel>();
            List<BTUser> users = _context.Users.ToList();

            foreach (var user in users)
            {
                ManageUserRolesViewModel vm = new ManageUserRolesViewModel();
                vm.User = user;
                var selected = (await _rolesService.ListUserRoles(user)).FirstOrDefault();
                vm.Roles = new SelectList(_context.Roles, "Name", "Name", selected);
                model.Add(vm);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageUserRoles(ManageUserRolesViewModel btuser)
        {
            BTUser user = await _context.Users.FindAsync(btuser.User.Id);

            IEnumerable<string> roles = await _rolesService.ListUserRoles(user);
            await _userManager.RemoveFromRolesAsync(user, roles);
            var userRoles = btuser.SelectedRole;

            foreach (var role in userRoles)
            {
                if (Enum.TryParse(userRoles, out Roles roleValue))
                {
                    await _rolesService.AddUserToRole(user, userRoles);
                    //return RedirectToAction("ManageUserRoles");
                }
            }

            return RedirectToAction("ManageUserRoles");
        }
    }
}
