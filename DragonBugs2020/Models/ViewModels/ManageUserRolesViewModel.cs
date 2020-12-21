using Microsoft.AspNetCore.Mvc.Rendering;

namespace DragonBugs2020.Models.ViewModels
{
    public class ManageUserRolesViewModel
    {
        public BTUser User { get; set; }
        public SelectList Roles { get; set; }
        public string SelectedRole { get; set; }
    }
}
