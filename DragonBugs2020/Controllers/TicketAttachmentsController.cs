﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DragonBugs2020.Data;
using DragonBugs2020.Models;
using System.IO;
using Microsoft.AspNetCore.Identity;
using DragonBugs2020.Services;
using Microsoft.AspNetCore.Authorization;

namespace DragonBugs2020.Controllers
{
    [Authorize]
    public class TicketAttachmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<BTUser> _userManager;
        private readonly IBTFileService _fileService;


        public TicketAttachmentsController(ApplicationDbContext context, UserManager<BTUser> userManager, IBTFileService fileService)
        {
            _context = context;
            _userManager = userManager;
            _fileService = fileService;
        }

        // GET: TicketAttachments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TicketAttachments.Include(t => t.Ticket).Include(t => t.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TicketAttachments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ticketAttachment = await _context.TicketAttachments
                .Include(t => t.Ticket)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketAttachment == null)
            {
                return NotFound();
            }
            return View(ticketAttachment);
        }

        // GET: TicketAttachments/Create
        public IActionResult Create()
        {
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: TicketAttachments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FormFile,Description,TicketId")] TicketAttachment ticketAttachment)
        {
            if (!User.IsInRole("Demo"))
            {
                if (ModelState.IsValid)
                {
                    MemoryStream ms = new MemoryStream();
                    await ticketAttachment.FormFile.CopyToAsync(ms);

                    ticketAttachment.ContentType = ticketAttachment.FormFile.ContentType;
                    ticketAttachment.FileData = await _fileService.ConvertFileToByteArrayAsync(ticketAttachment.FormFile);
                    ticketAttachment.FileName = ticketAttachment.FormFile.FileName;
                    ticketAttachment.Created = DateTimeOffset.Now;
                    ticketAttachment.UserId = _userManager.GetUserId(User);

                    try
                    {
                        _context.Add(ticketAttachment);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        TempData["Error"] = ex.Message;
                    }

                    return RedirectToAction("Details", "Tickets", new { id = ticketAttachment.TicketId });
                }
                ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description", ticketAttachment.TicketId);
                ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ticketAttachment.UserId);
                return View(ticketAttachment);
            }
            else
            {
                TempData["DemoLockout"] = "Your changes will not be saved.  To make changes to the database please log in as a full user.";
                return RedirectToAction("Details", "Tickets", new { id = ticketAttachment.TicketId });
            }
        }

        // GET: TicketAttachments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketAttachment = await _context.TicketAttachments.FindAsync(id);
            if (ticketAttachment == null)
            {
                return NotFound();
            }
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description", ticketAttachment.TicketId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ticketAttachment.UserId);
            return View(ticketAttachment);
        }

        // POST: TicketAttachments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FilePath,FileData,Description,Created,TicketId,UserId")] TicketAttachment ticketAttachment)
        {
            if (id != ticketAttachment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketAttachment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketAttachmentExists(ticketAttachment.Id))
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
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description", ticketAttachment.TicketId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ticketAttachment.UserId);
            return View(ticketAttachment);
        }

        // GET: TicketAttachments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.IsInRole("Demo"))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var ticketAttachment = await _context.TicketAttachments
                    .Include(t => t.Ticket)
                    .Include(t => t.User)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (ticketAttachment == null)
                {
                    return NotFound();
                }

                return View(ticketAttachment);
            }
            TempData["DemoLockout"] = "Your changes will not be saved.  To make changes to the database please log in as a full user.";
            return RedirectToAction(nameof(Index));
        }

        // POST: TicketAttachments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticketAttachment = await _context.TicketAttachments.FindAsync(id);
            _context.TicketAttachments.Remove(ticketAttachment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketAttachmentExists(int id)
        {
            return _context.TicketAttachments.Any(e => e.Id == id);
        }

        public async Task<FileResult> DownloadFile(int id)
        {
            TicketAttachment attachment = await _context.TicketAttachments.FindAsync(id);
            return File(attachment.FileData, attachment.ContentType);
        }
    }
}
