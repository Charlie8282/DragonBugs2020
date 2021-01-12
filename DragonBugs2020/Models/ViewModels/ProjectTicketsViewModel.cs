using DragonBugs2020.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DragonBugs2020.Models.ViewModels
{
    public class ProjectTicketsViewModel
    {
        public ProjectTicketsViewModel()
        {
            Tickets = new List<Ticket>();
            Projects = new List<Project>();
            ProjectUser = new List<ProjectUser>();
            Attachments = new List<TicketAttachment>();
        }

        public List<Ticket> Tickets { get; set; }
        public List<Project> Projects { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public string DeveloperUserId { get; set; }
        public BTUser DeveloperUser { get; set; }
        public List<ProjectUser> ProjectUser { get; set; }
        public List<TicketAttachment> Attachments { get; set; }
        public string Description { get; set; }
        public int TicketTypeId { get; set; }
        public int TicketPriorityId { get; set; }
        public int TicketStatusId { get; set; }
        public int MyProperty { get; set; }
        public string FilePath { get; set; }
        [Display(Name = "Select Image")]
        [NotMapped]
        [DataType(DataType.Upload)]
        [MaxFileSize(2 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".doc", ".xlsx", ".pdf" })]
        public IFormFile FormFile { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
        public TicketType TicketType { get; set; }
        public TicketPriority TicketPriority { get; set; }
        public TicketStatus TicketStatus { get; set; }

    }
}
