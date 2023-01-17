using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IssueTrackingSystem.Data;
using IssueTrackingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreHero.ToastNotification.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace IssueTrackingSystem.Pages.Issues
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly INotyfService _notyf;
        [Display(Name = "Project")]
        [BindProperty]
        public string SelectedProject { get; set; }

        public EditModel(ApplicationDbContext context, INotyfService notyf)
        {
            _context = context;
            _notyf = notyf;
        }

        [BindProperty]
        public Issue Issue { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Issues == null)
            {
                return NotFound();
            }

            var issue =  await _context.Issues.FirstOrDefaultAsync(m => m.IssueId == id);
            if (issue == null)
            {
                return NotFound();
            }
            Issue = issue;
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectName", "ProjectName");
            ViewData["User"] = new SelectList(_context.Users, "UserName", "UserName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //bind selected data from selectlist to model property
            //Issue.ProjectId = int.Parse(SelectedProject);
            Issue.ModifiedOn = DateTime.Now;
            
            if(Issue.Status == IssueStatus.Closed)
            {
                Issue.ResolutionDate = DateTime.Now;
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Issue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _notyf.Success("Issue Updated Successfully");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IssueExists(Issue.IssueId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool IssueExists(int id)
        {
          return _context.Issues.Any(e => e.IssueId == id);
        }
    }
}
