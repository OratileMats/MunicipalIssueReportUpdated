# Municipal Issue Reporting Application

## 1. Project Overview

The Municipal Issue Reporting Application is a C# Windows Forms application designed to allow citizens to report problems within their local community.

The application provides a simple and user-friendly interface where residents can provide information about a municipal issue, including its location, category and description. Users can also attach supporting images or documents to their report.

The application was developed in three parts: Part 1 (Report Issues), Part 2 (Local Events and Announcements) and Part 3 (Service Request Status).

---

## 2. Features

The application currently provides the following features:

- Main menu displayed when the application starts.
- Report Issues functionality.
- Location input for reported issues.
- Issue category selection.
- Detailed issue description.
- Image and document attachment.
- File selection using OpenFileDialog.
- Dynamic reporting progress indicator.
- Encouraging engagement messages.
- Validation of required information.
- Success and warning MessageBox notifications.
- Issue reference numbers.
- Back navigation to the main menu.
- Storage of reported issues using a List data structure.

Parts 2 and 3 add:

- Local Events and Announcements (search by category and date, personalised recommendations).
- Service Request Status (see section 21).
- A modern, consistent look across every screen (see section 22).

---

## 3. Technologies Used

The application was developed using:

- C#
- .NET Windows Forms
- Visual Studio
- Windows Forms controls
- Object-Oriented Programming
- Generic List data structure
- OpenFileDialog for file attachments

---

## 4. System Requirements

To compile and run the application, the following are required:

- Windows operating system
- Visual Studio
- C# development tools
- .NET Framework version required by the project

The project should be opened using the version of Visual Studio that supports the project's target .NET Framework.

---

## 5. How to Open the Project

1. Open Visual Studio.
2. Select **Open a project or solution**.
3. Locate the Municipal Issue Reporting project.
4. Open the project's `.sln` solution file.
5. Wait for Visual Studio to load the project and its dependencies.

---

## 6. How to Compile the Application

After opening the project:

1. Select **Build** from the Visual Studio menu.
2. Select **Build Solution**.

Alternatively, use:

`Ctrl + Shift + B`

The application should compile successfully without errors.

---

## 7. How to Run the Application

To run the application:

1. Open the project in Visual Studio.
2. Build the solution.
3. Press **F5** or select **Start**.

The Municipal Service Portal main menu will appear.

---

## 8. Using the Main Menu

When the application starts, the user is presented with three option cards:

### Report Issues

This option allows the user to create and submit a municipal issue report.

### Local Events and Announcements

Search community events by category and date and receive personalised recommendations (Part 2).

### Service Request Status

Track submitted service requests by reference number, see the most urgent requests and explore related requests (Part 3, see section 21).

---

## 9. Reporting an Issue

To report an issue:

1. Select **Report Issues** from the main menu.
2. Enter the location where the issue occurred.
3. Select an appropriate issue category.
4. Enter a detailed description of the problem.
5. Optionally attach an image or document.
6. Review the reporting progress.
7. Select **Submit**.

The application validates the required information before accepting the report.

---

## 10. Issue Categories

The application provides the following categories:

- Roads
- Sanitation
- Water
- Electricity
- Waste Management
- Public Safety
- Other

---

## 11. Attaching Files

Users can attach supporting evidence to an issue report.

Select the **Choose File** button to open the Windows file selection dialog.

The application supports common image and document formats including:

- JPG
- JPEG
- PNG
- GIF
- PDF
- DOC
- DOCX

The selected file path is stored with the issue report.

---

## 12. Reporting Progress and User Engagement

The application includes a dynamic progress bar to show the user's progress while completing a report.

The progress is divided between:

- Location - 25%
- Category - 25%
- Description - 25%
- Attachment - 25%

The application also displays encouraging messages as the user completes each part of the report.

Examples include:

- "Great start! Keep going."
- "You're halfway there!"
- "Almost done! Thank you for helping."
- "Excellent! Your report is ready to submit."

This feature was implemented to encourage users to complete their reports and actively participate in municipal issue reporting.

---

## 13. Validation and User Feedback

The application validates the information entered by the user before submitting a report.

The following fields are required:

- Location
- Category
- Description

If required information is missing, the application displays a warning message and directs the user to the relevant field.

After a successful submission, a confirmation message is displayed.

---

## 14. Issue Reference Numbers

Each submitted issue is assigned an issue reference number.

The reference number allows the user to identify the report that they have submitted.

For example:

`Your Issue Reference Number is: 1`

---

## 15. Data Structure

Reported issues are stored using a C# `List<Issue>` data structure.

Each `Issue` object contains:

- Issue ID
- Location
- Category
- Description
- Attachment path
- Date reported

The List data structure allows multiple issue objects to be stored and managed within the application.

---

## 16. Navigation

Users can select **Back to Main Menu** from the Report Issues form to return to the application's main menu.

---

## 17. Error Handling

The application uses validation and MessageBox notifications to provide feedback to users.

Examples include:

- Missing location warning.
- Missing category warning.
- Missing description warning.
- Successful report confirmation.

This helps users understand what information is required and whether their report has been successfully submitted.

