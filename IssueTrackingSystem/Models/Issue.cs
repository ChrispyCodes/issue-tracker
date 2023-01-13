using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IssueTrackingSystem.Models
{
    public class Issue
    {
        [Key]
        [Display(Name = "Issue Id")]
        public int IssueId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        [Display(Name = "Creator")]
        public string CreatedById { get; set; }
        [Required]
        [Display(Name = "Date Created")]
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        [Display(Name = "Project")]
        public int ProjectId { get; set; }
        public virtual Project? Project { get; set; }
        [Display(Name = "Assigned")]
        public string? AssignedToId { get; set; }
        [Required] 
        public IssueStatus Status { get; set; }
        [Required]
        public IssuePriority Priority { get; set; }
        [Display(Name = "Date Resolved")]
        public DateTime? ResolutionDate { get; set; }
        [Display(Name = "Target Date")]
        public DateTime? TargetResolutionDate { get; set; }
        [Display(Name = "Date Modified")]
        public DateTime? ModifiedOn { get; set; }

        [Display(Name = "User")]
        public string? Id { get; set; }
        [ForeignKey("Id")]
        public virtual IdentityUser? User { get; set; }


    }

    public enum IssueStatus
    {
        Open,
        Closed,
        InProgress,
        Resolved
    }

    public enum IssuePriority
    {
        Low,
        Medium,
        High
    }
}
