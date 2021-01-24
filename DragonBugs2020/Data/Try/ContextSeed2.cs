using DragonBugs2020.Models;
using DragonBugs2020.Services;
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
        ProjectManager2,
        Developer,
        Developer2,
        Submitter,
        Submitter2,
        Submitter3,
        NewUser,
        Demo
    }

    public static class ContextSeed
    {
        public static async Task RunSeedMethods(
            RoleManager<IdentityRole> roleManager,
            UserManager<BTUser> userManager,
            ApplicationDbContext context,
            IImageService imageService)
        {
            await SeedRolesAsync(roleManager);
            await SeedDefaultUsersAsync(userManager, imageService);
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


        private static async Task SeedDefaultUsersAsync(UserManager<BTUser> userManager, IImageService imageService)
        {
            #region Seed Admin

            var defaultAdmin = new BTUser
            {
                UserName = "charlie@mailinator.com",
                Email = "charlie@mailinator.com",
                FirstName = "Charlie",
                LastName = "Tincher",
                FileName = "defaultavatar.jpg",
                FileData = await imageService.AssignAvatarAsync("defaultavatar.jpg"),
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
                FileName = "defaultavatar.jpg",
                FileData = await imageService.AssignAvatarAsync("defaultavatar.jpg"),
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
                FileName = "defaultavatar.jpg",
                FileData = await imageService.AssignAvatarAsync("defaultavatar.jpg"),
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
                FileName = "defaultavatar.jpg",
                FileData = await imageService.AssignAvatarAsync("defaultavatar.jpg"),
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
                FileName = "defaultavatar.jpg",
                FileData = await imageService.AssignAvatarAsync("defaultavatar.jpg"),
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
            //Anthony Stark
            #region Demo Seed Admin

            defaultAdmin = new BTUser
            {
                UserName = "anthony@mailinator.com",
                Email = "anthony@mailinator.com",
                FirstName = "Anthony",
                LastName = "Stark",
                FileName = "stark.jpg",
                FileData = await imageService.AssignAvatarAsync("stark.jpg"),
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
            //Harley Keener
            #region Demo Seed Project Manager

            defaultProjectManager = new BTUser
            {
                UserName = "harley@mailinator.com",
                Email = "harley@mailinator.com",
                FirstName = "Harley",
                LastName = "Keener",
                FileName = "harley-keener.jpg",
                FileData = await imageService.AssignAvatarAsync("harley-keener.jpg"),
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

            //second PM
            defaultProjectManager = new BTUser
            {
                UserName = "luis@mailinator.com",
                Email = "luis@mailinator.com",
                FirstName = "Luis",
                LastName = "Pena",
                FileName = "luis-pena.jpg",
                FileData = await imageService.AssignAvatarAsync("luis-pena.jpg"),
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
                Debug.WriteLine("Error Seeding 2nd Default Project Manager User.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");

            }

            #endregion
            //Carol Danvers
            #region Demo Seed Developer

            defaultDeveloper = new BTUser
            {
                UserName = "carol@mailinator.com",
                Email = "carol@mailinator.com",
                FirstName = "Carol",
                LastName = "Danvers",
                FileName = "carol.png",
                FileData = await imageService.AssignAvatarAsync("carol.png"),
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

            //2nd developer
            defaultDeveloper = new BTUser
            {
                UserName = "chris@mailinator.com",
                Email = "chris@mailinator.com",
                FirstName = "Chris",
                LastName = "Thunder",
                FileName = "chris-thunder.png",
                FileData = await imageService.AssignAvatarAsync("chris-thunder.png"),
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
                Debug.WriteLine("Error Seeding 2nd Default Developer User.");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine("*******************************");

            }
            #endregion
            //Clint Barton
            #region Demo Seed Submitter

            defaultSubmitter = new BTUser
            {
                UserName = "clint@mailinator.com",
                Email = "clint@mailinator.com",
                FirstName = "Clint",
                LastName = "Barton",
                FileName = "clint.jpg",
                FileData = await imageService.AssignAvatarAsync("clint.jpg"),
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

            //2nd submitter
            defaultSubmitter = new BTUser
            {
                UserName = "paul@mailinator.com",
                Email = "paul@mailinator.com",
                FirstName = "Paul",
                LastName = "Bettany",
                FileName = "paul-bettany.jpg",
                FileData = await imageService.AssignAvatarAsync("paul-bettany.jpg"),
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
                Debug.WriteLine("Error Seeding 2nd Default Submitter User.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");

            }

            //3rd submitter
            defaultSubmitter = new BTUser
            {
                UserName = "shuri@mailinator.com",
                Email = "shuri@mailinator.com",
                FirstName = "Shuri",
                LastName = "Wright",
                FileName = "shuri-wright.jpg",
                FileData = await imageService.AssignAvatarAsync("shuri-wright.jpg"),
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
                Debug.WriteLine("Error Seeding 3rd Default Submitter User.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");

            }

            #endregion
            //James Gunn
            #region Demo Seed New User

            defaultNewUser = new BTUser
            {
                UserName = "james@mailinator.com",
                Email = "james@mailinator.com",
                FirstName = "James",
                LastName = "Gunn",
                FileName = "james.jpg",
                FileData = await imageService.AssignAvatarAsync("james.jpg"),
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
        }
        #endregion
        
        private static async Task SeedProjectUsersAsync(ApplicationDbContext context, UserManager<BTUser> userManager)
        {
            string adminId = (await userManager.FindByEmailAsync("anthony@mailinator.com")).Id;
            string projectManagerId = (await userManager.FindByEmailAsync("harley@mailinator.com")).Id;
            string projectManager2Id = (await userManager.FindByEmailAsync("luis@mailinator.com")).Id;
            string developerId = (await userManager.FindByEmailAsync("carol@mailinator.com")).Id;
            string developer2Id = (await userManager.FindByEmailAsync("chris@mailinator.com")).Id;
            string submitterId = (await userManager.FindByEmailAsync("clint@mailinator.com")).Id;
            string submitter2Id = (await userManager.FindByEmailAsync("paul@mailinator.com")).Id;
            string submitter3Id = (await userManager.FindByEmailAsync("shuri@mailinator.com")).Id;
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

            //2nd PM
            projectUser = new ProjectUser
            {
                UserId = projectManager2Id,
                ProjectId = project1Id
            };
            try
            {
                var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == projectManager2Id && r.ProjectId == project1Id);
                if (record == null)
                {
                    await context.ProjectUsers.AddAsync(projectUser);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding PM2 project 1.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };
            projectUser = new ProjectUser
            {
                UserId = projectManager2Id,
                ProjectId = project2Id
            };
            try
            {
                var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == projectManager2Id && r.ProjectId == project2Id);
                if (record == null)
                {
                    await context.ProjectUsers.AddAsync(projectUser);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding PM2 project 2.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };
            projectUser = new ProjectUser
            {
                UserId = projectManager2Id,
                ProjectId = project3Id
            };
            try
            {
                var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == projectManager2Id && r.ProjectId == project3Id);
                if (record == null)
                {
                    await context.ProjectUsers.AddAsync(projectUser);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding PM2 project 3.");
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

            //2nd developer
            projectUser = new ProjectUser
            {
                UserId = developer2Id,
                ProjectId = project1Id
            };
            try
            {
                var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == developer2Id && r.ProjectId == project1Id);
                if (record == null)
                {
                    await context.ProjectUsers.AddAsync(projectUser);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding Developer2 project 1.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };
            projectUser = new ProjectUser
            {
                UserId = developer2Id,
                ProjectId = project2Id
            };
            try
            {
                var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == developer2Id && r.ProjectId == project2Id);
                if (record == null)
                {
                    await context.ProjectUsers.AddAsync(projectUser);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding Developer2 project 2.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };
            projectUser = new ProjectUser
            {
                UserId = developer2Id,
                ProjectId = project3Id
            };
            try
            {
                var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == developer2Id && r.ProjectId == project3Id);
                if (record == null)
                {
                    await context.ProjectUsers.AddAsync(projectUser);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding Developer2 project 3.");
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

            //2nd submitter
            projectUser = new ProjectUser
            {
                UserId = submitter2Id,
                ProjectId = project1Id
            };
            try
            {
                var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == submitter2Id && r.ProjectId == project1Id);
                if (record == null)
                {
                    await context.ProjectUsers.AddAsync(projectUser);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding Submitter2 project 1.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };
            projectUser = new ProjectUser
            {
                UserId = submitter2Id,
                ProjectId = project2Id
            };
            try
            {
                var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == submitter2Id && r.ProjectId == project2Id);
                if (record == null)
                {
                    await context.ProjectUsers.AddAsync(projectUser);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding Submitter2 project 2.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };
            projectUser = new ProjectUser
            {
                UserId = submitter2Id,
                ProjectId = project3Id
            };
            try
            {
                var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == submitter2Id && r.ProjectId == project3Id);
                if (record == null)
                {
                    await context.ProjectUsers.AddAsync(projectUser);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding Submitter2 project 3.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };

            //3rd submitter
            projectUser = new ProjectUser
            {
                UserId = submitter3Id,
                ProjectId = project1Id
            };
            try
            {
                var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == submitter3Id && r.ProjectId == project1Id);
                if (record == null)
                {
                    await context.ProjectUsers.AddAsync(projectUser);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding Submitter3 project 1.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };
            projectUser = new ProjectUser
            {
                UserId = submitter3Id,
                ProjectId = project2Id
            };
            try
            {
                var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == submitter3Id && r.ProjectId == project2Id);
                if (record == null)
                {
                    await context.ProjectUsers.AddAsync(projectUser);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding Submitter3 project 2.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };
            projectUser = new ProjectUser
            {
                UserId = submitter3Id,
                ProjectId = project3Id
            };
            try
            {
                var record = await context.ProjectUsers.FirstOrDefaultAsync(r => r.UserId == submitter3Id && r.ProjectId == project3Id);
                if (record == null)
                {
                    await context.ProjectUsers.AddAsync(projectUser);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding Submitter3 project 3.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };
        }
      
       
        private static async Task SeedTicketsAsync(ApplicationDbContext context, UserManager<BTUser> userManager)
        {
            string submitterId = (await userManager.FindByEmailAsync("clint@mailinator.com")).Id;
            string submitter2Id = (await userManager.FindByEmailAsync("paul@mailinator.com")).Id;
            string submitter3Id = (await userManager.FindByEmailAsync("shuri@mailinator.com")).Id;
            string developerId = (await userManager.FindByEmailAsync("carol@mailinator.com")).Id;
            string developer2Id = (await userManager.FindByEmailAsync("chris@mailinator.com")).Id;
            string projectManagerId = (await userManager.FindByEmailAsync("harley@mailinator.com")).Id;
            string projectManager2Id = (await userManager.FindByEmailAsync("luis@mailinator.com")).Id;
            string adminId = (await userManager.FindByEmailAsync("anthony@mailinator.com")).Id;
            int project1Id = (await context.Projects.FirstOrDefaultAsync(p => p.Name == "Blog Project")).Id;
            int project2Id = (await context.Projects.FirstOrDefaultAsync(p => p.Name == "Bug Tracker Project")).Id;
            int project3Id = (await context.Projects.FirstOrDefaultAsync(p => p.Name == "Financial Portal Project")).Id;
            int type1Id = (await context.TicketTypes.FirstOrDefaultAsync(ts => ts.Name == "UI")).Id;
            int type2Id = (await context.TicketTypes.FirstOrDefaultAsync(ts => ts.Name == "Backend")).Id;
            int type3Id = (await context.TicketTypes.FirstOrDefaultAsync(ts => ts.Name == "Runtime")).Id;
            int status1Id = (await context.TicketStatuses.FirstOrDefaultAsync(ts => ts.Name == "New")).Id;
            int status2Id = (await context.TicketStatuses.FirstOrDefaultAsync(ts => ts.Name == "Open")).Id;
            int status3Id = (await context.TicketStatuses.FirstOrDefaultAsync(ts => ts.Name == "Closed")).Id;
            int priority1Id = (await context.TicketPriorities.FirstOrDefaultAsync(ts => ts.Name == "Low")).Id;
            int priority2Id = (await context.TicketPriorities.FirstOrDefaultAsync(ts => ts.Name == "High")).Id;
            int priority3Id = (await context.TicketPriorities.FirstOrDefaultAsync(ts => ts.Name == "Urgent")).Id;

            Ticket ticket = new Ticket
            {
                Title = "Styling Issues",
                Description = "It's come to my attention that most of the secondary (and less used) views on Charlie's Blog have not been styled to match the rest of the finished views.  Focus group #1 - take care of the Blog secondary views.  Focus group #2 - take care of Post secondary views.  Please comment here if you have further questions on this issue.",
                Created = DateTimeOffset.Now.AddDays(-20),
                Updated = DateTimeOffset.Now.AddHours(-18),
                ProjectId = project1Id,
                TicketTypeId = type1Id,
                TicketStatusId = status1Id,
                TicketPriorityId = priority1Id,
                DeveloperUserId = developer2Id,
                OwnerUserId = projectManagerId
            };
            try
            {
                var newTicket = await context.Tickets.FirstOrDefaultAsync(t => t.Title == "Styling Issues");
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

            //ticket 2

            ticket = new Ticket
            {
                Title = "Content Missing",
                Description = "There are not as many posts as the users were expecting to see on The Blog application.  From what I understand there are plenty of posts available but they just aren't posted to the blog application?",
                Created = DateTimeOffset.Now.AddDays(-21),
                Updated = DateTimeOffset.Now.AddHours(-21),
                ProjectId = project1Id,
                TicketTypeId = type3Id,
                TicketStatusId = status1Id,
                TicketPriorityId = priority1Id,
                DeveloperUserId = developerId,
                OwnerUserId = submitterId
            };
            try
            {
                var newTicket = await context.Tickets.FirstOrDefaultAsync(t => t.Title == "Content Missing");
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


            //ticket 3
            ticket = new Ticket
            {
                Title = "Ticket Creation",
                Description = "Users have brought it to my attention that when creating a ticket as a PM, they are allowed to select projects that they are not assigned to.  Is there some way that we can adjust the UI so that tickets can only be created from the project details view and remove the option to choose the project?",
                Created = DateTimeOffset.Now.AddDays(-22),
                Updated = DateTimeOffset.Now.AddHours(-18),
                ProjectId = project2Id,
                TicketTypeId = type1Id,
                TicketStatusId = status2Id,
                TicketPriorityId = priority2Id,
                DeveloperUserId = developer2Id,
                OwnerUserId = submitter2Id
            };
            try
            {
                var newTicket = await context.Tickets.FirstOrDefaultAsync(t => t.Title == "Ticket Creation");
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

            //ticket4
            ticket = new Ticket
            {
                Title = "Security issues",
                Description = "New Users still have access to edit and delete buttons in the tickets section.  They should only be able to view tickets and make commments on them as well.  Probably best to remove from the project all delete actions because by spec, no one can delete a ticket or project, only archive them.",
                Created = DateTimeOffset.Now.AddDays(-23),
                Updated = DateTimeOffset.Now.AddHours(-12),
                ProjectId = project2Id,
                TicketTypeId = type2Id,
                TicketStatusId = status3Id,
                TicketPriorityId = priority3Id,
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
                Debug.WriteLine("Error Seeding Ticket 4.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };

            //ticket5
            ticket = new Ticket
            {
                Title = "Expand Email Notifications",
                Description = "Currently, users are only getting notifications when they are assigned as a developer on a ticket.  Can this be expanded to alerting users when they have a change in role and when a ticket they have submitted is placed from new to open status?",
                Created = DateTimeOffset.Now.AddDays(-22),
                Updated = DateTimeOffset.Now.AddHours(-22),
                ProjectId = project2Id,
                TicketTypeId = type2Id,
                TicketStatusId = status2Id,
                TicketPriorityId = priority3Id,
                DeveloperUserId = developerId,
                OwnerUserId = projectManager2Id
            };
            try
            {
                var newTicket = await context.Tickets.FirstOrDefaultAsync(t => t.Title == "Expand Email Notifications");
                if (newTicket == null)
                {
                    await context.Tickets.AddAsync(ticket);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding Ticket 5.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };

            //ticket6
            ticket = new Ticket
            {
                Title = "Too Much Loading",
                Description = "We have complaints that the landing page is taking an unusually long time to load.  Can you please look into this?",
                Created = DateTimeOffset.Now.AddDays(-24),
                Updated = DateTimeOffset.Now.AddHours(-30),
                ProjectId = project3Id,
                TicketTypeId = type3Id,
                TicketStatusId = status2Id,
                TicketPriorityId = priority3Id,
                DeveloperUserId = developer2Id,
                OwnerUserId = submitter3Id
            };
            try
            {
                var newTicket = await context.Tickets.FirstOrDefaultAsync(t => t.Title == "Too Much Loading");
                if (newTicket == null)
                {
                    await context.Tickets.AddAsync(ticket);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding Ticket 6.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };

            //ticket7
            ticket = new Ticket
            {
                Title = "Modals Not Styled",
                Description = "The modals on the dashboard are not styled in such a way to match the overall theme of the application.  They should have a darker background and similar text as that found on the dashboard and landing.",
                Created = DateTimeOffset.Now.AddDays(-23),
                Updated = DateTimeOffset.Now.AddHours(-30),
                ProjectId = project3Id,
                TicketTypeId = type1Id,
                TicketStatusId = status3Id,
                TicketPriorityId = priority1Id,
                DeveloperUserId = developerId,
                OwnerUserId = projectManagerId
            };
            try
            {
                var newTicket = await context.Tickets.FirstOrDefaultAsync(t => t.Title == "Modals Not Styled");
                if (newTicket == null)
                {
                    await context.Tickets.AddAsync(ticket);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding Ticket 7.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };

            //ticket8
            ticket = new Ticket
            {
                Title = "Modal Not Populating",
                Description = "The Create Category Item modal on the dashboard is not populating, therefor the users cannot interact with this feature.",
                Created = DateTimeOffset.Now.AddDays(-23),
                Updated = DateTimeOffset.Now.AddHours(-33),
                ProjectId = project3Id,
                TicketTypeId = type2Id,
                TicketStatusId = status1Id,
                TicketPriorityId = priority2Id,
                DeveloperUserId = developer2Id,
                OwnerUserId = submitter3Id
            };
            try
            {
                var newTicket = await context.Tickets.FirstOrDefaultAsync(t => t.Title == "Modal Not Populating");
                if (newTicket == null)
                {
                    await context.Tickets.AddAsync(ticket);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("************ ERROR ************");
                Debug.WriteLine("Error Seeding Ticket 8.");
                Debug.WriteLine("ex.Message");
                Debug.WriteLine("*******************************");
            };
        }
    }
}

