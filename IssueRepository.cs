using System;
using System.Collections.Generic;
using System.IO;

namespace MunicipalIssueReport
{
    /// <summary>
    /// Single shared store for every reported issue. The Report Issues form
    /// writes to it and the Service Request Status form reads from it.
    ///
    /// Data structures used:
    ///   AvlTree      - look up a request by reference number (O(log n))
    ///   BST / Red-Black tree - same lookup, kept to compare with the AVL tree
    ///   GeneralTree  - municipal hierarchy (department > category > request)
    ///   MinHeap      - always know which pending request is most urgent
    ///   RequestGraph - link related requests (same location / category)
    /// </summary>
    public class TreeStats
    {
        public string Name { get; set; }
        public int Height { get; set; }
        public int Comparisons { get; set; }   // -1 = no request selected
        public string Note { get; set; }
    }

    public static class IssueRepository
    {
        // Set to false to start with an empty system (no demo requests).
        private const bool LoadDemoData = true;

        // Edge weights: lower = more closely related.
        private const int SameLocationWeight = 1;
        private const int SameCategoryWeight = 4;

        private static readonly AvlTree<Issue> tree = new AvlTree<Issue>();
        private static readonly BinarySearchTree<Issue> bst = new BinarySearchTree<Issue>();
        private static readonly RedBlackTree<Issue> redBlack = new RedBlackTree<Issue>();
        private static readonly MinHeap<Issue> pending = new MinHeap<Issue>(CompareUrgency);
        private static readonly RequestGraph graph = new RequestGraph();

        private static int nextId = 1;

        static IssueRepository()
        {
            if (LoadDemoData)
            {
                SeedDemoData();
            }
        }

        public static int Count
        {
            get { return tree.Count; }
        }

        public static int TreeHeight
        {
            get { return tree.Height; }
        }

        public static RequestGraph Graph
        {
            get { return graph; }
        }

        // ---------- Adding ----------

        /// <summary>Registers a newly reported issue and returns it with its reference number.</summary>
        public static Issue Add(Issue issue)
        {
            issue.IssueId = nextId++;
            issue.Status = RequestStatus.Submitted;
            issue.Priority = PriorityFor(issue.Category);
            issue.LastUpdated = issue.DateReported == default(DateTime)
                ? DateTime.Now
                : issue.DateReported;

            Register(issue);
            return issue;
        }

        private static void Register(Issue issue)
        {
            // Link to existing requests before inserting.
            graph.AddVertex(issue.IssueId);

            foreach (Issue other in tree.InOrder())
            {
                if (SameText(other.Location, issue.Location))
                {
                    graph.AddEdge(issue.IssueId, other.IssueId, SameLocationWeight);
                }
                else if (SameText(other.Category, issue.Category))
                {
                    graph.AddEdge(issue.IssueId, other.IssueId, SameCategoryWeight);
                }
            }

            tree.Insert(issue.IssueId, issue);
            bst.Insert(issue.IssueId, issue);
            redBlack.Insert(issue.IssueId, issue);

            if (issue.Status == RequestStatus.Submitted)
            {
                pending.Enqueue(issue);
            }
        }

        // ---------- Queries ----------

        public static List<Issue> GetAll()
        {
            return tree.InOrder();
        }

        public static bool TryFind(int id, out Issue issue, out int comparisons)
        {
            return tree.TryFind(id, out issue, out comparisons);
        }

        public static Issue Find(int id)
        {
            Issue issue;
            int comparisons;
            tree.TryFind(id, out issue, out comparisons);
            return issue;
        }

        /// <summary>Pending (Submitted) requests ordered most urgent first.</summary>
        public static List<Issue> GetPriorityQueue()
        {
            List<Issue> ordered = new List<Issue>();
            MinHeap<Issue> copy = pending.Clone();

            while (copy.Count > 0)
            {
                Issue next = copy.Dequeue();

                if (next.Status == RequestStatus.Submitted)
                {
                    ordered.Add(next);
                }
            }

            return ordered;
        }

        /// <summary>
        /// Looks the same reference number up in all three binary search
        /// trees so their height and search cost can be compared.
        /// </summary>
        public static List<TreeStats> CompareTrees(int id)
        {
            Issue unused;
            int cBst = -1, cAvl = -1, cRb = -1;
            bool hasId = id > 0;

            if (hasId)
            {
                bst.TryFind(id, out unused, out cBst);
                tree.TryFind(id, out unused, out cAvl);
                redBlack.TryFind(id, out unused, out cRb);
            }

            return new List<TreeStats>
            {
                new TreeStats { Name = "Binary search tree", Height = bst.Height, Comparisons = cBst,
                    Note = "No balancing - can degrade into a chain" },
                new TreeStats { Name = "AVL tree", Height = tree.Height, Comparisons = cAvl,
                    Note = "Strictly balanced (heights differ by at most 1)" },
                new TreeStats { Name = "Red-Black tree", Height = redBlack.Height, Comparisons = cRb,
                    Note = "Loosely balanced (height at most 2 x log n)" }
            };
        }

