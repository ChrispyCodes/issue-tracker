using IssueTrackingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IssueTrackingSystem.Pages.Roles
{
    [Authorize(Roles = "SuperAdmin")]
    public class IndexModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public IndexModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IList<IdentityRole> Role { get; set; } = default!;

        public async Task OnGet()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            Role = roles;
        }

        //AddRole method is called when the user clicks on the Add Role button
        public async Task<IActionResult> OnPostAddRoleAsync(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return Page();
            }

            var role = new IdentityRole(roleName);
            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToPage();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }

        //DeleteRole method is called when the user clicks on the Delete Role button
        public async Task<IActionResult> OnPostDeleteRoleAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return Page();
            }

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                return RedirectToPage();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }

        //EditRole method is called when the user clicks on the Edit Role button
        public async Task<IActionResult> OnPostEditRoleAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return Page();
            }

            return RedirectToPage("Edit", new { id = roleId });
        }
    }
}
