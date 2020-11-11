using DragonBugs2020.Data;
using DragonBugs2020.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Threading.Tasks;

namespace DragonBugs2020.Services
{
    public class BTHistoriesService : IBTHistoriesService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<BTUser> _userManager;

        public BTHistoriesService(ApplicationDbContext context, UserManager<BTUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task AddHistory(Ticket oldTicket, Ticket newTicket, string userId)
        {

            if (oldTicket.Title != newTicket.Title)
            {
                TicketHistory history = new TicketHistory
                {
                    TicketId = newTicket.Id,
                    Property = "Title",
                    OldValue = oldTicket.Title,
                    NewValue = newTicket.Title,
                    Created = DateTimeOffset.Now,
                    UserId = userId
                };
                await _context.TicketHistories.AddAsync(history);
            }

            if (oldTicket.Description != newTicket.Description)
            {
                TicketHistory history = new TicketHistory
                {
                    TicketId = newTicket.Id,
                    Property = "Description",
                    OldValue = oldTicket.Description,
                    NewValue = newTicket.Description,
                    Created = DateTimeOffset.Now,
                    UserId = userId
                };
                await _context.TicketHistories.AddAsync(history);
            }

            if (oldTicket.TicketTypeId != newTicket.TicketTypeId)
            {
                TicketHistory history = new TicketHistory
                {
                    TicketId = newTicket.Id,
                    Property = "Ticket Type",
                    OldValue = _context.TicketTypes.Find(oldTicket.TicketTypeId).Name,
                    NewValue = _context.TicketTypes.Find(newTicket.TicketTypeId).Name,
                    Created = DateTimeOffset.Now,
                    UserId = userId
                };
                await _context.TicketHistories.AddAsync(history);
            }

            if (oldTicket.TicketStatus != newTicket.TicketStatus)
            {
                TicketHistory history = new TicketHistory
                {
                    TicketId = newTicket.Id,
                    Property = "Ticket Status",
                    OldValue = _context.TicketStatuses.Find(oldTicket.TicketStatusId).Name,
                    NewValue = _context.TicketStatuses.Find(newTicket.TicketStatusId).Name,
                    Created = DateTimeOffset.Now,
                    UserId = userId
                };
                await _context.TicketHistories.AddAsync(history);
            }

            if (oldTicket.TicketPriority != newTicket.TicketPriority)
            {
                TicketHistory history = new TicketHistory
                {
                    TicketId = newTicket.Id,
                    Property = "Ticket Type",
                    OldValue = _context.TicketPriorities.Find(oldTicket.TicketPriorityId).Name,
                    NewValue = _context.TicketPriorities.Find(newTicket.TicketPriorityId).Name,
                    Created = DateTimeOffset.Now,
                    UserId = userId
                };
                await _context.TicketHistories.AddAsync(history);
            }

            if (oldTicket.DeveloperUserId != newTicket.DeveloperUserId)
            {
                if (String.IsNullOrWhiteSpace(oldTicket.DeveloperUserId))
                {
                    TicketHistory history = new TicketHistory
                    {
                        TicketId = newTicket.Id,
                        Property = "Developer User",
                        OldValue = "No Developer Assigned",
                        NewValue = _context.Users.Find(newTicket.DeveloperUserId).FullName,
                        Created = DateTimeOffset.Now,
                        UserId = userId
                    };
                    await _context.TicketHistories.AddAsync(history);
                }
                else if (String.IsNullOrWhiteSpace(newTicket.DeveloperUserId))
                {
                    TicketHistory history = new TicketHistory
                    {
                        TicketId = newTicket.Id,
                        Property = "Developer User",
                        OldValue = _context.Users.Find(oldTicket.DeveloperUserId).FullName,
                        NewValue = "No Developer Assigned",
                        Created = DateTimeOffset.Now,
                        UserId = userId
                    };
                    await _context.TicketHistories.AddAsync(history);
                }
                else
                {
                    TicketHistory history = new TicketHistory
                    {
                        TicketId = newTicket.Id,
                        Property = "Developer User",
                        OldValue = _context.Users.Find(oldTicket.DeveloperUserId).FullName,
                        NewValue = _context.Users.Find(newTicket.DeveloperUserId).FullName,
                        Created = DateTimeOffset.Now,
                        UserId = userId
                    };
                    await _context.TicketHistories.AddAsync(history);
                }
                await _context.SaveChangesAsync();

            }
        }

    }
}



//if (oldTicket.TicketStatusId != newTicket.TicketStatusId)
//{
//    TicketHistory history = new TicketHistory
//    {
//        TicketId = newTicket.Id,
//        Property = "Ticket Name",
//        OldValue = oldTicket.TicketStatus.Name,
//        NewValue = newTicket.TicketStatus.Name,

