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

namespace IssueTrackingSystem.Pages.Issues
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Issue> Issue { get;set; } = default!;


        public async Task OnGetAsync()
        {
            if (_context.Issues != null)
            {
                Issue = await _context.Issues
                .Include(i => i.Project)
                .Include(i => i.User).ToListAsync();
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
