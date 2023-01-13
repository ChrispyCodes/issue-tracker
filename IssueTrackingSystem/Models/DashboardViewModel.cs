namespace IssueTrackingSystem.Models
{
    public class DashboardViewModel
    {
        public int OverdueIssues { get; set; }
        public int OpenIssues { get; set; }

        public int TotalIssues { get; set; }

        public int TotalProjects { get; set; }

        public int TotalUsers { get; set; }

        public int AverageDaysToResolve { get; set; }
        
        

        public IList<Issue> Issues { get; set; } = default!;

        public IList<Project> Projects { get; set; } = default!;

        //AverageDaysToResolve = (int)Issues.Average(i => (i.ResolvedDate - i.CreatedDate).TotalDays);
    }
}
