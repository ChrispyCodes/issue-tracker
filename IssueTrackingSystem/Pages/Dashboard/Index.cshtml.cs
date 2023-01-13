using IssueTrackingSystem.Data;
using IssueTrackingSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;

namespace IssueTrackingSystem.Pages.Dashboard
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Issue> Issue { get; set; } = default!;
        public IList<Project> Project { get; set; } = default!;
        public IList<Issue> OverdueIssues { get; set; } = default!;


        public async Task OnGetAsync()
        {
            
            if (_context.Issues != null)
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                //get issues assigned to the current user
                Issue = await _context.Issues
                    .Include(i => i.Project)
                    .Include(i => i.User)
                    .Where(i => i.AssignedToId == currentUser.UserName || i.Status == IssueStatus.Open)
                    .ToListAsync();

                
                //get list of Issues that are overdue where ResolutionDate is a later date TargetResolutionDate
                OverdueIssues = await _context.Issues
                    .Include(i => i.Project)
                    .Include(i => i.User)
                    .Where(i => i.ResolutionDate > i.TargetResolutionDate)
                    .ToListAsync();
            }
            if (_context.Projects != null)
            {
                Project = await _context.Projects.ToListAsync();
            }
        }
    }
}
