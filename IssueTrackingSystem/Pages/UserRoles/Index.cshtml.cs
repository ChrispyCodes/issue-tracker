using AspNetCoreHero.ToastNotification.Abstractions;
using IssueTrackingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IssueTrackingSystem.Pages.UserRoles
{
    [Authorize(Roles = "SuperAdmin")]
    public class IndexModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly INotyfService _notyf;

        public IndexModel(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, INotyfService notyf)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _notyf = notyf;
        }
        [BindProperty]
        public IList<ManageUserRolesViewModel> ManageUserRolesModel { get; set; } = new List<ManageUserRolesViewModel>();
        public async Task OnGet(string userId)
        {
            var viewModel = new List<UserRolesViewModel>();
            var user = await _userManager.FindByIdAsync(userId);
            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = await _roleManager.Roles.ToListAsync();
            foreach (var role in _roleManager.Roles.ToList())
            {
                var userRolesViewModel = new UserRolesViewModel
                {
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                viewModel.Add(userRolesViewModel);
            }
            
            var model = new ManageUserRolesViewModel
            {
                UserId = userId,
                UserName = user.UserName,
                UserRoles = viewModel
            };
            ManageUserRolesModel.Add(model);
        }

        public async Task<IActionResult> OnPost(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = await _roleManager.Roles.ToListAsync();
           
            var selectedRoles = ManageUserRolesModel[0].UserRoles.Where(x => x.Selected).Select(x => x.RoleName).ToList();
            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Cannot add selected roles to user");
                return Page();
            }
            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Cannot remove selected roles from user");
                return Page();
            }
            _notyf.Success("Roles Saved");
            return RedirectToPage("/Users/Index");
        }
    }
}
