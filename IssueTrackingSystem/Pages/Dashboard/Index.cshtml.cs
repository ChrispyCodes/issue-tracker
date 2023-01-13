using IssueTrackingSystem.Data;
using IssueTrackingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IssueTrackingSystem.Pages.Dashboard
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Issue> Issue { get; set; } = default!;


        public async Task OnGetAsync()
        {
            if (_context.Issues != null)
            {
                //get issues assigned to the current user
                Issue = await _context.Issues
                    .Include(i => i.Project)
                    .Include(i => i.User)
                    .Where(i => i.User.UserName == User.Identity.Name)
                    .ToListAsync();

            }
        }
    }
}
