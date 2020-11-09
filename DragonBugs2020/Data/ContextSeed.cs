using DragonBugs2020.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DragonBugs2020.Data
{
    public enum Roles
    {
        Admin,
        ProjectManager,
        Developer,
        Submitter,
        NewUser
    }

    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.ProjectManager.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Developer.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Submitter.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.NewUser.ToString()));
        }

        
        public static async Task SeedDefaultUsersAsync(UserManager<BTUser> userManager)
        {
            #region Seed Admin
          
            var defaultAdmin = new BTUser
            {
                UserName = "charlie@mailinator.com",
                Email = "charlie@mailinator.com",
                FirstName = "Charlie",
                LastName = "Tincher",
                EmailConfirmed = true
            };
            try
            {
                var user = await userManager.FindByEmailAsync(defaultAdmin.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultAdmin, "Charlie@123");
                    await userManager.AddToRoleAsync(defaultAdmin, Roles.Admin.ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding Default Admin User.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");

            }
            #endregion

            #region Seed Project Manager
          
            var defaultProjectManager = new BTUser
            {
                UserName = "topdollar@mailinator.com",
                Email = "topdollar@mailinator.com",
                FirstName = "Top",
                LastName = "Dollar",
                EmailConfirmed = true
            };
            try
            {
                var user = await userManager.FindByEmailAsync(defaultProjectManager.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultProjectManager, "TopDollar@123");
                    await userManager.AddToRoleAsync(defaultProjectManager, Roles.ProjectManager.ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding Default Project Manager User.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");

            }
            #endregion

            #region Seed Developer

            var defaultDeveloper = new BTUser
            {
                UserName = "draven@mailinator.com",
                Email = "draven@mailinator.com",
                FirstName = "Eric",
                LastName = "Draven",
                EmailConfirmed = true
            };
            try
            {
                var user = await userManager.FindByEmailAsync(defaultDeveloper.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultDeveloper, "Eric@123");
                    await userManager.AddToRoleAsync(defaultDeveloper, Roles.Developer.ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding Default Developer User.");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine("*******************************");

            }
            #endregion

            #region Seed Submitter

            var defaultSubmitter = new BTUser
            {
                UserName = "brandon@mailinator.com",
                Email = "brandon@mailinator.com",
                FirstName = "Brandon",
                LastName = "Lee",
                EmailConfirmed = true
            };
            try
            {
                var user = await userManager.FindByEmailAsync(defaultSubmitter.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultSubmitter, "Brandon@123");
                    await userManager.AddToRoleAsync(defaultSubmitter, Roles.Submitter.ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding Default Submitter User.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");

            }
            #endregion

            #region Seed New User

            var defaultNewUser = new BTUser
            {
                UserName = "webster@mailinator.com",
                Email = "webster@mailinator.com",
                FirstName = "Shelly",
                LastName = "Webster",
                EmailConfirmed = true
            };
            try
            {
                var user = await userManager.FindByEmailAsync(defaultNewUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultNewUser, "Shelly@123");
                    await userManager.AddToRoleAsync(defaultNewUser, Roles.NewUser.ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding Default New User.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");

            }
            #endregion
        }
        //public static async Task SeedTicketInfo(TicketManager<Ticket> ticketManager)
        //{
        //    await ticketManager.CreateAsync(new Ticket(Type.)
        //}
    }
}
