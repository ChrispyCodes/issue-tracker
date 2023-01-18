using IssueTrackingSystem.Data;
using IssueTrackingSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace IssueTrackingSystem.Pages.Calender
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IList<EventsViewModel> events { get; set; }
        public IndexModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Issue> Issue { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            if (_context.Issues != null)
            {
                //get list of Issues where the current user is assigned to and the status is not closed
                Issue = await _context.Issues.Include(i => i.Project)
                .Include(i => i.User)
                .Where(i => i.AssignedToId == currentUser.UserName && i.Status != IssueStatus.Closed)
                .ToListAsync();

                //return 
            }

            events = new List<EventsViewModel>();

            foreach (var issue in Issue)
            {
                events.Add(new EventsViewModel
                {
                    id = issue.IssueId,
                    title = issue.Title,
                    start = issue.CreatedDate.ToString(),
                    end = issue.TargetResolutionDate.ToString(),
                    allDay = false
                });
            }
            ViewData["Events"] = JsonConvert.SerializeObject(events);
        }
        
       
        
    }
}
