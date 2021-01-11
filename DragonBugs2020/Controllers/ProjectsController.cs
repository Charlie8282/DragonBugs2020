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
                model = new List<Project>();
                //grab data that I need here
                var userProjects = _context.ProjectUsers.Where(pu => pu.UserId == userId).ToList();

                foreach (var record in userProjects)
                {
                    projectIds.Add(_context.Projects.Find(record.ProjectId).Id);
                }
                foreach (var id in projectIds)
                {
                    var project = _context.Projects.Where(t => t.Id == id).ToList();
                    model.AddRange(project);
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
            else if (User.IsInRole("NewUser"))
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

            project.Tickets = await _context.Tickets
                .Where(t => t.ProjectId == id)
                .Include(t => t.DeveloperUser)
                .Include(t => t.OwnerUser)
                .Include(t => t.Project)
                .Include(t => t.TicketPriority)
                .Include(t => t.TicketStatus)
                .Include(t => t.TicketType)
                .ToListAsync();

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
        public async Task<IActionResult> Create([Bind("Id,Name")] Project project)
        {
            if (!User.IsInRole("Demo"))
            {
                if (ModelState.IsValid)
                {
                    _context.Add(project);
                    await _context.SaveChangesAsync();
                    ProjectUser record = new ProjectUser { UserId = _userManager.GetUserId(User), ProjectId = project.Id };
                    _context.ProjectUsers.Add(record);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Dashboard", "Home");

                }

                return View(project);
            }
            else
            {
                TempData["DemoLockout"] = "Your changes will not be saved.  To make changes to the database please log in as a full user.";

                return RedirectToAction("Dashboard", "Home");


            }
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
            if (!User.IsInRole("Demo"))
            {
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
                return RedirectToAction(nameof(Index));
            }
            return View(project);
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
            if (!User.IsInRole("Demo"))
            {
                model.Project = await project;
                List<BTUser> users = await _context.Users.ToListAsync();
                List<BTUser> members = (List<BTUser>)await _btProjectService.UsersOnProject(id);
                model.Users = new MultiSelectList(users, "Id", "FullName", members);
                return View(model);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, ProjectManager")]
        public async Task<IActionResult> AssignUsers(ManageProjectUsersViewModel model)
        {
            if (!User.IsInRole("Demo"))
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
                        return RedirectToAction("Details", "Projects", new { id = model.Project.Id });
                    }
                    else
                    {

                    }
                    return View(model);

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
            if (!User.IsInRole("Demo"))
            {
                if (ModelState.IsValid)
                {

                    if (model.SelectedUsers != null)
                    {
                        foreach (string id in model.SelectedUsers)
                        {
                            await _btProjectService.RemoveUserFromProject(id, model.Project.Id);
                        }
                        return RedirectToAction("Details", "Projects", new { id = model.Project.Id });
                    }
                    else
                    {
                        return View(model);
                    }
                }
            }
            return RedirectToAction("Details", "Projects", new { id = model.Project.Id });
        }
    }
}
