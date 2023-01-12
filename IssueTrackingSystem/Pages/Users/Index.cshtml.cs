using IssueTrackingSystem.Data;
using IssueTrackingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IssueTrackingSystem.Pages.Users
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IList<IdentityUser> User { get; set; } = default!;

        public async Task OnGet()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            //get currentUser roles
            var currentUserRoles = await _userManager.GetRolesAsync(currentUser);
            var allUsersExceptCurrentUser = await _userManager.Users.Where(a => a.Id != currentUser.Id).ToListAsync();
            User = allUsersExceptCurrentUser;
        }
    }
}
