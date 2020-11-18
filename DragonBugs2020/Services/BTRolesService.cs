using DragonBugs2020.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DragonBugs2020.Services
{

    public class BTRolesService : IBTRolesService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BTUser> _userManager;
        public BTRolesService(RoleManager<IdentityRole> roleManager, UserManager<BTUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<bool> AddUserToRole(BTUser user, string roleName)
        {
            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.Succeeded;
        }

        public async Task<bool> IsUserInRole(BTUser user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
           
        }

        public async Task<IEnumerable<string>> ListUserRoles(BTUser user)
        {
            return await _userManager.GetRolesAsync(user);
     
        }

        public async Task<bool> RemoveUserFromRole(BTUser user, string roleName)
        {
            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            return result.Succeeded;
        }

        public async Task<ICollection<BTUser>> UsersInRole(string roleName)
        {
            var users = await _userManager.GetUsersInRoleAsync(roleName);
            return users;
            //return await _userManager.Users.Where(u => IsUserInRole(u, role.Name).Result == false);
        }


        //public async Task<ICollection<BTUser>> UsersNotInRole(string roleName)
        //{
        //    var inRole = await _userManager.GetUsersInRoleAsync(roleName);
        //    var users = await _userManager.Users.ToListAsync();
        //    return users.Except(inRole);
        //}


        public async Task<ICollection<BTUser>> UsersNotInRole(IdentityRole role)
        {
            var roleId = await _roleManager.GetRoleIdAsync(role);
            return _userManager.Users.Where(u => IsUserInRole(u, role.Name).Result == false).ToList();
        }
        //public async Task<IEnumerable<BTUser>> UsersNotInRoles(List<string> roleNames)
        //{
        //    List<BTUser> inRole = new List<BTUser>();
        //    foreach (var role in roleNames)
        //    {
        //        inRole.AddRange(await _userManager.GetUsersInRoleAsync(role));
        //    }
        //    var users = await _userManager.Users.ToListAsync();
        //    return users.Except(inRole);
        //}
    }
}