---

## 18. Current Limitations

All data is held in memory, so reported issues are lost when the application closes. The following are not implemented:

- Persistent database storage.
- User accounts and authentication.
- Administrative issue management (the "Advance status" button is a demonstration of this).

---

## 19. Future Improvements

Possible future improvements include:

- Connecting the application to a database.
- Allowing administrators to view and manage reported issues.
- Allowing citizens to track the status of submitted issues.
- Implementing Local Events and Announcements.
- Adding Service Request Status functionality.
- Adding user authentication.
- Providing more advanced reporting and analytics.
- Improving accessibility and multilingual support.

---

## 20. Author

Municipal Issue Reporting Application

Developed as part of the Task 2 implementation requirements.

---

## 21. Part 3 - Service Request Status (Implementation Report)

Select **Service Request Status** on the main menu. Every issue submitted through **Report Issues** appears here, together with eight demo requests (reference numbers 1001-1008) so the screen is populated on first run. To start with an empty system, set `LoadDemoData` to `false` in `IssueRepository.cs`.

### 21.1 How to use the screen

1. **Track a request** - type a reference number (for example `1004`) and press Enter or **Search**. A green message reports how many comparisons the lookup needed; a red message appears if no request has that number.
2. **All Requests tab** - the full list with its status. Use the **Status** drop-down to filter. Click a row to open its details on the right, including the four-step progress tracker (Submitted, Under Review, In Progress, Resolved).
3. **Priority Queue tab** - pending requests, most urgent first. **Start work on most urgent request** takes the top request off the queue and sets it to In Progress.
4. **Related Requests tab** - choose a request in another tab first. Three lists show the requests related to it.
5. **Tree Structures tab** - shows the municipal hierarchy, compares the three search trees for the selected request and lists the tree in different traversal orders.
6. **Advance status** (details panel) - a demonstration of an officer moving a request to its next stage.

### 21.2 Data structures and their role

All forms share one `IssueRepository`, so a report submitted on the Report Issues screen is immediately visible in Service Request Status. Every request is inserted into the structures below at the same time.

| Rubric topic | Class | Used for |
|---|---|---|
| Basic tree | `GeneralTree.cs` | Municipal hierarchy |
| Binary tree and traversals | `AvlTree.cs` | Pre-, in-, post- and level-order |
| Binary search tree | `BinarySearchTree.cs` | Baseline for comparison |
| AVL tree | `AvlTree.cs` | Main lookup by reference number |
| Red-Black tree | `RedBlackTree.cs` | Alternative balanced lookup |
| Heap | `MinHeap.cs` | Priority queue of pending requests |
| Graph | `RequestGraph.cs` | Related requests |
| Graph traversal | `RequestGraph.Bfs` / `Dfs` | Exploring related requests |
| Minimum spanning tree | `RequestGraph.MinimumSpanningTree` | Cheapest links between related requests |

#### Basic (general) tree - `GeneralTree.cs`

*Role:* A basic tree is a hierarchy in which each node can have any number of children. The Tree Structures tab builds the hierarchy Municipal Services > Department > Category > Request. For example, Infrastructure contains Roads, Water and Electricity, and the Roads node contains requests #1001 and #1005.

*Contribution:* Requests are organised the way the municipality is organised, so a user can see at a glance which department a request belongs to and how many requests each category holds. The tree is rebuilt from the repository each time the tab opens, so it always reflects the latest reports and statuses.

#### Binary tree and traversals - `AvlTree.cs`

*Role:* A binary tree gives each node at most two children. The AVL tree is a binary tree, and the Tree Structures tab lists its contents in four traversal orders:

- **In-order** (left, node, right) returns requests sorted by reference number. This is how the All Requests list is filled.
- **Pre-order** (node, left, right) shows the root first. With the demo data it starts `1004, 1002, 1001, 1003, 1006, 1005, 1007, 1008`, so #1004 is the root.
- **Post-order** (left, right, node) visits children before their parent.
- **Level-order** visits the tree one level at a time, using a queue.

*Contribution:* In-order traversal means the list never needs a separate sort step, and the other orders make the shape of the tree visible.

#### Binary search tree - `BinarySearchTree.cs`

*Role:* A binary search tree keeps smaller keys on the left and larger keys on the right, so each comparison discards half of the remaining branch, provided the tree is balanced.

*Contribution and limitation:* It is included as a baseline. Reference numbers are issued in ascending order, which is the worst case: each new request is added to the right of the previous one, so the tree becomes a chain. With the eight demo requests the BST has height 8 and finding #1008 takes 8 comparisons. This is the reason the balanced trees below are used.

#### AVL tree - `AvlTree.cs`

*Role:* The AVL tree is the main lookup structure for the status page. After every insert it checks the balance factor (height of the left subtree minus the right subtree) of each node on the path back to the root. If it is above 1 or below -1, it restores balance with a single or double rotation.

*Contribution:* The height stays close to log2(n). With the same eight requests the height is 4 and finding #1008 takes 4 comparisons instead of 8. For 1,000 requests a balanced tree needs about 10 comparisons, compared with up to 1,000 for a chain. The Search box reports the comparison count so the saving can be demonstrated.

