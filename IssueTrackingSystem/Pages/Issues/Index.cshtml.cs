using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IssueTrackingSystem.Data;
using IssueTrackingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace IssueTrackingSystem.Pages.Issues
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

        public IList<Issue> Issue { get;set; } = default!;
        public IList<Issue> OverdueIssues { get; set; } = default!;


        public async Task OnGetAsync()
        {
            if (_context.Issues != null)
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                Issue = await _context.Issues.Include(i => i.Project)
                    .Include(i => i.User)
                    .Where(i => i.AssignedToId == currentUser.UserName && i.Status != IssueStatus.Closed)
                    .ToListAsync();

                OverdueIssues = await _context.Issues
                   .Include(i => i.Project)
                   .Include(i => i.User)
                   .Where(i => i.ResolutionDate > i.TargetResolutionDate && i.AssignedToId == currentUser.UserName && i.Status != IssueStatus.Closed)
                   .ToListAsync();
            }
            
        }


        //public async Task<IActionResult> OnPostDeleteAsync(int? id)
        //{

        //    if (id == null || _context.Issues == null)
        //    {
        //        return NotFound();
        //    }
        //    var issue = await _context.Issues.FindAsync(id);

        //    if (issue != null)
        //    {

        //        _context.Issues.Remove(issue);
        //        await _context.SaveChangesAsync();
        //    }

        //    return Page();
        //}
    }
}
