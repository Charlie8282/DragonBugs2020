using DragonBugs2020.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
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
        public static async Task RunSeedMethods(
            RoleManager<IdentityRole> roleManager,
            UserManager<BTUser> userManager,
            ApplicationDbContext context)
        {
            await SeedRolesAsync(roleManager);
            await SeedDefaultUsersAsync(userManager);
            await SeedDefaultTicketTypeAsync(context);
            await SeedDefaultTicketStatusAsync(context);
            await SeedDefaultTicketPriorityAsync(context);
            await SeedProjectsAsync(context);
            await SeedProjectUsersAsync(context, userManager);
            await SeedTicketsAsync(context, userManager);
        }
            
        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.ProjectManager.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Developer.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Submitter.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.NewUser.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Demo.ToString()));
        }


        private static async Task SeedDefaultUsersAsync(UserManager<BTUser> userManager)
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
                UserName = "kensrue@mailinator.com",
                Email = "kensrue@mailinator.com",
                FirstName = "DustinPM",
                LastName = "Kensrue",
                EmailConfirmed = true
            };
            try
            {
                var user = await userManager.FindByEmailAsync(defaultProjectManager.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultProjectManager, "Kensrue@123");
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

            defaultProjectManager = new BTUser
            {
                UserName = "jakesmith@mailinator.com",
                Email = "jakesmith@mailinator.com",
                FirstName = "JakePM",
                LastName = "Smith",
                EmailConfirmed = true
            };
            try
            {
                var user = await userManager.FindByEmailAsync(defaultProjectManager.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultProjectManager, "Jake@123");
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
                UserName = "bobbylong@mailinator.com",
                Email = "bobbylong@mailinator.com",
                FirstName = "BobbyDev",
                LastName = "Long",
                EmailConfirmed = true
            };
            try
            {
                var user = await userManager.FindByEmailAsync(defaultDeveloper.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultDeveloper, "Bobby@123");
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
                UserName = "chriscornell@mailinator.com",
                Email = "chriscornell@mailinator.com",
                FirstName = "Chris",
                LastName = "Cornell",
                EmailConfirmed = true
            };
            try
            {
                var user = await userManager.FindByEmailAsync(defaultSubmitter.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultSubmitter, "Chris@123");
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
                UserName = "jamestaylor@mailinator.com",
                Email = "jamestaylor@mailinator.com",
                FirstName = "James",
                LastName = "Taylor",
                EmailConfirmed = true
            };
            try
            {
                var user = await userManager.FindByEmailAsync(defaultNewUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultNewUser, "James@123");
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
                FirstName = "CharlieDemo",
                LastName = "Tincher",
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
                UserName = "demokensrue@mailinator.com",
                Email = "demokensrue@mailinator.com",
                FirstName = "DustinDemo",
                LastName = "Kensrue",
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

            defaultProjectManager = new BTUser
            {
                UserName = "demojakesmith@mailinator.com",
                Email = "demojakesmith@mailinator.com",
                FirstName = "JakeDemo",
                LastName = "Smith",
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
                UserName = "demobobbylong@mailinator.com",
                Email = "demobobbylong@mailinator.com",
                FirstName = "BobbyDemo",
                LastName = "Long",
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
                UserName = "demochriscornell@mailinator.com",
                Email = "demochriscornell@mailinator.com",
                FirstName = "ChrisDemo",
                LastName = "Cornell",
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
                UserName = "demojamestaylor@mailinator.com",
                Email = "demojamestaylor@mailinator.com",
                FirstName = "James",
                LastName = "Taylor",
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
        private static async Task SeedDefaultTicketTypeAsync(ApplicationDbContext context)
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
        #region Ticket Status
        private static async Task SeedDefaultTicketStatusAsync(ApplicationDbContext context)
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
        #region TicketPriority
        private static async Task SeedDefaultTicketPriorityAsync(ApplicationDbContext context)
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
        #region Projects
        private static async Task SeedProjectsAsync(ApplicationDbContext context)
        {
            List<Project> projects = new List<Project>();
            Project seedProject1 = new Project
            {
                Name = "Blog Project"
            };
            try
            {
                var project = await context.Projects.FirstOrDefaultAsync(p => p.Name == "Blog Project");
                if (project == null)
                {
                    await context.Projects.AddAsync(seedProject1);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding default Blog Project.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };

            Project seedProject2 = new Project
            {
                Name = "Bug Tracker Project"
            };
            try
            {
                var project = await context.Projects.FirstOrDefaultAsync(p => p.Name == "Bug Tracker Project");
                if (project == null)
                {
                    await context.Projects.AddAsync(seedProject2);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding default Bug Tracker Project.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };

            Project seedProject3 = new Project
            {
                Name = "Financial Portal Project"
            };
            try
            {
                var project = await context.Projects.FirstOrDefaultAsync(p => p.Name == "Financial Portal Project");
                if (project == null)
                {
                    await context.Projects.AddAsync(seedProject3);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding default Financial Portal Project.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };
            #endregion
        }
        private static async Task SeedProjectUsersAsync(ApplicationDbContext context, UserManager<BTUser> userManager)
        {
            string adminId = (await userManager.FindByEmailAsync("democharlie@mailinator.com")).Id;
            string projectManagerId = (await userManager.FindByEmailAsync("demokensrue@mailinator.com")).Id;
            string developerId = (await userManager.FindByEmailAsync("demobobbylong@mailinator.com")).Id;
            string submitterId = (await userManager.FindByEmailAsync("demochriscornell@mailinator.com")).Id;
            int project1Id = (await context.Projects.FirstOrDefaultAsync(p => p.Name == "Blog Project")).Id;
            int project2Id = (await context.Projects.FirstOrDefaultAsync(p => p.Name == "Bug Tracker Project")).Id;
            int project3Id = (await context.Projects.FirstOrDefaultAsync(p => p.Name == "Financial Portal Project")).Id;
            ProjectUser projectUser = new ProjectUser
            {
                UserId = adminId,
                ProjectId = project1Id
            };
            try
            {
                var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == adminId && r.ProjectId == project1Id);
                if (record == null)
                {
                    await context.ProjectUsers.AddAsync(projectUser);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding Admin project 1.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };
            projectUser = new ProjectUser
            {
                UserId = adminId,
                ProjectId = project2Id
            };
            try
            {
                var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == adminId && r.ProjectId == project2Id);
                if (record == null)
                {
                    await context.ProjectUsers.AddAsync(projectUser);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding Admin project 2.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };
            projectUser = new ProjectUser
            {
                UserId = adminId,
                ProjectId = project3Id
            };
            try
            {
                var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == adminId && r.ProjectId == project3Id);
                if (record == null)
                {
                    await context.ProjectUsers.AddAsync(projectUser);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding Admin project 3.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };
            projectUser = new ProjectUser
            {
                UserId = projectManagerId,
                ProjectId = project1Id
            };
            try
            {
                var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == projectManagerId && r.ProjectId == project1Id);
                if (record == null)
                {
                    await context.ProjectUsers.AddAsync(projectUser);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding PM project 1.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };
            projectUser = new ProjectUser
            {
                UserId = projectManagerId,
                ProjectId = project2Id
            };
            try
            {
                var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == projectManagerId && r.ProjectId == project2Id);
                if (record == null)
                {
                    await context.ProjectUsers.AddAsync(projectUser);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding PM project 2.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };
            projectUser = new ProjectUser
            {
                UserId = projectManagerId,
                ProjectId = project3Id
            };
            try
            {
                var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == projectManagerId && r.ProjectId == project3Id);
                if (record == null)
                {
                    await context.ProjectUsers.AddAsync(projectUser);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding PM project 3.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };
            projectUser = new ProjectUser
            {
                UserId = developerId,
                ProjectId = project1Id
            };
            try
            {
                var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == developerId && r.ProjectId == project1Id);
                if (record == null)
                {
                    await context.ProjectUsers.AddAsync(projectUser);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding Developer project 1.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };
            projectUser = new ProjectUser
            {
                UserId = developerId,
                ProjectId = project2Id
            };
            try
            {
                var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == developerId && r.ProjectId == project2Id);
                if (record == null)
                {
                    await context.ProjectUsers.AddAsync(projectUser);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding Developer project 2.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };
            projectUser = new ProjectUser
            {
                UserId = developerId,
                ProjectId = project3Id
            };
            try
            {
                var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == developerId && r.ProjectId == project3Id);
                if (record == null)
                {
                    await context.ProjectUsers.AddAsync(projectUser);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding Developer project 3.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };
            projectUser = new ProjectUser
            {
                UserId = submitterId,
                ProjectId = project1Id
            };
            try
            {
                var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == submitterId && r.ProjectId == project1Id);
                if (record == null)
                {
                    await context.ProjectUsers.AddAsync(projectUser);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding Submitter project 1.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };
            projectUser = new ProjectUser
            {
                UserId = submitterId,
                ProjectId = project2Id
            };
            try
            {
                var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == submitterId && r.ProjectId == project2Id);
                if (record == null)
                {
                    await context.ProjectUsers.AddAsync(projectUser);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding Submitter project 2.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };
            projectUser = new ProjectUser
            {
                UserId = submitterId,
                ProjectId = project3Id
            };
            try
            {
                var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == submitterId && r.ProjectId == project3Id);
                if (record == null)
                {
                    await context.ProjectUsers.AddAsync(projectUser);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding Submitter project 3.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };
        }
        private static async Task SeedTicketsAsync(ApplicationDbContext context, UserManager<BTUser> userManager)
        {
            string submitterId = (await userManager.FindByEmailAsync("demochriscornell@mailinator.com")).Id;
            string developerId = (await userManager.FindByEmailAsync("demobobbylong@mailinator.com")).Id;
            string projectManagerId = (await userManager.FindByEmailAsync("demokensrue@mailinator.com")).Id;
            string adminId = (await userManager.FindByEmailAsync("democharlie@mailinator.com")).Id;
            int project1Id = (await context.Projects.FirstOrDefaultAsync(p => p.Name == "Blog Project")).Id;
            int project2Id = (await context.Projects.FirstOrDefaultAsync(p => p.Name == "Bug Tracker Project")).Id;
            int project3Id = (await context.Projects.FirstOrDefaultAsync(p => p.Name == "Financial Portal Project")).Id;
            int typeId = (await context.TicketTypes.FirstOrDefaultAsync(ts => ts.Name == "UI")).Id;
            int statusId = (await context.TicketStatuses.FirstOrDefaultAsync(ts => ts.Name == "Open")).Id;
            int priorityId = (await context.TicketPriorities.FirstOrDefaultAsync(ts => ts.Name == "High")).Id;

            Ticket ticket = new Ticket
            {
                Title = "Styling Issues",
                Description = "Most of the secondary pages are still in there original scaffolded state.  This is not acceptable for production software.  Please be aware of the changes that need to be made before you do so.",
                Created = DateTimeOffset.Now.AddDays(-20),
                Updated = DateTimeOffset.Now.AddHours(-18),
                ProjectId = project2Id,
                TicketPriorityId = priorityId,
                TicketTypeId = typeId,
                TicketStatusId = statusId,
                DeveloperUserId = developerId,
                OwnerUserId = projectManagerId
            };
            try
            {
                var newTicket = await context.Tickets.FirstOrDefaultAsync(t => t.Title == "Need more blog posts");
                if (newTicket == null)
                {
                    await context.Tickets.AddAsync(ticket);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding Ticket 1.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };

            submitterId = (await userManager.FindByEmailAsync("demochriscornell@mailinator.com")).Id;
            developerId = (await userManager.FindByEmailAsync("demobobbylong@mailinator.com")).Id;
            projectManagerId = (await userManager.FindByEmailAsync("demokensrue@mailinator.com")).Id;
            adminId = (await userManager.FindByEmailAsync("democharlie@mailinator.com")).Id;
            statusId = (await context.TicketStatuses.FirstOrDefaultAsync(ts => ts.Name == "Open")).Id;
            typeId = (await context.TicketTypes.FirstOrDefaultAsync(ts => ts.Name == "Backend")).Id;
            priorityId = (await context.TicketPriorities.FirstOrDefaultAsync(ts => ts.Name == "Urgent")).Id;

            ticket = new Ticket
            {
                Title = "Navbar links missing",
                Description = "Our uses currently cannot move foward from the landing page because the links in the navbar are not present.  It's been requested that you fix this immediately so that our users can gain access to your recent blogs and leave comments that you need to have.",
                Created = DateTimeOffset.Now.AddDays(-22),
                Updated = DateTimeOffset.Now.AddHours(-20),
                ProjectId = project1Id,
                TicketPriorityId = priorityId,
                TicketTypeId = typeId,
                TicketStatusId = statusId,
                DeveloperUserId = developerId,
                OwnerUserId = submitterId
            };
            try
            {
                var newTicket = await context.Tickets.FirstOrDefaultAsync(t => t.Title == "Navbar links missing");
                if (newTicket == null)
                {
                    await context.Tickets.AddAsync(ticket);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding Ticket 2.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };

            submitterId = (await userManager.FindByEmailAsync("demochriscornell@mailinator.com")).Id;
            developerId = (await userManager.FindByEmailAsync("demobobbylong@mailinator.com")).Id;
            projectManagerId = (await userManager.FindByEmailAsync("demokensrue@mailinator.com")).Id;
            adminId = (await userManager.FindByEmailAsync("democharlie@mailinator.com")).Id;
            statusId = (await context.TicketStatuses.FirstOrDefaultAsync(ts => ts.Name == "New")).Id;
            typeId = (await context.TicketTypes.FirstOrDefaultAsync(ts => ts.Name == "Runtime")).Id;
            priorityId = (await context.TicketPriorities.FirstOrDefaultAsync(ts => ts.Name == "Urgent")).Id;

            ticket = new Ticket
            {
                Title = "Security issues",
                Description = "New Users still have access to edit and delete buttons in the tickets section.  They should only be able to view tickets and make commments on them as well.  Probably best to remove from the project all delete actions because by spec, no one can delete a ticket or project, only archive them.",
                Created = DateTimeOffset.Now.AddDays(-30),
                Updated = DateTimeOffset.Now.AddHours(-28),
                ProjectId = project1Id,
                TicketPriorityId = priorityId,
                TicketTypeId = typeId,
                TicketStatusId = statusId,
                DeveloperUserId = developerId,
                OwnerUserId = adminId
            };
            try
            {
                var newTicket = await context.Tickets.FirstOrDefaultAsync(t => t.Title == "Security issues");
                if (newTicket == null)
                {
                    await context.Tickets.AddAsync(ticket);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding Ticket 3.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };

            submitterId = (await userManager.FindByEmailAsync("demochriscornell@mailinator.com")).Id;
            developerId = (await userManager.FindByEmailAsync("demobobbylong@mailinator.com")).Id;
            projectManagerId = (await userManager.FindByEmailAsync("demokensrue@mailinator.com")).Id;
            adminId = (await userManager.FindByEmailAsync("democharlie@mailinator.com")).Id;
            statusId = (await context.TicketStatuses.FirstOrDefaultAsync(ts => ts.Name == "Open")).Id;
            typeId = (await context.TicketTypes.FirstOrDefaultAsync(ts => ts.Name == "Runtime")).Id;
            priorityId = (await context.TicketPriorities.FirstOrDefaultAsync(ts => ts.Name == "Low")).Id;

            ticket = new Ticket
            {
                Title = "Too much loading",
                Description = "We have complaints that the landing page is taking an unusually long time to load.  Can you please look into this?",
                Created = DateTimeOffset.Now.AddDays(-32),
                Updated = DateTimeOffset.Now.AddHours(-30),
                ProjectId = project3Id,
                TicketPriorityId = priorityId,
                TicketTypeId = typeId,
                TicketStatusId = statusId,
                DeveloperUserId = developerId,
                OwnerUserId = submitterId
            };
            try
            {
                var newTicket = await context.Tickets.FirstOrDefaultAsync(t => t.Title == "Finish styling");
                if (newTicket == null)
                {
                    await context.Tickets.AddAsync(ticket);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding Ticket 4.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };
        }
    }
}

