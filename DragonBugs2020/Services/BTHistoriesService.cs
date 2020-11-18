using DragonBugs2020.Data;
using DragonBugs2020.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DragonBugs2020.Services
{
    public class BTHistoriesService : IBTHistoriesService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<BTUser> _userManager;
        private readonly IEmailSender _emailSender;

        public BTHistoriesService(ApplicationDbContext context, UserManager<BTUser> userManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
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
                    OldValue = oldTicket.TicketType.Name,
                    NewValue = newTicket.TicketType.Name,
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
                    OldValue = oldTicket.TicketStatus.Name,
                    NewValue = newTicket.TicketStatus.Name,
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
                    OldValue = oldTicket.TicketPriority.Name,
                    NewValue = newTicket.TicketPriority.Name,
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
                        NewValue = newTicket.DeveloperUser.FullName,
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
                        OldValue = oldTicket.DeveloperUser.FullName,
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
                        OldValue = oldTicket.DeveloperUser.FullName,
                        NewValue = newTicket.DeveloperUser.FullName,
                        Created = DateTimeOffset.Now,
                        UserId = userId
                    };
                    await _context.TicketHistories.AddAsync(history);

                    //notification
                    Notification notification = new Notification
                    {
                        TicketId = newTicket.Id,
                        Description = "You have been assigned to a new ticket.",
                        Created = DateTimeOffset.Now,
                        SenderId = userId,
                        RecipientId = newTicket.DeveloperUserId,

                    };
                    await _context.Notifications.AddAsync(notification);

                    //Send an email
                    string devEmail = newTicket.DeveloperUser.Email;
                    string subject = "New Ticket Assignment";
                    string message = $"You have a new ticket for a project: {newTicket.Project.Name}";

                    await _emailSender.SendEmailAsync(devEmail, subject, message);
                }
            }
            await _context.SaveChangesAsync();
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

