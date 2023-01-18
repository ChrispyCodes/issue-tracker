using IssueTrackingSystem.Data;
using IssueTrackingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace IssueTrackingSystem.Pages.Dashboard
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Project> OpenIssuesByProject { get; set; } = default!;
        public IList<Project> Projects { get; set; } = default!;
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
                    .Where(i => i.Status != IssueStatus.Closed)
                    .ToListAsync(),
                    Projects = await _context.Projects.ToListAsync(),
     
                };
                
                OverdueIssues = await _context.Issues
                   .Include(i => i.Project)
                   .Include(i => i.User)
                   .Where(i => i.ResolutionDate > i.TargetResolutionDate)
                   .ToListAsync();
                
                //Get count of Issues from each project 
                OpenIssuesByProject = await _context.Projects.Include(p => p.Issues).Where(p => p.Issues.Count != 0).ToListAsync();
                //create list with the average amount of days it takes issues to be resolved by assigning the average of the difference between the ResolutionDate and the CreatedDate to the property

                //var averageDaysToResolve = _context.Issues.Where(i => i.ResolutionDate != null).Select(i => (i.ResolutionDate - i.CreatedDate).Value.Days).Average();
                //foreach (var issue in _context.Issues)
                //{                  
                //        if (issue.ResolutionDate != null)
                //        {
                //            var daysToResolve = Average(i => (i.ResolutionDate - i.CreatedDate).TotalDays);
                //        averageDaysToResolve.Add(daysToResolve);
                //        }
                //    }
                //}

            }
            if (_context.Projects != null)
            {
                Projects = await _context.Projects.ToListAsync();
                  
            }
        }
    }
}
