using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DragonBugs2020.Models;
using Microsoft.AspNetCore.Authorization;
using DragonBugs2020.Data;
using Microsoft.EntityFrameworkCore;

namespace DragonBugs2020.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        public async Task<IActionResult> Dashboard(string userId)
        {
            
            var applicationDbContext = _context.Tickets
                .Include(t => t.DeveloperUser)
                .Include(t => t.OwnerUser)
                .Include(t => t.Project)
                .Include(t => t.TicketPriority)
                .Include(t => t.TicketStatus)
                .Include(t => t.TicketType);
            return View(await applicationDbContext.ToListAsync());
            
        }

        public async Task<IActionResult> SecondDashboard(string userId)
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
            return View(model);
        }
            [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
