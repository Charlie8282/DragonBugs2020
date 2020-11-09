using DragonBugs2020.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonBugs2020.Models.ViewModels
{
    public class ManageProjectUsersViewModel
    {
        public Project Project { get; set; }
        public MultiSelectList Users { get; set; }  //populates list box
        public string[] SelectedUsers { get; set; }  // recieves selected users
    }
}