        /// <summary>Returns the AVL tree's contents in the requested traversal order.</summary>
        public static List<Issue> GetTraversal(string kind)
        {
            switch (kind)
            {
                case "Pre-order": return tree.PreOrder();
                case "Post-order": return tree.PostOrder();
                case "Level-order": return tree.LevelOrder();
                default: return tree.InOrder();
            }
        }

        /// <summary>
        /// Builds the municipal hierarchy as a general tree:
        /// Municipality - Department - Category - Request.
        /// </summary>
        public static GeneralTreeNode<string> BuildHierarchy()
        {
            GeneralTreeNode<string> rootNode = new GeneralTreeNode<string>("Municipal Services");

            string[][] departments =
            {
                new[] { "Infrastructure", "Roads", "Water", "Electricity" },
                new[] { "Environment and Health", "Sanitation", "Waste Management" },
                new[] { "Community Safety", "Public Safety" },
                new[] { "General Services", "Other" }
            };

            List<Issue> all = tree.InOrder();

            foreach (string[] department in departments)
            {
                GeneralTreeNode<string> deptNode = rootNode.AddChild(department[0]);

                for (int i = 1; i < department.Length; i++)
                {
                    GeneralTreeNode<string> categoryNode = deptNode.AddChild(department[i]);

                    foreach (Issue issue in all)
                    {
                        if (issue.Category == department[i])
                        {
                            categoryNode.AddChild("#" + issue.IssueId + "  " + issue.StatusText);
                        }
                    }
                }
            }

            return rootNode;
        }

        // ---------- Updating ----------

        /// <summary>Takes the most urgent pending request and starts work on it.</summary>
        public static Issue DispatchNext()
        {
            while (pending.Count > 0)
            {
                Issue next = pending.Dequeue();

                // Skip entries whose status was already changed elsewhere.
                if (next.Status == RequestStatus.Submitted)
                {
                    next.Status = RequestStatus.InProgress;
                    next.LastUpdated = DateTime.Now;
                    return next;
                }
            }

            return null;
        }

        /// <summary>Moves a request to its next stage. Returns false if already resolved.</summary>
        public static bool AdvanceStatus(Issue issue)
        {
            if (issue == null || issue.Status == RequestStatus.Resolved)
            {
                return false;
            }

            issue.Status = (RequestStatus)((int)issue.Status + 1);
            issue.LastUpdated = DateTime.Now;
            return true;
        }

        // ---------- Helpers ----------

        public static int PriorityFor(string category)
        {
            switch (category)
            {
                case "Public Safety": return 1;
                case "Electricity": return 2;
                case "Water": return 2;
                case "Roads": return 3;
                case "Sanitation": return 3;
                case "Waste Management": return 3;
                default: return 4;
            }
        }

        private static int CompareUrgency(Issue a, Issue b)
        {
            int byPriority = a.Priority.CompareTo(b.Priority);
            return byPriority != 0 ? byPriority : a.DateReported.CompareTo(b.DateReported);
        }

        private static bool SameText(string a, string b)
        {
            return string.Equals(
                (a ?? "").Trim(),
                (b ?? "").Trim(),
                StringComparison.OrdinalIgnoreCase);
        }

        private static void SeedDemoData()
        {
            AddDemo(1001, "Roads", "Oxford Road, Rosebank",
                "Large pothole forcing vehicles to swerve into the next lane.",
                RequestStatus.Submitted, 2);

            AddDemo(1002, "Water", "Rivonia Road, Sandton",
                "Burst pipe leaking onto the pavement for several days.",
                RequestStatus.InProgress, 6);

            AddDemo(1003, "Public Safety", "Vilakazi Street, Soweto",
                "Streetlights out and a broken fence beside the children's play area.",
                RequestStatus.Submitted, 1);

            AddDemo(1004, "Electricity", "Old Pretoria Road, Midrand",
                "Exposed cables hanging from a damaged electricity pole.",
                RequestStatus.UnderReview, 4);

            AddDemo(1005, "Roads", "Oxford Road, Rosebank",
                "Road markings have faded at the intersection.",
                RequestStatus.Submitted, 3);

            AddDemo(1006, "Sanitation", "Jan Smuts Avenue, Johannesburg",
                "Blocked storm drain causing a foul smell.",
                RequestStatus.Resolved, 12);

            AddDemo(1007, "Waste Management", "Rivonia Road, Sandton",
                "Refuse has not been collected for two weeks.",
                RequestStatus.UnderReview, 5);

            AddDemo(1008, "Water", "Vilakazi Street, Soweto",
                "Low water pressure affecting the whole street.",
                RequestStatus.Submitted, 8);
        }

        private static void AddDemo(int id, string category, string location,
            string description, RequestStatus status, int daysAgo)
        {
            Issue issue = new Issue
            {
                IssueId = id,
                Category = category,
                Location = location,
                Description = description,
                AttachmentPath = "",
                DateReported = DateTime.Now.AddDays(-daysAgo),
                Status = status,
                Priority = PriorityFor(category)
            };

            issue.LastUpdated = issue.DateReported;
            Register(issue);
        }
    }
}
