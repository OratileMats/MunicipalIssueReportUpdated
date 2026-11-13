using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MunicipalIssueReport
{
    public partial class LocalEventsForm : Form
    {
        // Stores all available events
        private List<Event> events = new List<Event>();

        // Organises events by date
        private SortedDictionary<DateTime, List<Event>> eventsByDate =
            new SortedDictionary<DateTime, List<Event>>();

        // Stores unique event categories
        private HashSet<string> eventCategories =
            new HashSet<string>();

        // Tracks how often the user searches for each category.
        private Dictionary<string, int> categorySearchCounts =
            new Dictionary<string, int>();

        // Priority queue implemented using a SortedDictionary.
        // Lower priority number = higher priority.
        private SortedDictionary<int, Queue<Event>> priorityQueue =
            new SortedDictionary<int, Queue<Event>>();

        public LocalEventsForm()
        {
            InitializeComponent();

            // Modern look shared across the application
            Theme.Apply(this);
            Theme.StyleGhost(btnBack);   // sits on the dark header
            Theme.StyleSecondary(btnClear);

            // Keep the Back button on the right-hand side of the header.
            pnlHeader.Resize += (s, e) => PositionBackButton();
            Load += (s, e) => PositionBackButton();

            // Custom drawing so the recommendations read as a tidy list.
            lstRecommendations.DrawItem += lstRecommendations_DrawItem;

            // Set up the application data
            LoadEvents();

            // Set up the category dropdown
            LoadCategories();

            // Set up the event table
            SetupEventGrid();

            // Display all events when the form opens
            DisplayEvents(events);

            // Connect buttons
            btnSearch.Click += btnSearch_Click;
            btnClear.Click += btnClear_Click;
            btnBack.Click += btnBack_Click;
        }

        private void LoadEvents()
        {
            events.Add(new Event
            {
                EventId = 1,
                EventName = "Community Clean-Up Day",
                Category = "Environment",
                EventDate = new DateTime(2026, 10, 10),
                Location = "Rosebank",
                Description = "Residents are invited to participate in a community clean-up initiative.",
                Priority = 2
            });

            events.Add(new Event
            {
                EventId = 2,
                EventName = "Water Awareness Workshop",
                Category = "Utilities",
                EventDate = new DateTime(2026, 10, 15),
                Location = "Sandton",
                Description = "A workshop covering water conservation and responsible water usage.",
                Priority = 1
            });

            events.Add(new Event
            {
                EventId = 3,
                EventName = "Youth Development Workshop",
                Category = "Education",
                EventDate = new DateTime(2026, 10, 20),
                Location = "Johannesburg",
                Description = "A development workshop providing educational and career guidance.",
                Priority = 3
            });

            events.Add(new Event
            {
                EventId = 4,
                EventName = "Community Sports Day",
                Category = "Sports",
                EventDate = new DateTime(2026, 10, 25),
                Location = "Soweto",
                Description = "A community sports event promoting health and active lifestyles.",
                Priority = 4
            });

            events.Add(new Event
            {
                EventId = 5,
                EventName = "Public Safety Awareness Day",
                Category = "Safety",
                EventDate = new DateTime(2026, 11, 1),
                Location = "Midrand",
                Description = "An awareness event focused on community safety and emergency preparedness.",
                Priority = 1
            });

            // Add events to the SortedDictionary
            foreach (Event currentEvent in events)
            {
                if (!eventsByDate.ContainsKey(currentEvent.EventDate))
                {
                    eventsByDate[currentEvent.EventDate] =
                        new List<Event>();
                }

                eventsByDate[currentEvent.EventDate].Add(currentEvent);
            }

            // Add unique categories to the HashSet
            foreach (Event currentEvent in events)
            {
                eventCategories.Add(currentEvent.Category);
            }

            // Add events to the PriorityQueue
            foreach (Event currentEvent in events)
            {
                if (!priorityQueue.ContainsKey(currentEvent.Priority))
                {
                    priorityQueue[currentEvent.Priority] = new Queue<Event>();
                }

                priorityQueue[currentEvent.Priority].Enqueue(currentEvent);
            }
        }
        private void RecordCategorySearch(string category)
        {
            // Only record searches for a specific category.
            if (category == "All Categories")
            {
                return;
            }

            // Increase the count if this category was searched before.
            if (categorySearchCounts.ContainsKey(category))
            {
                categorySearchCounts[category]++;
            }
            else
            {
                // Record the category's first search.
                categorySearchCounts[category] = 1;
            }
        }

        private void LoadCategories()
        {
            cmbCategory.Items.Clear();

            cmbCategory.Items.Add("All Categories");

            foreach (string category in eventCategories.OrderBy(c => c))
            {
                cmbCategory.Items.Add(category);
            }

            cmbCategory.SelectedIndex = 0;
        }

        private void SetupEventGrid()
        {
            dgvEvents.Columns.Clear();
            dgvEvents.AutoGenerateColumns = false;

            dgvEvents.Columns.Add(
                "EventName",
                "Event");

            dgvEvents.Columns.Add(
                "Category",
                "Category");

            dgvEvents.Columns.Add(
                "EventDate",
                "Date");

            dgvEvents.Columns.Add(
                "Location",
                "Location");

            dgvEvents.Columns.Add(
                "Description",
                "Description");

            dgvEvents.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            // Give the long text columns more room.
            dgvEvents.Columns["EventName"].FillWeight = 130;
            dgvEvents.Columns["Category"].FillWeight = 70;
            dgvEvents.Columns["EventDate"].FillWeight = 65;
            dgvEvents.Columns["Location"].FillWeight = 70;
            dgvEvents.Columns["Description"].FillWeight = 230;

            foreach (DataGridViewColumn column in dgvEvents.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dgvEvents.ReadOnly = true;
            dgvEvents.AllowUserToAddRows = false;
            dgvEvents.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
        }

        private void DisplayEvents(List<Event> eventsToDisplay)
        {
            dgvEvents.Rows.Clear();

            foreach (Event currentEvent in eventsToDisplay)
            {
                dgvEvents.Rows.Add(
                    currentEvent.EventName,
                    currentEvent.Category,
                    currentEvent.EventDate.ToShortDateString(),
                    currentEvent.Location,
                    currentEvent.Description);
            }
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            string selectedCategory = cmbCategory.SelectedItem == null
                ? "All Categories"
                : cmbCategory.SelectedItem.ToString();

            RecordCategorySearch(selectedCategory);

            DateTime selectedDate = dtpEventDate.Value.Date;

            // Start with all available events.
            List<Event> filteredEvents = new List<Event>(events);

            // Filter by category if the user selected a specific category.
            if (selectedCategory != "All Categories")
            {
                filteredEvents = filteredEvents
                    .Where(ev => ev.Category == selectedCategory)
                    .ToList();
            }

            // Filter by the selected date.
            filteredEvents = filteredEvents
                .Where(ev => ev.EventDate.Date == selectedDate)
                .ToList();

            // Display matching events.
            DisplayEvents(filteredEvents);

            // Generate recommendations from the search results.
            GenerateRecommendations(filteredEvents);

            // Show the number of matching events.
            if (filteredEvents.Count == 0)
            {
                MessageBox.Show(
                    "No events were found for the selected category and date.",
                    "No Results",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            // Reset the category filter.
            cmbCategory.SelectedIndex = 0;

            // Reset the date to today's date.
            dtpEventDate.Value = DateTime.Today;

            // Show all events again.
            DisplayEvents(GetEventsByPriority());

            // Clear the recommendations.
            lstRecommendations.Items.Clear();
        }


        private void GenerateRecommendations(List<Event> searchedEvents)
        {
            lstRecommendations.Items.Clear();

            // Check whether the user has searched for a specific category.
            if (categorySearchCounts.Count == 0)
            {
                lstRecommendations.Items.Add(
                    "Welcome! Search for a category to receive personalised recommendations.");

                return;
            }

            // Identify the category searched for most frequently.
            string preferredCategory = categorySearchCounts
                .OrderByDescending(item => item.Value)
                .ThenBy(item => item.Key)
                .First()
                .Key;

            int searchCount = categorySearchCounts[preferredCategory];

            lstRecommendations.Items.Add(
                "YOUR PERSONALIZED RECOMMENDATIONS");

            lstRecommendations.Items.Add(
                "Preferred category: " + preferredCategory);

            lstRecommendations.Items.Add(
                "Searches in this category: " + searchCount);

            // Find upcoming events in the preferred category.
            List<Event> recommendedEvents = events
                .Where(ev =>
                    ev.Category == preferredCategory &&
                    ev.EventDate.Date >= DateTime.Today)
                .OrderBy(ev => ev.Priority)
                .ThenBy(ev => ev.EventDate)
                .ToList();

            lstRecommendations.Items.Add("");
            lstRecommendations.Items.Add("Recommended upcoming events:");

            if (recommendedEvents.Count == 0)
            {
                lstRecommendations.Items.Add(
                    "No upcoming events are available in your preferred category.");

                return;
            }

            // Display events in priority order.
            foreach (Event ev in recommendedEvents)
            {
                lstRecommendations.Items.Add(
                    ev.EventName
                    + " | Date: " + ev.EventDate.ToString("dd MMM yyyy")
                    + " | Location: " + ev.Location
                    + " | Priority: " + ev.Priority);
            }
        }
        private List<Event> GetEventsByPriority()
        {
            List<Event> orderedEvents = new List<Event>();

            // Process priority levels in ascending order.
            foreach (KeyValuePair<int, Queue<Event>> priorityGroup in priorityQueue)
            {
                // Process each event within this priority level.
                foreach (Event ev in priorityGroup.Value)
                {
                    orderedEvents.Add(ev);
                }
            }

            return orderedEvents;
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PositionBackButton()
        {
            btnBack.Location = new System.Drawing.Point(
                pnlHeader.ClientSize.Width - btnBack.Width - 28, 29);
        }

        private void lstRecommendations_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }

            string text = lstRecommendations.Items[e.Index].ToString();
            bool isHeading = text.StartsWith("YOUR PERSONALIZED")
                || text.StartsWith("Recommended upcoming events");
            bool isNote = text.StartsWith("Welcome")
                || text.StartsWith("No upcoming")
                || text.StartsWith("Preferred category")
                || text.StartsWith("Searches in this category");
            bool isEvent = text.Contains(" | Date:");

            using (System.Drawing.SolidBrush background =
                new System.Drawing.SolidBrush(Theme.Surface))
            {
                e.Graphics.FillRectangle(background, e.Bounds);
            }

            System.Drawing.FontStyle style = isHeading
                ? System.Drawing.FontStyle.Bold
                : System.Drawing.FontStyle.Regular;

            System.Drawing.Color colour = isHeading ? Theme.Primary
                : isNote ? Theme.Muted
                : Theme.Ink;

            if (isEvent)
            {
                text = "\u2022  " + text;
            }

            System.Drawing.Rectangle bounds = new System.Drawing.Rectangle(
                e.Bounds.X + 8, e.Bounds.Y, e.Bounds.Width - 16, e.Bounds.Height);

            using (System.Drawing.Font font = Theme.Ui(10F, style))
            {
                TextRenderer.DrawText(e.Graphics, text, font, bounds, colour,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter |
                    TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding);
            }
        }
    }
}
