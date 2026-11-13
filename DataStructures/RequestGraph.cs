using System;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalIssueReport
{
    /// <summary>A weighted, undirected link between two related requests.</summary>
    public class GraphEdge
    {
        public int From { get; set; }
        public int To { get; set; }
        public int Weight { get; set; }
    }

    /// <summary>
    /// Undirected weighted graph (adjacency list) that links related
    /// service requests. Provides BFS, DFS and a minimum spanning tree
    /// (Prim's algorithm) over the connected group of related requests.
    /// </summary>
    public class RequestGraph
    {
        private readonly Dictionary<int, List<GraphEdge>> adjacency =
            new Dictionary<int, List<GraphEdge>>();

        public void AddVertex(int id)
        {
            if (!adjacency.ContainsKey(id))
            {
                adjacency[id] = new List<GraphEdge>();
            }
        }

        public bool HasVertex(int id)
        {
            return adjacency.ContainsKey(id);
        }

        public void AddEdge(int a, int b, int weight)
        {
            if (a == b)
            {
                return;
            }

            AddVertex(a);
            AddVertex(b);

            // Avoid duplicate links.
            if (adjacency[a].Any(e => e.To == b))
            {
                return;
            }

            adjacency[a].Add(new GraphEdge { From = a, To = b, Weight = weight });
            adjacency[b].Add(new GraphEdge { From = b, To = a, Weight = weight });
        }

        public List<GraphEdge> Neighbours(int id)
        {
            List<GraphEdge> edges;
            return adjacency.TryGetValue(id, out edges)
                ? new List<GraphEdge>(edges)
                : new List<GraphEdge>();
        }

        /// <summary>
        /// Breadth-first traversal. Returns each reachable request with its
        /// number of hops from the start (nearest requests first).
        /// </summary>
        public List<KeyValuePair<int, int>> Bfs(int start)
        {
            List<KeyValuePair<int, int>> order = new List<KeyValuePair<int, int>>();

            if (!adjacency.ContainsKey(start))
            {
                return order;
            }

            HashSet<int> visited = new HashSet<int> { start };
            Queue<KeyValuePair<int, int>> queue = new Queue<KeyValuePair<int, int>>();
            queue.Enqueue(new KeyValuePair<int, int>(start, 0));

            while (queue.Count > 0)
            {
                KeyValuePair<int, int> current = queue.Dequeue();
                order.Add(current);

                foreach (GraphEdge edge in adjacency[current.Key].OrderBy(e => e.To))
                {
                    if (visited.Add(edge.To))
                    {
                        queue.Enqueue(new KeyValuePair<int, int>(edge.To, current.Value + 1));
                    }
                }
            }

            return order;
        }

        /// <summary>Depth-first traversal (explicit stack, no recursion).</summary>
        public List<int> Dfs(int start)
        {
            List<int> order = new List<int>();

            if (!adjacency.ContainsKey(start))
            {
                return order;
            }

            HashSet<int> visited = new HashSet<int>();
            Stack<int> stack = new Stack<int>();
            stack.Push(start);

            while (stack.Count > 0)
            {
                int current = stack.Pop();

                if (!visited.Add(current))
                {
                    continue;
                }

                order.Add(current);

                // Push in descending order so the lowest id is visited first.
                foreach (GraphEdge edge in adjacency[current].OrderByDescending(e => e.To))
                {
                    if (!visited.Contains(edge.To))
                    {
                        stack.Push(edge.To);
                    }
                }
            }

            return order;
        }

        /// <summary>
        /// Prim's algorithm: the cheapest set of links that connects every
        /// request reachable from 'start'.
        /// </summary>
        public List<GraphEdge> MinimumSpanningTree(int start)
        {
            List<GraphEdge> tree = new List<GraphEdge>();

            if (!adjacency.ContainsKey(start))
            {
                return tree;
            }

            HashSet<int> inTree = new HashSet<int> { start };

            while (true)
            {
                GraphEdge best = null;

                foreach (int vertex in inTree)
                {
                    foreach (GraphEdge edge in adjacency[vertex])
                    {
                        if (inTree.Contains(edge.To))
                        {
                            continue;
                        }

                        if (best == null
                            || edge.Weight < best.Weight
                            || (edge.Weight == best.Weight && edge.To < best.To))
                        {
                            best = edge;
                        }
                    }
                }

                if (best == null)
                {
                    break;
                }

                tree.Add(best);
                inTree.Add(best.To);
            }

            return tree;
        }
    }
}
