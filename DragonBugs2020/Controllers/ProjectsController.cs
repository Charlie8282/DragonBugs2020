using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DragonBugs2020.Data;
using DragonBugs2020.Models;
using DragonBugs2020.Services;
using DragonBugs2020.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace DragonBugs2020.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly UserManager<BTUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IBTProjectService _btProjectService;
        public ProjectsController(ApplicationDbContext context, IBTProjectService bTProjectService, UserManager<BTUser> userManager)
        {
            _context = context;
            _btProjectService = bTProjectService;
            _userManager = userManager;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            return View(await _context.Projects.ToListAsync());
        }
        public async Task<IActionResult> MyProjects()
        {
            var model = new List<Project>();
            var userId = _userManager.GetUserId(User);

            if (User.IsInRole("Admin"))
            {
                model = _context.Projects.Include(p => p.ProjectUsers).ToList();
            }

            else if (User.IsInRole("ProjectManager"))
            {
                var projectIds = new List<int>();
                //grab data that I need here
                var userProjects = _context.ProjectUsers.Where(pu => pu.UserId == userId).ToList();

                foreach (var record in userProjects)
                {
                    projectIds.Add(record.ProjectId);
                }
                foreach (var id in projectIds)
                {
                    var project = _context.Projects.Find(id);
                    model.Add(project);
                }
            }

            else if (User.IsInRole("Developer"))
            {
                model = await _btProjectService.ListUserProjects(userId);
                
            }

            else if (User.IsInRole("Submitter"))
            {
                model = await _btProjectService.ListUserProjects(userId);

            }
            else
            {
                return NotFound();
            }
            return View(model);
        }
        //GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.ProjectUsers)
                .ThenInclude(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        [Authorize(Roles = "Admin, ProjectManager")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, ProjectManager")]
        public async Task<IActionResult> Create([Bind("Id,Name,ImagePath,ImageData")] Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Projects/Edit/5
        [Authorize(Roles = "Admin, ProjectManager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, ProjectManager")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImagePath,ImageData")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        [Authorize(Roles = "Admin, ProjectManager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, ProjectManager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, ProjectManager")]
        public async Task<IActionResult> AssignUsers(int id)
        {
            var model = new ManageProjectUsersViewModel();
            var project = _context.Projects
                .Include(p => p.ProjectUsers)
                .ThenInclude(p => p.User)
                .FirstAsync(p => p.Id == id);

            model.Project = await project;
            List<BTUser> users = await _context.Users.ToListAsync();
            List<BTUser> members = (List<BTUser>)await _btProjectService.UsersOnProject(id);
            model.Users = new MultiSelectList(users, "Id", "FullName", members);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, ProjectManager")]
        public async Task<IActionResult> AssignUsers(ManageProjectUsersViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.SelectedUsers != null)
                {
                    var currentMembers = await _context.Projects.Include(p => p.ProjectUsers).FirstOrDefaultAsync(p => p.Id == model.Project.Id);
                    List<string> memberIds = currentMembers.ProjectUsers.Select(u => u.UserId).ToList();
                    memberIds.ForEach(i => _btProjectService.AddUserToProject(i, model.Project.Id));
                    foreach (string id in memberIds)
                    {
                        await _btProjectService.RemoveUserFromProject(id, model.Project.Id);
                    }
                    foreach (string id in model.SelectedUsers)
                    {
                        await _btProjectService.AddUserToProject(id, model.Project.Id);
                    }
                    return RedirectToAction("Details", "Projects", new { id = model.Project.Id});
                 }
                else
                {
                    
                }
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, ProjectManager")]
        public async Task<IActionResult> RemoveUsers(int id)
        {
            var model = new ManageProjectUsersViewModel();
            var project = _context.Projects.Find(id);

            model.Project = project;
            List<BTUser> members = (List<BTUser>)await _btProjectService.UsersOnProject(id);
            model.Users = new MultiSelectList(members, "Id", "FullName");
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, ProjectManager")]
        public async Task<IActionResult> RemoveUsers(ManageProjectUsersViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var project = db.Projects.Find(model.projectId);
                //project.Users.Clear();
                if (model.SelectedUsers != null)
                {
                    foreach (string id in model.SelectedUsers)
                    {
                        await _btProjectService.RemoveUserFromProject(id, model.Project.Id);
                    }
                    //return RedirectToAction("Index", "Projects");

                    return RedirectToAction("Details", "Projects", new { id = model.Project.Id });
                }
                else
                {
                    //Send error message back
                }
            }
            return View(model);
        }


    }
}
