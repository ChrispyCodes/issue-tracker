using IssueTrackingSystem.Data;
using IssueTrackingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IssueTrackingSystem.Pages
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


                //get total Issues grouped by month an year to display in bar chart
                var IssuesByMonth = await _context.Issues
                         .GroupBy(i => new { i.CreatedDate.Year, i.CreatedDate.Month })
                         .Select(g => new { Month = g.Key.Month, Year = g.Key.Year, Count = g.Count() })
                         .ToListAsync();

                //add Counts from IssuesByMonth to array for bar chart
                var IssuesByMonthArray = new int[12];
                foreach (var item in IssuesByMonth)
                {
                    //insert item.Count into array
                    IssuesByMonthArray[item.Month - 1] = item.Count;
                }
                ViewData["IssuesByMonth"] = IssuesByMonthArray;

            }
            if (_context.Projects != null)
            {
                Projects = await _context.Projects.ToListAsync();

            }
        }
    }
}
