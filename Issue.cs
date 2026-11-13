using System;

namespace MunicipalIssueReport
{
    public enum RequestStatus
    {
        Submitted = 0,
        UnderReview = 1,
        InProgress = 2,
        Resolved = 3
    }

    public class Issue
    {
        public int IssueId { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string AttachmentPath { get; set; }
        public DateTime DateReported { get; set; }

        // Added for Part 3 (Service Request Status)
        public RequestStatus Status { get; set; } = RequestStatus.Submitted;

        // 1 = Critical, 2 = High, 3 = Medium, 4 = Low
        public int Priority { get; set; } = 3;

        public DateTime LastUpdated { get; set; }

        public string StatusText
        {
            get { return StatusName(Status); }
        }

        public string PriorityText
        {
            get { return PriorityName(Priority); }
        }

        public static string StatusName(RequestStatus status)
        {
            switch (status)
            {
                case RequestStatus.UnderReview: return "Under Review";
                case RequestStatus.InProgress: return "In Progress";
                case RequestStatus.Resolved: return "Resolved";
                default: return "Submitted";
            }
        }

        public static string PriorityName(int priority)
        {
            switch (priority)
            {
                case 1: return "Critical";
                case 2: return "High";
                case 3: return "Medium";
                default: return "Low";
            }
        }
    }
}
