using DragonBugs2020.Areas.Identity.Pages.Account;
using DragonBugs2020.Models;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project = DragonBugs2020.Models.Project;
using DragonBugs2020.Data;
using Microsoft.AspNetCore.Identity;
using BTUser = DragonBugs2020.Models.BTUser;
using System.Diagnostics;

namespace DragonBugs2020.Services
{
    public class BTProjectService : IBTProjectService
    {

        private readonly ApplicationDbContext _context;

        public BTProjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        //METHODS
        public async Task<bool> IsUserOnProject(string userId, int projectId)
        {
            Project project = await _context.Projects
               .Include(u => u.ProjectUsers)
               .ThenInclude(u => u.User)
               .FirstOrDefaultAsync(u => u.Id == projectId);
            bool result = project.ProjectUsers.Any(u => u.UserId == userId);
            return result;
        }
        public async Task<List<Project>> ListUserProjects(string userId)
        {
            BTUser user = await _context.Users
                 .Include(p => p.ProjectUsers)
                 .ThenInclude(p => p.Project).ThenInclude(p => p.Tickets).ThenInclude(p => p.TicketPriority)
                 .Include(p => p.ProjectUsers)
                 .ThenInclude(p => p.Project).ThenInclude(p => p.Tickets).ThenInclude(p => p.TicketStatus)
                 .Include(p => p.ProjectUsers)
                 .ThenInclude(p => p.Project).ThenInclude(p => p.Tickets).ThenInclude(p => p.TicketType)
                 .Include(p => p.ProjectUsers)
                 .ThenInclude(p => p.Project).ThenInclude(p => p.Tickets).ThenInclude(p => p.DeveloperUser)
                 .FirstOrDefaultAsync(p => p.Id == userId);

            List<Project> projects = user.ProjectUsers.SelectMany(p => (IEnumerable<Project>)p.Project).ToList();
            return projects;
        }
        public async Task AddUserToProject(string userId, int projectId)
        {
            if (!await IsUserOnProject(userId, projectId))
            {
                try
                {
                    await _context.ProjectUsers.AddAsync(new ProjectUser { ProjectId = projectId, UserId = userId });
                    await _context.SaveChangesAsync();

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"*** ERROR *** - Error Adding user to project.  --> {ex.Message}");
                    throw;
                }
            }
        }
        public async Task RemoveUserFromProject(string userId, int projectId)
        {
            try
            {
                ProjectUser projectUser = _context.ProjectUsers.Where(u => u.UserId == userId && u.ProjectId == projectId).FirstOrDefault();

                _context.ProjectUsers.Remove(projectUser);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"***ERROR*** - Error removing user from project. --> {ex.Message}");
                throw;
            }
        }
        public async Task<ICollection<BTUser>> UsersOnProject(int projectId)
        {
            Project project = await _context.Projects
                .Include(u => u.ProjectUsers)
                .ThenInclude(u => u.User)
                .FirstOrDefaultAsync(u => u.Id == projectId);

            List<BTUser> projectusers = project.ProjectUsers.Select(p => p.User).ToList();
            return projectusers;
        }
        public async Task<ICollection<BTUser>> UsersNotOnProject(int projectId)
        {
            return await _context.Users.Where(u => IsUserOnProject(u.Id, projectId).Result == false).ToListAsync();
        }

    }
}


