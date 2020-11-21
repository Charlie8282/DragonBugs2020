using DragonBugs2020.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonBugs2020.Services
{
    public interface IBTProjectService
    {
        public Task<bool> IsUserOnProject(string userId, int projectId);

        public Task<List<Project>> ListUserProjects(string userId);

        public Task AddUserToProject(string userId, int projectId);

        public Task RemoveUserFromProject(string userId, int projectId);

        public Task<ICollection<BTUser>> UsersOnProject(int projectId);

        public Task<ICollection<BTUser>> UsersNotOnProject(int projectId);

       
    }
}
