using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DragonBugs2020.Data;
using DragonBugs2020.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace DragonBugs2020.Controllers
{
    public class TicketCommentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<BTUser> _userManager;
        private readonly IEmailSender _emailSender;

        public TicketCommentsController(ApplicationDbContext context, UserManager<BTUser> userManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        // GET: TicketComments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TicketComments.Include(t => t.Ticket).Include(t => t.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TicketComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketComment = await _context.TicketComments
                .Include(t => t.Ticket)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketComment == null)
            {
                return NotFound();
            }

            return View(ticketComment);
        }

        // GET: TicketComments/Create
        public IActionResult Create(int? id)
        {
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName");
            var model = new TicketComment();
            model.TicketId = id.Value;
            return View(model);
        }

        // POST: TicketComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Comment,TicketId")] TicketComment ticketComment)
        {
            if (!User.IsInRole("Demo"))
            {
                if (ModelState.IsValid)
                {
                    ticketComment.Created = DateTime.Now;
                    ticketComment.UserId = _userManager.GetUserId(User);
                    _context.Add(ticketComment);
                    await _context.SaveChangesAsync();

                    var currentTicket = _context.Tickets.Find(ticketComment.TicketId);
                    var devUser = _context.Users.Find(currentTicket.DeveloperUserId);
                    var commentCreator = _context.Users.Find(ticketComment.UserId);
                    //string devEmail = devUser.Email;
                    //string subject = "New Ticket Comment";
                    //string message = $"Someone commented on a ticket: {commentCreator.FullName}";
                    //await _emailSender.SendEmailAsync(devEmail, subject, message);

                    return RedirectToAction("Details", "Tickets", new { id = ticketComment.TicketId });
                }
                else
                {
                    return NotFound();
                }

            }
            else
            {
                TempData["DemoLockout"] = "Your changes will not be saved.  To make changes to the database please log in as a full user.";
                return RedirectToAction("Details", "Tickets");
            }
            //ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description", ticketComment.TicketId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ticketComment.UserId);
            //return View(ticketComment);
        }

        // GET: TicketComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketComment = await _context.TicketComments.FindAsync(id);
            if (ticketComment == null)
            {
                return NotFound();
            }
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description", ticketComment.TicketId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ticketComment.UserId);
            return View(ticketComment);
        }

        // POST: TicketComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Comment,Created,TicketId,UserId,Updated")] TicketComment ticketComment)
        {
            if (id != ticketComment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ticketComment.Updated = DateTime.Now;
                    ticketComment.UserId = _userManager.GetUserId(User);
                    _context.Update(ticketComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketCommentExists(ticketComment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Tickets", new { id = ticketComment.TicketId });
            }
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description", ticketComment.TicketId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ticketComment.UserId);
            return View(ticketComment);
        }

        // GET: TicketComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketComment = await _context.TicketComments
                .Include(t => t.Ticket)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketComment == null)
            {
                return NotFound();
            }

            return View(ticketComment);
        }

        // POST: TicketComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticketComment = await _context.TicketComments.FindAsync(id);
            _context.TicketComments.Remove(ticketComment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketCommentExists(int id)
        {
            return _context.TicketComments.Any(e => e.Id == id);
        }
    }
}
