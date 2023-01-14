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

        public DashboardViewModel DashboardViewModel { get; set; } = default!;

        public async Task OnGetAsync()
        {
            
            if (_context.Issues != null)
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                DashboardViewModel = new DashboardViewModel
                {
                    OverdueIssues = await _context.Issues.Where(i => i.ResolutionDate > i.TargetResolutionDate).CountAsync(),
                    OpenIssues = await _context.Issues.Where(i => i.Status == IssueStatus.Open).CountAsync(),
                    TotalIssues = await _context.Issues.CountAsync(),
                    TotalProjects = await _context.Projects.CountAsync(),
                    TotalUsers = await _context.Users.CountAsync(),
                    Issues = await _context.Issues.Include(i => i.Project)
                    .Include(i => i.User)
                    .Where(i => i.AssignedToId == currentUser.UserName || i.Status == IssueStatus.Open)
                    .ToListAsync(),
                    Projects = await _context.Projects.ToListAsync(),
                    //AverageDaysToResolve = (int)await _context.Issues.AverageAsync(i => (i.ResolutionDate - i.CreatedDate).Value.TotalDays)
                };

                
            }
            if (_context.Projects != null)
            {
                Project = await _context.Projects.ToListAsync();
            }
        }
    }
}
