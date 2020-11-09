using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonBugs2020.Models.ViewModels
{
    public class TicketsManagerViewModel
    {
        public Ticket ticket { get; set; }
        public TicketPriority ticketPriority { get; set; }
        public ProjectUser projectUser { get; set; }
        
    }
}