*Example:* Inserting 1001, 1002, 1003 in order makes the tree right-heavy at 1001, so a left rotation makes 1002 the root with 1001 and 1003 as its children.

#### Red-Black tree - `RedBlackTree.cs`

*Role:* A Red-Black tree colours each node red or black and enforces three rules: the root is black, no red node has a red child, and every path has the same number of black nodes. Rotations and colour flips restore these rules after an insert. This implementation uses the left-leaning variant.

*Contribution:* Height is guaranteed to stay below 2 x log2(n + 1). It is slightly less strictly balanced than an AVL tree, but it usually performs fewer rotations while inserting. The comparison table shows both: with the demo data finding #1008 takes 3 comparisons in the Red-Black tree and 4 in the AVL tree. AVL trees favour lookups, Red-Black trees favour frequent inserts, and the table lets the user compare them on the same data.

#### Heap (priority queue) - `MinHeap.cs`

*Role:* A binary min-heap stored in a list. The smallest item is always at index 0, and inserting or removing an item only moves it along one path (sift up or sift down). Requests are compared first by priority (1 = Critical to 4 = Low) and then by date reported, so equal priorities are served oldest first.

*Contribution:* Finding the most urgent request is O(1), and adding or removing a request is O(log n). A sorted list would need O(n) to insert. The Priority Queue tab shows the order, and **Start work on most urgent request** dequeues the top item.

*Example:* Request #1003 (Public Safety, Critical) is shown before #1001 (Roads, Medium) even though #1001 was reported earlier. When a request's status changes by other means, its stale heap entry is skipped when it reaches the top.

#### Graph - `RequestGraph.cs`

*Role:* An undirected, weighted graph stored as an adjacency list. Each request is a vertex. When a request is registered, it is linked to existing requests at the same location (weight 1) or in the same category (weight 4). A lower weight means more closely related.

*Contribution:* A tree can only order requests by one key. The graph captures many-to-many relationships, so the municipality can see that a burst pipe and a low-pressure report belong to the same problem area. Finding the neighbours of a request takes time proportional to the number of its links, not the number of requests.

#### Graph traversal - BFS and DFS

*Role:* **Breadth-first search** uses a queue and visits the start request, then everything one link away, then two links away. **Depth-first search** uses a stack and follows one chain of links as far as it goes before backtracking.

*Contribution:* BFS lists the nearest related requests first and shows the number of hops (Start, Hop 1, Hop 2). DFS shows how a chain of related requests continues. Both run in O(V + E) and are shown in the Related Requests tab.

*Example:* Selecting #1002 (Water, Rivonia Road) starts at #1002 and finds #1007 (same location) and #1008 (same category) one hop away. #1003, which shares a location with #1008, is two hops away.

#### Minimum spanning tree - Prim's algorithm

*Role:* A spanning tree connects all the related requests using the fewest links. The minimum spanning tree does this with the smallest total weight. Prim's algorithm starts at the selected request and repeatedly adds the cheapest link that reaches a new request.

*Contribution:* It removes redundant links and shows the strongest set of relationships, which suits grouping requests for a single work crew or visit. The Related Requests tab lists the links and their total weight.

*Example:* For #1002 the tree uses #1002-#1007 (weight 1), #1002-#1008 (weight 4) and #1008-#1003 (weight 1), a total weight of 6. Prim's algorithm always prefers a weight-1 location link to a weight-4 category link.

### 21.3 Efficiency summary

| Operation | Structure | Cost |
|---|---|---|
| Find a request by reference number | AVL / Red-Black tree | O(log n) |
| Same lookup in an unbalanced tree | BST (worst case) | O(n) |
| List all requests in order | In-order traversal | O(n) |
| Find the most urgent request | Min-heap | O(1) |
| Add or remove a request from the queue | Min-heap | O(log n) |
| Explore related requests | BFS / DFS | O(V + E) |
| Link related requests cheaply | Prim's MST | O(V x E) in this simple implementation |

---

## 22. Modernised Styling

All three screens share the same visual design: a dark header bar with the screen title and a Back button, a light grey page background, white bordered cards, Segoe UI typography, flat blue buttons and clean data grids.

- `UI/Theme.cs` holds the colour palette, fonts and helpers for flat buttons and data grids. `Theme.Apply(form)` applies them to every control on a form.
- **Main menu** - rounded option cards drawn by `UI/CardButton.cs`.
- **Report Issues** - a centred form card with labelled fields, a divider between sections, a slim rounded progress bar (`UI/ModernProgressBar.cs`, which turns green at 100%) and a prominent Submit Report button.
- **Local Events and Announcements** - a filter card (category, date, Search, Clear), a full-width events grid and a recommendations card whose headings, notes and events are drawn in different styles.
- **Service Request Status** - built in code, with a status tracker drawn by `UI/StatusStepper.cs`.

The Report Issues and Local Events screens were restyled only. Their control names, event handlers and logic are unchanged, so they behave exactly as in Parts 1 and 2. All screens resize with the window.
