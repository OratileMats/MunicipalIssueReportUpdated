using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MunicipalIssueReport
{
    /// <summary>
    /// Part 3 - Service Request Status.
    ///
    /// Three views over the same shared data (IssueRepository):
    ///   1. All Requests   - AVL tree: fast lookup by reference number
    ///   2. Priority Queue - Min-heap: most urgent pending request first
    ///   3. Related        - Graph: BFS, DFS and Minimum Spanning Tree
    ///   4. Tree Structures - general tree, BST vs AVL vs Red-Black, traversals
    ///
    /// The form is built entirely in code (no designer file) so the
    /// layout can be modern and responsive using docking.
    /// </summary>
    public class ServiceRequestStatusForm : Form
    {
        // ----- header -----
        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblSub;
        private Button btnBack;

        // ----- search -----
        private Panel pnlSearch;
        private Label lblSearchCaption;
        private TextBox txtSearch;
        private Button btnSearch;
        private Label lblSearchResult;

        // ----- tabs / filter -----
        private Panel pnlNav;
        private Button btnTabAll;
        private Button btnTabPriority;
        private Button btnTabRelated;
        private Button btnTabTrees;
        private Label lblFilter;
        private ComboBox cmbStatus;

        // ----- views -----
        private Panel pnlBody;
        private Panel pnlViews;
        private Panel pnlViewAll;
        private Panel pnlViewPriority;
        private Panel pnlViewRelated;
        private Panel pnlViewTrees;

        private DataGridView dgvRequests;
        private Label lblTreeInfo;

        private DataGridView dgvPriority;
        private Button btnDispatch;
        private Label lblDispatchResult;

        private Label lblRelatedHeader;
        private Label lblRelatedFooter;
        private ListBox lstBfs;
        private ListBox lstDfs;
        private ListBox lstMst;

        private TreeView tvHierarchy;
        private DataGridView dgvTrees;
        private Label lblCompareHeading;
        private Label lblTreeFooter;
        private ComboBox cmbTraversal;
        private ListBox lstTraversal;

        // ----- details -----
        private Panel pnlDetails;
        private Label lblDetailHeader;
        private Label lblDetailSub;
        private StatusStepper stepper;
        private FlowLayoutPanel flowFields;
        private Label valLocation;
        private Label valReported;
        private Label valUpdated;
        private Label valPriority;
        private Label valAttachment;
        private Label valRelated;
        private Label valDescription;
        private Button btnAdvance;

        private readonly ToolTip toolTip = new ToolTip();
        private readonly Font cellBold = Theme.Ui(9.5F, FontStyle.Bold);

        private Issue selected;
        private bool loading;
        private int viewIndex;

        public ServiceRequestStatusForm()
        {
            Text = "Service Request Status";
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1200, 740);
            MinimumSize = new Size(1040, 660);
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Theme.Background;
            Font = Theme.Ui(9.5F);

            BuildUi();

            LoadAll();
            LoadPriority();
            ShowDetails(null);
            SetView(0);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                cellBold.Dispose();
                toolTip.Dispose();
            }

            base.Dispose(disposing);
        }

        // =====================================================================
        //  UI construction
        // =====================================================================

        private void BuildUi()
        {
            SuspendLayout();

            BuildBody();
            BuildNav();
            BuildSearch();
            BuildHeader();

            ResumeLayout(true);
        }

        private void BuildHeader()
        {
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 92,
                BackColor = Theme.Navy
            };

            lblTitle = new Label
            {
                Text = "Service Request Status",
                AutoSize = true,
                Font = Theme.Ui(20F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(26, 12)
            };

            lblSub = new Label
            {
                Text = "Track your reports, see what is most urgent and explore related requests",
                AutoSize = true,
                Font = Theme.Ui(10F),
                ForeColor = Theme.OnNavyMuted,
                Location = new Point(29, 54)
            };

            btnBack = new Button
            {
                Text = "\u2190  Back to menu",
                Size = new Size(150, 38),
                Font = Theme.Ui(9.5F, FontStyle.Bold)
            };
            Theme.StyleGhost(btnBack);
            btnBack.Click += (s, e) => Close();

            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblSub);
            pnlHeader.Controls.Add(btnBack);
            pnlHeader.Resize += (s, e) => PositionHeaderButton();
            PositionHeaderButton();

            Controls.Add(pnlHeader);
        }

        private void PositionHeaderButton()
        {
            btnBack.Location = new Point(pnlHeader.ClientSize.Width - btnBack.Width - 28, 27);
        }

        private void BuildSearch()
        {
            pnlSearch = new Panel
            {
                Dock = DockStyle.Top,
                Height = 86,
                BackColor = Theme.Background
            };

            lblSearchCaption = new Label
            {
                Text = "TRACK A REQUEST BY REFERENCE NUMBER",
                AutoSize = true,
                Font = Theme.Ui(8.5F, FontStyle.Bold),
                ForeColor = Theme.Muted,
                Location = new Point(28, 14)
            };

            txtSearch = new TextBox
            {
                Font = Theme.Ui(11F),
                Width = 240,
                Location = new Point(28, 38),
                BackColor = Theme.Surface,
                ForeColor = Theme.Ink
            };
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    btnSearch.PerformClick();
                }
            };

            btnSearch = new Button
            {
                Text = "Search",
                Font = Theme.Ui(9.5F, FontStyle.Bold),
                Width = 110,
                Height = txtSearch.Height + 2,
                Location = new Point(txtSearch.Right + 10, txtSearch.Top - 1)
            };
            Theme.StylePrimary(btnSearch);
            btnSearch.Click += btnSearch_Click;

            lblSearchResult = new Label
            {
                Text = "Enter the reference number you received when you reported the issue.",
                AutoSize = true,
                MaximumSize = new Size(640, 0),
                Font = Theme.Ui(9.5F),
                ForeColor = Theme.Muted,
                Location = new Point(btnSearch.Right + 18, txtSearch.Top + 4)
            };

            pnlSearch.Controls.Add(lblSearchCaption);
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Controls.Add(btnSearch);
            pnlSearch.Controls.Add(lblSearchResult);

            Controls.Add(pnlSearch);
        }

        private void BuildNav()
        {
            pnlNav = new Panel
            {
                Dock = DockStyle.Top,
                Height = 56,
                BackColor = Theme.Background
            };

            btnTabAll = MakeTab("All Requests", 28, 0);
            btnTabPriority = MakeTab("Priority Queue", 28 + 158, 1);
            btnTabRelated = MakeTab("Related Requests", 28 + 316, 2);
            btnTabTrees = MakeTab("Tree Structures", 28 + 474, 3);

            lblFilter = new Label
            {
                Text = "Status",
                AutoSize = true,
                Font = Theme.Ui(9.5F),
                ForeColor = Theme.Muted
            };

            cmbStatus = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                Font = Theme.Ui(10F),
                Width = 170,
                BackColor = Theme.Surface,
                ForeColor = Theme.Ink
            };
            cmbStatus.Items.Add("All statuses");
            foreach (RequestStatus status in Enum.GetValues(typeof(RequestStatus)))
            {
                cmbStatus.Items.Add(Issue.StatusName(status));
            }
            cmbStatus.SelectedIndex = 0;
            cmbStatus.SelectedIndexChanged += (s, e) => LoadAll();

            pnlNav.Controls.Add(btnTabAll);
            pnlNav.Controls.Add(btnTabPriority);
            pnlNav.Controls.Add(btnTabRelated);
            pnlNav.Controls.Add(btnTabTrees);
            pnlNav.Controls.Add(lblFilter);
            pnlNav.Controls.Add(cmbStatus);
            pnlNav.Resize += (s, e) => PositionFilter();
            PositionFilter();

            Controls.Add(pnlNav);
        }

        private Button MakeTab(string text, int left, int index)
        {
            Button tab = new Button
            {
                Text = text,
                Size = new Size(150, 38),
                Location = new Point(left, 8),
                Font = Theme.Ui(9.5F, FontStyle.Bold)
            };
            Theme.StyleSecondary(tab);
            tab.Click += (s, e) => SetView(index);
            return tab;
        }

        private void PositionFilter()
        {
            int right = pnlNav.ClientSize.Width - 28;
            cmbStatus.Location = new Point(right - cmbStatus.Width, 12);
            lblFilter.Location = new Point(cmbStatus.Left - lblFilter.Width - 10, 16);
        }

        private void BuildBody()
        {
            pnlBody = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Theme.Background,
                Padding = new Padding(28, 4, 28, 24)
            };

            // 1px border around the views, drawn with padding on a coloured panel
            pnlViews = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Theme.Border,
                Padding = new Padding(1)
            };

            BuildAllView();
            BuildPriorityView();
            BuildRelatedView();
            BuildTreesView();

            pnlViews.Controls.Add(pnlViewAll);
            pnlViews.Controls.Add(pnlViewPriority);
            pnlViews.Controls.Add(pnlViewRelated);
            pnlViews.Controls.Add(pnlViewTrees);

            Panel gap = new Panel { Dock = DockStyle.Right, Width = 18, BackColor = Theme.Background };

            BuildDetails();

            // Fill first, then the edge panels (later = outermost).
            pnlBody.Controls.Add(pnlViews);
            pnlBody.Controls.Add(gap);
            pnlBody.Controls.Add(pnlDetails);

            Controls.Add(pnlBody);
        }

        // ----- All requests view -----

        private void BuildAllView()
        {
            pnlViewAll = new Panel { Dock = DockStyle.Fill, BackColor = Theme.Surface };

            dgvRequests = NewGrid();
            AddColumn(dgvRequests, "colId", "Ref #", 11);
            AddColumn(dgvRequests, "colCategory", "Category", 20);
            AddColumn(dgvRequests, "colLocation", "Location", 30);
            AddColumn(dgvRequests, "colStatus", "Status", 17);
            AddColumn(dgvRequests, "colPriority", "Priority", 14);
            AddColumn(dgvRequests, "colDate", "Reported", 16);
            dgvRequests.SelectionChanged += Grid_SelectionChanged;
            dgvRequests.CellFormatting += Grid_CellFormatting;

            lblTreeInfo = new Label
            {
                Dock = DockStyle.Bottom,
                Height = 34,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(14, 0, 0, 0),
                Font = Theme.Ui(8.75F),
                ForeColor = Theme.Muted,
                BackColor = Color.FromArgb(248, 250, 252)
            };

            pnlViewAll.Controls.Add(dgvRequests);
            pnlViewAll.Controls.Add(lblTreeInfo);
        }

        // ----- Priority queue view -----

        private void BuildPriorityView()
        {
            pnlViewPriority = new Panel { Dock = DockStyle.Fill, BackColor = Theme.Surface };

            dgvPriority = NewGrid();
            AddColumn(dgvPriority, "colRank", "Order", 9);
            AddColumn(dgvPriority, "colPId", "Ref #", 11);
            AddColumn(dgvPriority, "colPCategory", "Category", 20);
            AddColumn(dgvPriority, "colPLocation", "Location", 30);
            AddColumn(dgvPriority, "colPPriority", "Priority", 14);
            AddColumn(dgvPriority, "colPDate", "Reported", 16);
            dgvPriority.SelectionChanged += Grid_SelectionChanged;
            dgvPriority.CellFormatting += Grid_CellFormatting;

            Label info = new Label
            {
                Dock = DockStyle.Top,
                Height = 52,
                Padding = new Padding(14, 8, 14, 0),
                Font = Theme.Ui(9F),
                ForeColor = Theme.Muted,
                BackColor = Color.FromArgb(248, 250, 252),
                Text = "Pending requests, most urgent first. A min-heap keeps the highest priority request "
                     + "at the top; requests of equal priority are served oldest first."
            };

            Panel bottom = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 64,
                BackColor = Color.FromArgb(248, 250, 252)
            };

            btnDispatch = new Button
            {
                Text = "Start work on most urgent request",
                Size = new Size(268, 38),
                Location = new Point(14, 13),
                Font = Theme.Ui(9.5F, FontStyle.Bold)
            };
            Theme.StylePrimary(btnDispatch);
            btnDispatch.Click += btnDispatch_Click;
            toolTip.SetToolTip(btnDispatch,
                "Demo action: simulates an officer taking the top item off the priority queue.");

            lblDispatchResult = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(520, 0),
                Font = Theme.Ui(9.5F),
                ForeColor = Theme.Muted,
                Location = new Point(btnDispatch.Right + 16, 22),
                Text = ""
            };

            bottom.Controls.Add(btnDispatch);
            bottom.Controls.Add(lblDispatchResult);

            pnlViewPriority.Controls.Add(dgvPriority);
            pnlViewPriority.Controls.Add(info);
            pnlViewPriority.Controls.Add(bottom);
        }

        // ----- Related requests (graph) view -----

        private void BuildRelatedView()
        {
            pnlViewRelated = new Panel { Dock = DockStyle.Fill, BackColor = Theme.Surface };

            TableLayoutPanel table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 1,
                BackColor = Theme.Surface,
                Padding = new Padding(10, 0, 10, 10)
            };
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            lstBfs = NewList();
            lstDfs = NewList();
            lstMst = NewList();

            table.Controls.Add(MakeListColumn("Nearest first (BFS)", lstBfs), 0, 0);
            table.Controls.Add(MakeListColumn("Follow the chain (DFS)", lstDfs), 1, 0);
            table.Controls.Add(MakeListColumn("Cheapest links (MST)", lstMst), 2, 0);

            lblRelatedHeader = new Label
            {
                Dock = DockStyle.Top,
                Height = 58,
                Padding = new Padding(18, 14, 18, 0),
                Font = Theme.Ui(11F, FontStyle.Bold),
                ForeColor = Theme.Ink,
                BackColor = Theme.Surface
            };

            lblRelatedFooter = new Label
            {
                Dock = DockStyle.Bottom,
                Height = 66,
                Padding = new Padding(18, 8, 18, 0),
                Font = Theme.Ui(8.75F),
                ForeColor = Theme.Muted,
                BackColor = Color.FromArgb(248, 250, 252)
            };

            pnlViewRelated.Controls.Add(table);
            pnlViewRelated.Controls.Add(lblRelatedHeader);
            pnlViewRelated.Controls.Add(lblRelatedFooter);
        }

        private Panel MakeListColumn(string caption, Control list)
        {
            Panel column = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(8, 0, 8, 0),
                BackColor = Theme.Surface
            };

            Label heading = new Label
            {
                Text = caption,
                Dock = DockStyle.Top,
                Height = 30,
                Font = Theme.Ui(9F, FontStyle.Bold),
                ForeColor = Theme.Primary,
                TextAlign = ContentAlignment.MiddleLeft
            };

            Panel border = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Theme.Border,
                Padding = new Padding(1)
            };
            border.Controls.Add(list);

            column.Controls.Add(border);
            column.Controls.Add(heading);
            return column;
        }

        private ListBox NewList()
        {
            return new ListBox
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None,
                BackColor = Theme.Surface,
                ForeColor = Theme.Ink,
                Font = Theme.Ui(9.25F),
                IntegralHeight = false,
                HorizontalScrollbar = true,
                ItemHeight = 20
            };
        }

        // ----- Tree structures view -----

        private void BuildTreesView()
        {
            pnlViewTrees = new Panel { Dock = DockStyle.Fill, BackColor = Theme.Surface };

            TableLayoutPanel table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Theme.Surface,
                Padding = new Padding(10, 6, 10, 10)
            };
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62F));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // Left: general tree
            tvHierarchy = new TreeView
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None,
                BackColor = Theme.Surface,
                ForeColor = Theme.Ink,
                Font = Theme.Ui(9.5F),
                ItemHeight = 26,
                ShowLines = true,
                HideSelection = false
            };
            table.Controls.Add(MakeListColumn("Municipal hierarchy (general tree)", tvHierarchy), 0, 0);

            // Right: comparison + traversal
            Panel right = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(8, 0, 8, 0),
                BackColor = Theme.Surface
            };

            lblCompareHeading = new Label
            {
                Dock = DockStyle.Top,
                Height = 30,
                Font = Theme.Ui(9F, FontStyle.Bold),
                ForeColor = Theme.Primary,
                TextAlign = ContentAlignment.MiddleLeft
            };

            dgvTrees = NewGrid();
            dgvTrees.Dock = DockStyle.Top;
            dgvTrees.Height = 42 + 3 * 38 + 2;
            dgvTrees.ScrollBars = ScrollBars.None;
            AddColumn(dgvTrees, "colTree", "Tree", 26);
            AddColumn(dgvTrees, "colHeight", "Height", 12);
            AddColumn(dgvTrees, "colComparisons", "Comparisons", 18);
            AddColumn(dgvTrees, "colNote", "Behaviour", 44);
            dgvTrees.SelectionChanged += (s, e) => dgvTrees.ClearSelection();

            Label travHeading = new Label
            {
                Dock = DockStyle.Top,
                Height = 38,
                Text = "Traversals of the AVL tree (a binary tree)",
                Font = Theme.Ui(9F, FontStyle.Bold),
                ForeColor = Theme.Primary,
                TextAlign = ContentAlignment.BottomLeft
            };

            cmbTraversal = new ComboBox
            {
                Dock = DockStyle.Top,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                Font = Theme.Ui(10F),
                BackColor = Theme.Surface,
                ForeColor = Theme.Ink
            };
            cmbTraversal.Items.AddRange(new object[] { "In-order", "Pre-order", "Post-order", "Level-order" });
            cmbTraversal.SelectedIndex = 0;
            cmbTraversal.SelectedIndexChanged += (s, e) => LoadTraversal();

            lstTraversal = NewList();
            Panel listBorder = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Theme.Border,
                Padding = new Padding(1)
            };
            listBorder.Controls.Add(lstTraversal);

            // Fill first, then edges (the last one added sits outermost).
            right.Controls.Add(listBorder);
            right.Controls.Add(cmbTraversal);
            right.Controls.Add(travHeading);
            right.Controls.Add(dgvTrees);
            right.Controls.Add(lblCompareHeading);

            table.Controls.Add(right, 1, 0);

            lblTreeFooter = new Label
            {
                Dock = DockStyle.Bottom,
                Height = 66,
                Padding = new Padding(18, 8, 18, 0),
                Font = Theme.Ui(8.75F),
                ForeColor = Theme.Muted,
                BackColor = Color.FromArgb(248, 250, 252),
                Text = "The hierarchy is a general tree (a node can have any number of children). "
                     + "The three search trees hold the same requests, so the table shows how much "
                     + "balancing reduces the height and the number of comparisons. Reference numbers "
                     + "arrive in ascending order, which is the worst case for an unbalanced tree."
            };

            pnlViewTrees.Controls.Add(table);
            pnlViewTrees.Controls.Add(lblTreeFooter);
        }

        private void RefreshTrees()
        {
            // General tree
            tvHierarchy.BeginUpdate();
            tvHierarchy.Nodes.Clear();
            GeneralTreeNode<string> hierarchy = IssueRepository.BuildHierarchy();
            AddTreeNodes(tvHierarchy.Nodes, hierarchy);
            tvHierarchy.ExpandAll();
            tvHierarchy.EndUpdate();

            // Binary search tree comparison
            int id = selected != null ? selected.IssueId : 0;

            lblCompareHeading.Text = selected == null
                ? "Select a request in another tab to compare how each tree finds it"
                : "Looking up request #" + id + " in each tree";

            dgvTrees.Rows.Clear();

            foreach (TreeStats stats in IssueRepository.CompareTrees(id))
            {
                dgvTrees.Rows.Add(
                    stats.Name,
                    stats.Height,
                    stats.Comparisons < 0 ? "-" : stats.Comparisons.ToString(),
                    stats.Note);
            }

            dgvTrees.ClearSelection();
            LoadTraversal();
        }

        private void AddTreeNodes(TreeNodeCollection target, GeneralTreeNode<string> node)
        {
            TreeNode added = target.Add(node.Value);

            foreach (GeneralTreeNode<string> child in node.Children)
            {
                AddTreeNodes(added.Nodes, child);
            }
        }

        private void LoadTraversal()
        {
            lstTraversal.Items.Clear();
            string kind = cmbTraversal.SelectedItem == null ? "In-order" : cmbTraversal.SelectedItem.ToString();
            int position = 1;

            foreach (Issue issue in IssueRepository.GetTraversal(kind))
            {
                lstTraversal.Items.Add(position + ".  #" + issue.IssueId + "  " + issue.Category
                    + " \u2013 " + issue.Location);
                position++;
            }
        }

        // ----- Details panel -----

        private void BuildDetails()
        {
            pnlDetails = new Panel
            {
                Dock = DockStyle.Right,
                Width = 362,
                BackColor = Theme.Border,
                Padding = new Padding(1)
            };

            Panel inner = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Theme.Surface,
                Padding = new Padding(20, 18, 20, 18)
            };

            flowFields = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Theme.Surface
            };

            valLocation = AddField("Location");
            valReported = AddField("Reported");
            valUpdated = AddField("Last updated");
            valPriority = AddField("Priority");
            valRelated = AddField("Related requests");
            valAttachment = AddField("Attachment");
            valDescription = AddField("Description");

            btnAdvance = new Button
            {
                Dock = DockStyle.Bottom,
                Height = 44,
                Font = Theme.Ui(9.5F, FontStyle.Bold),
                Text = "Advance status"
            };
            Theme.StylePrimary(btnAdvance);
            btnAdvance.Click += btnAdvance_Click;
            toolTip.SetToolTip(btnAdvance,
                "Demo action: simulates a municipal officer moving the request to its next stage.");

            stepper = new StatusStepper { Dock = DockStyle.Top, Height = 84 };

            lblDetailSub = new Label
            {
                Dock = DockStyle.Top,
                Height = 28,
                Font = Theme.Ui(10F),
                ForeColor = Theme.Muted
            };

            lblDetailHeader = new Label
            {
                Dock = DockStyle.Top,
                Height = 42,
                Font = Theme.Ui(17F, FontStyle.Bold),
                ForeColor = Theme.Ink
            };

            // Fill first, then edges (the last one added sits outermost).
            inner.Controls.Add(flowFields);
            inner.Controls.Add(btnAdvance);
            inner.Controls.Add(stepper);
            inner.Controls.Add(lblDetailSub);
            inner.Controls.Add(lblDetailHeader);

            pnlDetails.Controls.Add(inner);
        }

        private Label AddField(string caption)
        {
            Label cap = new Label
            {
                Text = caption.ToUpper(),
                AutoSize = true,
                Font = Theme.Ui(8F, FontStyle.Bold),
                ForeColor = Theme.Muted,
                Margin = new Padding(0, 14, 0, 2)
            };

            Label value = new Label
            {
                Text = "-",
                AutoSize = true,
                MaximumSize = new Size(290, 0),
                Font = Theme.Ui(10F),
                ForeColor = Theme.Ink,
                Margin = new Padding(0)
            };

            flowFields.Controls.Add(cap);
            flowFields.Controls.Add(value);
            return value;
        }

        // ----- grid helpers -----

        private DataGridView NewGrid()
        {
            DataGridView grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoGenerateColumns = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            Theme.StyleGrid(grid);
            return grid;
        }

        private static void AddColumn(DataGridView grid, string name, string header, float weight)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = header,
                FillWeight = weight,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };

            grid.Columns.Add(column);
        }

        private void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridView grid = (DataGridView)sender;
            Issue issue = grid.Rows[e.RowIndex].Tag as Issue;

            if (issue == null)
            {
                return;
            }

            string column = grid.Columns[e.ColumnIndex].Name;

            if (column == "colStatus")
            {
                e.CellStyle.ForeColor = Theme.StatusColor(issue.Status);
                e.CellStyle.SelectionForeColor = Theme.StatusColor(issue.Status);
                e.CellStyle.Font = cellBold;
            }
            else if (column == "colPriority" || column == "colPPriority")
            {
                e.CellStyle.ForeColor = PriorityColor(issue.Priority);
                e.CellStyle.SelectionForeColor = PriorityColor(issue.Priority);
                e.CellStyle.Font = cellBold;
            }
        }

        private static Color PriorityColor(int priority)
        {
            switch (priority)
            {
                case 1: return Theme.Danger;
                case 2: return Theme.Warning;
                case 3: return Theme.Ink;
                default: return Theme.Muted;
            }
        }

        // =====================================================================
        //  Views
        // =====================================================================

        private void SetView(int index)
        {
            viewIndex = index;

            pnlViewAll.Visible = index == 0;
            pnlViewPriority.Visible = index == 1;
            pnlViewRelated.Visible = index == 2;
            pnlViewTrees.Visible = index == 3;

            // The status filter only applies to the full list.
            lblFilter.Visible = index == 0;
            cmbStatus.Visible = index == 0;

            StyleTab(btnTabAll, index == 0);
            StyleTab(btnTabPriority, index == 1);
            StyleTab(btnTabRelated, index == 2);
            StyleTab(btnTabTrees, index == 3);

            if (index == 1)
            {
                LoadPriority();
            }
            else if (index == 2)
            {
                RefreshRelated();
            }
            else if (index == 3)
            {
                RefreshTrees();
            }
        }

        private static void StyleTab(Button tab, bool active)
        {
            if (active)
            {
                Theme.StylePrimary(tab);
            }
            else
            {
                Theme.StyleSecondary(tab);
            }
        }

        private RequestStatus? GetStatusFilter()
        {
            if (cmbStatus.SelectedIndex <= 0)
            {
                return null;
            }

            return (RequestStatus)(cmbStatus.SelectedIndex - 1);
        }

        private void LoadAll()
        {
            loading = true;

            dgvRequests.Rows.Clear();
            RequestStatus? filter = GetStatusFilter();
            int shown = 0;

            foreach (Issue issue in IssueRepository.GetAll())
            {
                if (filter.HasValue && issue.Status != filter.Value)
                {
                    continue;
                }

                int row = dgvRequests.Rows.Add(
                    issue.IssueId,
                    issue.Category,
                    issue.Location,
                    issue.StatusText,
                    issue.PriorityText,
                    issue.DateReported.ToString("dd MMM yyyy"));

                dgvRequests.Rows[row].Tag = issue;
                shown++;
            }

            loading = false;
            dgvRequests.ClearSelection();

            lblTreeInfo.Text = "Showing " + shown + " of " + IssueRepository.Count + " requests"
                + "   \u00B7   stored in a self-balancing AVL tree (height "
                + IssueRepository.TreeHeight + "), listed by reference number";

            if (selected != null)
            {
                SelectIssueInGrid(dgvRequests, selected.IssueId);
            }
        }

        private void LoadPriority()
        {
            loading = true;

            dgvPriority.Rows.Clear();
            int rank = 1;

            foreach (Issue issue in IssueRepository.GetPriorityQueue())
            {
                int row = dgvPriority.Rows.Add(
                    rank,
                    issue.IssueId,
                    issue.Category,
                    issue.Location,
                    issue.PriorityText,
                    issue.DateReported.ToString("dd MMM yyyy"));

                dgvPriority.Rows[row].Tag = issue;
                rank++;
            }

            loading = false;
            dgvPriority.ClearSelection();

            if (selected != null)
            {
                SelectIssueInGrid(dgvPriority, selected.IssueId);
            }
        }

        private void RefreshRelated()
        {
            lstBfs.Items.Clear();
            lstDfs.Items.Clear();
            lstMst.Items.Clear();

            if (selected == null)
            {
                lblRelatedHeader.Text = "Select a request in another tab to see the requests related to it.";
                lblRelatedFooter.Text = "";
                return;
            }

            RequestGraph graph = IssueRepository.Graph;
            int id = selected.IssueId;

            lblRelatedHeader.Text = "Related to #" + id + "  \u2013  "
                + selected.Category + ", " + selected.Location;

            foreach (KeyValuePair<int, int> step in graph.Bfs(id))
            {
                string prefix = step.Value == 0 ? "Start" : "Hop " + step.Value;
                lstBfs.Items.Add(prefix + "   " + Describe(step.Key));
            }

            foreach (int related in graph.Dfs(id))
            {
                lstDfs.Items.Add(Describe(related));
            }

            int totalWeight = 0;
            var tree = graph.MinimumSpanningTree(id);

            foreach (GraphEdge edge in tree)
            {
                totalWeight += edge.Weight;
                lstMst.Items.Add("#" + edge.From + "  \u2192  #" + edge.To + "    weight " + edge.Weight);
            }

            if (tree.Count == 0)
            {
                lstMst.Items.Add("No links \u2013 this request stands alone.");
            }

            lblRelatedFooter.Text =
                "Requests are linked automatically: same location = weight 1, same category = weight 4 "
                + "(lower means more closely related). The spanning tree connects every related request "
                + "using the cheapest links. Total weight: " + totalWeight
                + " across " + tree.Count + " link(s).";
        }

        private static string Describe(int id)
        {
            Issue issue = IssueRepository.Find(id);

            return issue == null
                ? "#" + id
                : "#" + id + "  " + issue.Category + " \u2013 " + issue.Location;
        }

        private void RefreshAllViews()
        {
            LoadAll();
            LoadPriority();
            ShowDetails(selected);

            if (viewIndex == 2)
            {
                RefreshRelated();
            }
            else if (viewIndex == 3)
            {
                RefreshTrees();
            }
        }

        private void SelectIssueInGrid(DataGridView grid, int id)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                Issue issue = row.Tag as Issue;

                if (issue != null && issue.IssueId == id)
                {
                    grid.ClearSelection();
                    row.Selected = true;
                    grid.CurrentCell = row.Cells[0];
                    return;
                }
            }
        }

        // =====================================================================
        //  Details panel
        // =====================================================================

        private void ShowDetails(Issue issue)
        {
            if (issue == null)
            {
                lblDetailHeader.Text = "No request selected";
                lblDetailSub.Text = "Search above or pick a row from the list";
                stepper.Clear();

                valLocation.Text = "-";
                valReported.Text = "-";
                valUpdated.Text = "-";
                valPriority.Text = "-";
                valRelated.Text = "-";
                valAttachment.Text = "-";
                valDescription.Text = "-";

                btnAdvance.Text = "Advance status";
                SetAdvanceEnabled(false);
                return;
            }

            lblDetailHeader.Text = "Request #" + issue.IssueId;
            lblDetailSub.Text = issue.Category;
            stepper.SetStatus(issue.Status);

            valLocation.Text = issue.Location;
            valReported.Text = issue.DateReported.ToString("dd MMM yyyy, HH:mm");
            valUpdated.Text = issue.LastUpdated.ToString("dd MMM yyyy, HH:mm");
            valPriority.Text = issue.PriorityText;
            valPriority.ForeColor = PriorityColor(issue.Priority);

            int links = IssueRepository.Graph.Neighbours(issue.IssueId).Count;
            valRelated.Text = links == 0
                ? "None"
                : links + (links == 1 ? " linked request" : " linked requests");

            valAttachment.Text = string.IsNullOrWhiteSpace(issue.AttachmentPath)
                ? "None"
                : System.IO.Path.GetFileName(issue.AttachmentPath);

            valDescription.Text = issue.Description;

            if (issue.Status == RequestStatus.Resolved)
            {
                btnAdvance.Text = "Request resolved";
                SetAdvanceEnabled(false);
            }
            else
            {
                RequestStatus next = (RequestStatus)((int)issue.Status + 1);
                btnAdvance.Text = "Advance to " + Issue.StatusName(next);
                SetAdvanceEnabled(true);
            }
        }

        private void SetAdvanceEnabled(bool enabled)
        {
            btnAdvance.Enabled = enabled;

            // Flat buttons keep their fill when disabled, so switch to the
            // neutral style to make the disabled state obvious.
            if (enabled)
            {
                Theme.StylePrimary(btnAdvance);
            }
            else
            {
                Theme.StyleSecondary(btnAdvance);
            }
        }

        // =====================================================================
        //  Events
        // =====================================================================

        private void Grid_SelectionChanged(object sender, EventArgs e)
        {
            if (loading)
            {
                return;
            }

            DataGridView grid = (DataGridView)sender;

            if (grid.SelectedRows.Count == 0)
            {
                return;
            }

            Issue issue = grid.SelectedRows[0].Tag as Issue;

            if (issue == null)
            {
                return;
            }

            selected = issue;
            ShowDetails(issue);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string digits = new string(txtSearch.Text.Where(char.IsDigit).ToArray());
            int id;

            if (digits.Length == 0 || !int.TryParse(digits, out id))
            {
                ShowSearchMessage("Please enter a reference number, for example 1004.", Theme.Danger);
                txtSearch.Focus();
                return;
            }

            Issue issue;
            int comparisons;

            if (IssueRepository.TryFind(id, out issue, out comparisons))
            {
                selected = issue;
                cmbStatus.SelectedIndex = 0;   // make sure the row is not filtered out
                SetView(0);
                SelectIssueInGrid(dgvRequests, id);
                ShowDetails(issue);

                ShowSearchMessage(
                    "Found request #" + id + " after " + comparisons + " comparison"
                    + (comparisons == 1 ? "" : "s") + " (balanced tree of "
                    + IssueRepository.Count + " requests, height "
                    + IssueRepository.TreeHeight + ").",
                    Theme.Success);
            }
            else
            {
                ShowSearchMessage(
                    "No request found with reference number " + id + " ("
                    + comparisons + " comparison" + (comparisons == 1 ? "" : "s") + ").",
                    Theme.Danger);
            }
        }

        private void ShowSearchMessage(string message, Color color)
        {
            lblSearchResult.ForeColor = color;
            lblSearchResult.Text = message;
        }

        private void btnDispatch_Click(object sender, EventArgs e)
        {
            Issue next = IssueRepository.DispatchNext();

            if (next == null)
            {
                lblDispatchResult.ForeColor = Theme.Muted;
                lblDispatchResult.Text = "The queue is empty - there are no pending requests.";
                return;
            }

            selected = next;

            lblDispatchResult.ForeColor = Theme.Success;
            lblDispatchResult.Text = "Request #" + next.IssueId + " (" + next.Category
                + ") was the most urgent and is now In Progress.";

            RefreshAllViews();
        }

        private void btnAdvance_Click(object sender, EventArgs e)
        {
            if (selected != null && IssueRepository.AdvanceStatus(selected))
            {
                RefreshAllViews();
            }
        }
    }
}
