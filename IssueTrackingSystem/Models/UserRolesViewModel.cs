namespace IssueTrackingSystem.Models
{
    public class UserRolesViewModel
    {
        public string RoleName { get; set; }
        public bool Selected { get; set; }
    }
    public class ManageUserRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<UserRolesViewModel> UserRoles { get; set; }
    }
}
