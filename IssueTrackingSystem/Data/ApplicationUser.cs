using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IssueTrackingSystem.Data
{
    public class ApplicationUser : IdentityUser
    {
        
        public string Name { get; set; }
    }
}
