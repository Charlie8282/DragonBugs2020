using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DragonBugs2020.Data;
using DragonBugs2020.Models;
using Microsoft.AspNetCore.Identity;
using DragonBugs2020.Models.ViewModels;
using DragonBugs2020.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace DragonBugs2020.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBTProjectService _projectService;
        private readonly UserManager<BTUser> _userManager;
        private readonly IBTHistoriesService _historiesService;
        private readonly IBTAccessService _accessService;
        public TicketsController(ApplicationDbContext context, IBTProjectService projectService, UserManager<BTUser> userManager, IBTHistoriesService historiesService, IBTAccessService accessService)
        {
            _context = context;
            _projectService = projectService;
            _userManager = userManager;
            _historiesService = historiesService;
            _accessService = accessService;
        }

        // GET: Tickets
        public async Task<IActionResult> Index(string userId)
        {
            var projectIds = new List<int>();
            var model = new List<Ticket>();
            var userProjects = _context.ProjectUsers.Where(pu => pu.UserId == userId).ToList();
            foreach (var record in userProjects)
            {
                projectIds.Add(_context.Projects.Find(record.ProjectId).Id);

            }
            foreach (var id in projectIds)
            {
                var tickets = _context.Tickets.Where(t => t.ProjectId == id).ToList();
                model.AddRange(tickets);
            }

            var applicationDbContext = _context.Tickets
                .Include(t => t.DeveloperUser)
                .Include(t => t.OwnerUser)
                .Include(t => t.Project)
                .Include(t => t.TicketPriority)
                .Include(t => t.TicketStatus)
                .Include(t => t.TicketType);
            return View(await applicationDbContext.ToListAsync());
        }


        public async Task<IActionResult> MyTickets()
        {
            var model = new List<Ticket>();
            var userId = _userManager.GetUserId(User);

            if (User.IsInRole("Admin"))
            {
                model = _context.Tickets
                    .Include(t => t.DeveloperUser)
                    .Include(t => t.OwnerUser)
                    .Include(t => t.Project)
                    .Include(t => t.TicketPriority)
                    .Include(t => t.TicketStatus)
                    .Include(t => t.TicketType).ToList();
            }
            else if (User.IsInRole("ProjectManager"))
            {

                //var projects = await _projectService.ListUserProjects(userId);
                //model = projects.SelectMany(t => t.Tickets).ToList();
            }
            else if (User.IsInRole("Developer"))
            {
                model = _context.Tickets
                    .Where(t => t.DeveloperUserId == userId)
                    .Include(t => t.OwnerUser)
                    .Include(t => t.Project)
                    .Include(t => t.TicketPriority)
                    .Include(t => t.TicketStatus)
                    .Include(t => t.TicketType).ToList();
            }
            else if (User.IsInRole("Submitter"))
            {
                model = _context.Tickets
                    .Where(t => t.OwnerUserId == userId)
                    .Include(t => t.OwnerUser)
                    .Include(t => t.Project)
                    .Include(t => t.TicketPriority)
                    .Include(t => t.TicketStatus)
                    .Include(t => t.TicketType).ToList();
            }
            else if (User.IsInRole("NewUser"))
            {
                model = _context.Tickets
                    .Where(t => t.OwnerUserId == userId)
                    .Include(t => t.OwnerUser)
                    .Include(t => t.Project)
                    .Include(t => t.TicketPriority)
                    .Include(t => t.TicketStatus)
                    .Include(t => t.TicketType).ToList();
            }
            else
            {
                return NotFound();
            }
            return View(model);
        }
        // GET: Tickets/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = _userManager.GetUserId(User);
            var roleName = _userManager.GetRolesAsync(await _userManager.GetUserAsync(User)).Result.FirstOrDefault();
            if (await _accessService.CanInteractTicket(userId, (int)id, roleName))
            {
                var ticket = await _context.Tickets
                               .Include(t => t.TicketPriority)
                               .Include(t => t.TicketStatus)
                               .Include(t => t.TicketType)
                               .Include(t => t.DeveloperUser)
                               .Include(t => t.OwnerUser)
                               .Include(t => t.Attachments)
                               .Include(t => t.Comments)
                               .ThenInclude(tc => tc.User)
                           .FirstOrDefaultAsync(m => m.Id == id);
                if (ticket == null)
                {
                    return NotFound();
                }

                return View(ticket);
            }
            return RedirectToAction("Index");

        }
        // GET: Tickets/Create
        public IActionResult Create(int? Id, IFormFile attachment)
        {
            ViewData["DeveloperUserId"] = new SelectList(_context.Users, "Id", "FullName");
            //ViewData["OwnerUserId"] = new SelectList(_context.Users, "Id", "FullName");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name");
            ViewData["TicketPriorityId"] = new SelectList(_context.TicketPriorities, "Id", "Name");
            ViewData["TicketStatusId"] = new SelectList(_context.TicketStatuses, "Id", "Name");
            ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "Id", "Name");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,ProjectId,DeveloperUserId,TicketPriorityId,TicketStatusId,TicketTypeId,TicketAttachment")] Ticket ticket, IFormFile attachment)
        {
            if (!User.IsInRole("Demo"))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        ticket.Created = DateTime.Now;
                        ticket.OwnerUserId = _userManager.GetUserId(User);
                        TicketPriority priority = await _context.TicketPriorities.FirstOrDefaultAsync(t => t.Name == "Low");
                        if (priority != null)
                        {
                            ticket.TicketPriorityId = priority.Id;
                        }
                        TicketStatus status = await _context.TicketStatuses.FirstOrDefaultAsync(s => s.Name == "New");
                        if (status != null)
                        {
                            ticket.TicketStatusId = status.Id;
                        }
                        if (attachment != null)
                        {
                            AttachmentsService attachmentsService = new AttachmentsService();
                            ticket.Attachments.Add(attachmentsService.Attach(attachment));
                        }

                        _context.Add(ticket);
                        await _context.SaveChangesAsync();
                    }
                    catch
                    {
                        throw;
                    }
                    return RedirectToAction(nameof(MyTickets));
                }
                ViewData["DeveloperUserId"] = new SelectList(_context.Users, "Id", "Id", ticket.DeveloperUserId);
                ViewData["OwnerUserId"] = new SelectList(_context.Users, "Id", "Id", ticket.OwnerUserId);
                ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", ticket.ProjectId);
                ViewData["TicketPriorityId"] = new SelectList(_context.TicketPriorities, "Id", "Id", ticket.TicketPriorityId);
                ViewData["TicketStatusId"] = new SelectList(_context.TicketStatuses, "Id", "Id", ticket.TicketStatusId);
                ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "Id", "Id", ticket.TicketTypeId);
                return View(ticket);
            }
            else
            {
                TempData["DemoLockout"] = "Your changes will not be saved.  To make changes to the database please log in as a full user.";
                return RedirectToAction(nameof(MyTickets));
            }
        }

        // GET: Tickets/Edit/5
        [Authorize(Roles = "Admin, ProjectManager, Developer, Submitter")]
        public async Task<IActionResult> Edit(int? id, IFormFile attachment)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var roleName = _userManager.GetRolesAsync(await _userManager.GetUserAsync(User)).Result.FirstOrDefault();
            if (await _accessService.CanInteractTicket(userId, (int)id, roleName))
            {
                await _projectService.UsersOnProject(ticket.ProjectId);

                ViewData["DeveloperUserId"] = new SelectList(_context.Users, "Id", "FullName", ticket.DeveloperUserId);
                ViewData["OwnerUserId"] = new SelectList(_context.Users, "Id", "FullName", ticket.OwnerUserId);
                ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", ticket.ProjectId);
                ViewData["TicketPriorityId"] = new SelectList(_context.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
                ViewData["TicketStatusId"] = new SelectList(_context.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
                ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "Id", "Name", ticket.TicketTypeId);
                return View(ticket);
            }
            return RedirectToAction("Index");
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Created,Updated,ProjectId,TicketTypeId,TicketPriorityId,TicketStatusId,OwnerUserId,DeveloperUserId")] Ticket ticket, IFormFile attachment)
        {
            if (!User.IsInRole("Demo"))
            {
                if (id != ticket.Id)
                {
                    return NotFound();
                }

                Ticket oldTicket = await _context.Tickets
                    //.Where(t => t.Id == ticket.Id)
                    .Include(t => t.TicketPriority)
                    .Include(t => t.TicketStatus)
                    .Include(t => t.TicketType)
                    .Include(t => t.DeveloperUser)
                    .Include(t => t.Project)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.Id == ticket.Id);


                if (ModelState.IsValid)
                {
                    if (attachment != null)
                    {
                        AttachmentsService attachmentsService = new AttachmentsService();
                        ticket.Attachments.Add(attachmentsService.Attach(attachment));
                    }

                    try
                    {
                        ticket.Updated = DateTimeOffset.Now;
                        _context.Update(ticket);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TicketExists(ticket.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                    //Add history
                    string userId = _userManager.GetUserId(User);
                    Ticket newTicket = await _context.Tickets
                    .Include(t => t.TicketPriority)
                    .Include(t => t.TicketStatus)
                    .Include(t => t.TicketType)
                    .Include(t => t.DeveloperUser)
                    .Include(t => t.Project)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.Id == ticket.Id);
                    await _historiesService.AddHistory(oldTicket, newTicket, userId);

                    return RedirectToAction(nameof(Index));
                }
                ViewData["DeveloperUserId"] = new SelectList(_context.Users, "Id", "Id", ticket.DeveloperUserId);
                ViewData["OwnerUserId"] = new SelectList(_context.Users, "Id", "Id", ticket.OwnerUserId);
                ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", ticket.ProjectId);
                ViewData["TicketPriorityId"] = new SelectList(_context.TicketPriorities, "Id", "Id", ticket.TicketPriorityId);
                ViewData["TicketStatusId"] = new SelectList(_context.TicketStatuses, "Id", "Id", ticket.TicketStatusId);
                ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "Id", "Id", ticket.TicketTypeId);
                return RedirectToAction("Details", "Projects", new { id = ticket.ProjectId });
                //return View(ticket);
            }
            else
            {
                TempData["DemoLockout"] = "Your changes will not be saved.  To make changes to the database please log in as a full user.";
                return RedirectToAction("Details", "Tickets", new { id = ticket.ProjectId });
            }
        }

        // GET: Tickets/Delete/5
        [Authorize(Roles = "Admin, ProjectManager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.DeveloperUser)
                .Include(t => t.OwnerUser)
                .Include(t => t.Project)
                .Include(t => t.TicketPriority)
                .Include(t => t.TicketStatus)
                .Include(t => t.TicketType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, ProjectManager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!User.IsInRole("Demo"))
            {
                var ticket = await _context.Tickets.FindAsync(id);
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
