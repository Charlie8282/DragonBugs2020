using DragonBugs2020.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace DragonBugs2020.Services
{
    public interface IBTHistoriesService
    {
        public Task AddHistory(Ticket oldTicket, Ticket newTicket, string userId);
      

    }
    }
