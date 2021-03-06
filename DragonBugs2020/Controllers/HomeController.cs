﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DragonBugs2020.Models;
using Microsoft.AspNetCore.Authorization;
using DragonBugs2020.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DragonBugs2020.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DragonBugs2020.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<BTUser> _userManager;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<BTUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }
        
        public async Task<IActionResult> Dashboard(int? page)
        {
            var viewModel = new ProjectTicketsViewModel();
            var model = new List<Ticket>();
            var userId = _userManager.GetUserId(User);
            if (page == null)
            {
                viewModel.Page = 1;
            }
            else
            {
                viewModel.Page = (int)page;
            }
            var skip = 5 * (viewModel.Page - 1);

            if (User.IsInRole("Admin"))
            {
                model = _context.Tickets
                    //.Include(u => u.User)
                    .Include(t => t.DeveloperUser)
                    .Include(t => t.OwnerUser)
                    .Include(t => t.Project)
                    .Include(t => t.TicketPriority)
                    .Include(t => t.TicketStatus)
                    .Include(t => t.TicketType)
                    .OrderByDescending(t => t.Created)
                    .Skip(skip)
                    .Take(5)
                    .ToList();
                viewModel.Remainder = _context.Tickets.Skip(skip + 5).Count();
            }
            else if (User.IsInRole("ProjectManager"))
            {
                model = _context.Tickets
                    .Where(t => t.DeveloperUserId == userId)
                    .Include(t => t.OwnerUser)
                    .Include(t => t.Project)
                    .Include(t => t.TicketPriority)
                    .Include(t => t.TicketStatus)
                    .Include(t => t.TicketType)
                    .OrderByDescending(t => t.Created)
                    .ToList();


                var projectIds = new List<int>();
                var userProjects = _context.ProjectUsers.Where(pu => pu.UserId == userId).ToList();

                foreach (var record in userProjects)
                {
                    projectIds.Add(record.ProjectId);
                }
                foreach (var id in projectIds)
                {
                    var tickets = _context.Tickets.Where(t => t.ProjectId == id)
                    .Include(t => t.DeveloperUser)
                    .Include(t => t.OwnerUser)
                    .Include(t => t.Project)
                    .Include(t => t.TicketPriority)
                    .Include(t => t.TicketStatus)
                    .Include(t => t.TicketType)
                    .OrderByDescending(t => t.Created)
                    .ToList();

                    model.AddRange(tickets);
                    viewModel.Remainder = model.Skip(skip + 5).Count();
                    model.OrderByDescending(t => t.Created).Skip(skip).Take(5);
                }
            }
            else if (User.IsInRole("Developer"))
            {
                model = _context.Tickets
                    .Where(t => t.DeveloperUserId == userId)
                    .Include(t => t.OwnerUser)
                    .Include(t => t.Project)
                    .Include(t => t.TicketPriority)
                    .Include(t => t.TicketStatus)
                    .Include(t => t.TicketType)
                    .OrderByDescending(t => t.Created)
                    .Skip(skip)
                    .Take(5)
                    .ToList();
                viewModel.Remainder = _context.Tickets.Where(t => t.DeveloperUserId == userId).Skip(skip + 5).Count();

            }
            else if (User.IsInRole("Submitter"))
            {
                model = _context.Tickets
                    .Where(t => t.OwnerUserId == userId)
                    .Include(t => t.OwnerUser)
                    .Include(t => t.Project)
                    .Include(t => t.TicketPriority)
                    .Include(t => t.TicketStatus)
                    .Include(t => t.TicketType)
                    .OrderByDescending(t => t.Created)
                    .Skip(skip)
                    .Take(5)
                    .ToList();
                viewModel.Remainder = _context.Tickets.Where(t => t.OwnerUserId == userId).Skip(skip + 5).Count();

            }
            else if (User.IsInRole("NewUser"))
            {
                model = _context.Tickets
                    .Where(t => t.OwnerUserId == userId)
                    .Include(t => t.OwnerUser)
                    .Include(t => t.Project)
                    .Include(t => t.TicketPriority)
                    .Include(t => t.TicketStatus)
                    .Include(t => t.TicketType)
                    .OrderByDescending(t => t.Created)
                    .Skip(skip)
                    .Take(5)
                    .ToList();
                viewModel.Remainder = _context.Tickets.Where(t => t.OwnerUserId == userId).Skip(skip + 5).Count();

            }
            else
            {
                return NotFound();
            }

            ViewData["DeveloperUserId"] = new SelectList(_context.Users, "Id", "FullName");
            //ViewData["OwnerUserId"] = new SelectList(_context.Users, "Id", "FullName");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name");
            ViewData["TicketPriorityId"] = new SelectList(_context.TicketPriorities, "Id", "Name");
            ViewData["TicketStatusId"] = new SelectList(_context.TicketStatuses, "Id", "Name");
            ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "Id", "Name");
            viewModel.Tickets = model;
            var projects = _context.Projects.ToList();
            viewModel.Projects = projects;
            return View(viewModel);

            //var vm = new ProjectTicketsViewModel
            //{
            //    Tickets = _context.Tickets.ToList(),
            //    Projects = _context.Projects.ToList()
            //};
            //return View(vm);
        }

        [Authorize]
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
