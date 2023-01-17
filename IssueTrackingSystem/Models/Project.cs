using System.ComponentModel.DataAnnotations;

namespace IssueTrackingSystem.Models
{
    public class Project
    {
        [Key]
        [Display(Name = "Project Id")]
        public int ProjectId { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string? ProjectName { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; } = DateTime.Now;
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "Target End Date")]
        public DateTime? TargetEndDate { get; set; }

        public virtual List<Issue>? Issues { get; set; }

    }
}
