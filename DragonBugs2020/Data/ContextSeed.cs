using DragonBugs2020.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        NewUser,
        Demo
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
            await roleManager.CreateAsync(new IdentityRole(Roles.Demo.ToString()));
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

            //Start demo user seeds
            //Each user occupies a "main" role and this new demo version of the role
            //We will target this Demo role in order to demo this project without giving the abilty to this demo users to access the database
            //Below is the password for all of the demo users
            string demoPassword = "Dragon@2020";
            #region Demo Seed Admin

            defaultAdmin = new BTUser
            {
                UserName = "democharlie@mailinator.com",
                Email = "democharlie@mailinator.com",
                FirstName = "Chaarlie",
                LastName = "Tiincheer",
                EmailConfirmed = true
            };
            try
            {
                var user = await userManager.FindByEmailAsync(defaultAdmin.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultAdmin, demoPassword);
                    await userManager.AddToRoleAsync(defaultAdmin, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultAdmin, Roles.Demo.ToString());
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

            #region Demo Seed Project Manager

            defaultProjectManager = new BTUser
            {
                UserName = "demotopdollar@mailinator.com",
                Email = "demotopdollar@mailinator.com",
                FirstName = "Toop",
                LastName = "Doollar",
                EmailConfirmed = true
            };
            try
            {
                var user = await userManager.FindByEmailAsync(defaultProjectManager.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultProjectManager, demoPassword);
                    await userManager.AddToRoleAsync(defaultProjectManager, Roles.ProjectManager.ToString());
                    await userManager.AddToRoleAsync(defaultProjectManager, Roles.Demo.ToString());
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

            #region Demo Seed Developer

            defaultDeveloper = new BTUser
            {
                UserName = "demodraven@mailinator.com",
                Email = "demodraven@mailinator.com",
                FirstName = "Eriic",
                LastName = "Draaven",
                EmailConfirmed = true
            };
            try
            {
                var user = await userManager.FindByEmailAsync(defaultDeveloper.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultDeveloper, demoPassword);
                    await userManager.AddToRoleAsync(defaultDeveloper, Roles.Developer.ToString());
                    await userManager.AddToRoleAsync(defaultDeveloper, Roles.Demo.ToString());
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

            #region Demo Seed Submitter

            defaultSubmitter = new BTUser
            {
                UserName = "demobrandon@mailinator.com",
                Email = "demobrandon@mailinator.com",
                FirstName = "Braandon",
                LastName = "Leee",
                EmailConfirmed = true
            };
            try
            {
                var user = await userManager.FindByEmailAsync(defaultSubmitter.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultSubmitter, demoPassword);
                    await userManager.AddToRoleAsync(defaultSubmitter, Roles.Submitter.ToString());
                    await userManager.AddToRoleAsync(defaultSubmitter, Roles.Demo.ToString());
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

            #region Demo Seed New User

            defaultNewUser = new BTUser
            {
                UserName = "demowebster@mailinator.com",
                Email = "demowebster@mailinator.com",
                FirstName = "Sheelly",
                LastName = "Weebster",
                EmailConfirmed = true
            };
            try
            {
                var user = await userManager.FindByEmailAsync(defaultNewUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultNewUser, demoPassword);
                    await userManager.AddToRoleAsync(defaultNewUser, Roles.NewUser.ToString());
                    await userManager.AddToRoleAsync(defaultNewUser, Roles.Demo.ToString());
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




        #region TicketType

        public static async Task SeedDefaultTicketTypeAsync(ApplicationDbContext context)
        {
            try
            {
                if (!context.TicketTypes.Any(t => t.Name == "Runtime"))
                {
                    await context.TicketTypes.AddAsync(new TicketType { Name = "Runtime" });
                }
                if (!context.TicketTypes.Any(t => t.Name == "UI"))
                {
                    await context.TicketTypes.AddAsync(new TicketType { Name = "UI" });
                }
                if (!context.TicketTypes.Any(t => t.Name == "Backend"))
                {
                    await context.TicketTypes.AddAsync(new TicketType { Name = "Backend" });
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("*************  ERROR  *************");
                Debug.WriteLine("Error Seeding Ticket Types.");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine("***********************************");
                throw;
            }
        }

        #endregion

        #region TicketPriority

        public static async Task SeedDefaultTicketPriorityAsync(ApplicationDbContext context)
        {
            try
            {
                if (!context.TicketPriorities.Any(t => t.Name == "Low"))
                {
                    await context.TicketPriorities.AddAsync(new TicketPriority { Name = "Low" });
                }
                if (!context.TicketPriorities.Any(t => t.Name == "High"))
                {
                    await context.TicketPriorities.AddAsync(new TicketPriority { Name = "High" });
                }
                if (!context.TicketPriorities.Any(t => t.Name == "Urgent"))
                {
                    await context.TicketPriorities.AddAsync(new TicketPriority { Name = "Urgent" });
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("*************  ERROR  *************");
                Debug.WriteLine("Error Seeding Ticket Priority.");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine("***********************************");
                throw;
            }
        }


        #endregion

        #region Ticket Status

        public static async Task SeedDefaultTicketStatusAsync(ApplicationDbContext context)
        {
            try
            {
                if (!context.TicketStatuses.Any(t => t.Name == "New"))
                {
                    await context.TicketStatuses.AddAsync(new TicketStatus { Name = "New" });
                }
                if (!context.TicketStatuses.Any(t => t.Name == "Open"))
                {
                    await context.TicketStatuses.AddAsync(new TicketStatus { Name = "Open" });
                }
                if (!context.TicketStatuses.Any(t => t.Name == "Closed"))
                {
                    await context.TicketStatuses.AddAsync(new TicketStatus { Name = "Closed" });
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("*************  ERROR  *************");
                Debug.WriteLine("Error Seeding Ticket Status.");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine("***********************************");
                throw;
            }
        }


        #endregion


    }
}

