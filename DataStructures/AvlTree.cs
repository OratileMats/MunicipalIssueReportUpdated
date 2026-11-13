using System;
using System.Collections.Generic;

namespace MunicipalIssueReport
{
    /// <summary>
    /// Self-balancing binary search tree (AVL tree) keyed by an integer.
    /// Used to store service requests by reference number so that a
    /// request can be located in O(log n) comparisons.
    /// </summary>
    public class AvlTree<T>
    {
        private class Node
        {
            public int Key;
            public T Value;
            public Node Left;
            public Node Right;
            public int Height = 1;
        }

        private Node root;

        public int Count { get; private set; }

        public int Height
        {
            get { return HeightOf(root); }
        }

        // ---------- Insert ----------

        public void Insert(int key, T value)
        {
            root = Insert(root, key, value);
        }

        private Node Insert(Node node, int key, T value)
        {
            if (node == null)
            {
                Count++;
                return new Node { Key = key, Value = value };
            }

            if (key < node.Key)
            {
                node.Left = Insert(node.Left, key, value);
            }
            else if (key > node.Key)
            {
                node.Right = Insert(node.Right, key, value);
            }
            else
            {
                // Same key: replace the stored value.
                node.Value = value;
                return node;
            }

            return Rebalance(node);
        }

        // ---------- Search ----------

        /// <summary>
        /// Searches for a key. 'comparisons' reports how many nodes were
        /// inspected, which demonstrates the efficiency of the tree.
        /// </summary>
        public bool TryFind(int key, out T value, out int comparisons)
        {
            comparisons = 0;
            Node current = root;

            while (current != null)
            {
                comparisons++;

                if (key == current.Key)
                {
                    value = current.Value;
                    return true;
                }

                current = key < current.Key ? current.Left : current.Right;
            }

            value = default(T);
            return false;
        }

        // ---------- Traversal ----------

        /// <summary>In-order traversal returns values sorted by key.</summary>
        public List<T> InOrder()
        {
            List<T> result = new List<T>();
            InOrder(root, result);
            return result;
        }

        private void InOrder(Node node, List<T> result)
        {
            if (node == null)
            {
                return;
            }

            InOrder(node.Left, result);
            result.Add(node.Value);
            InOrder(node.Right, result);
        }

        /// <summary>Pre-order: node, left, right (shows the tree's shape from the root).</summary>
        public List<T> PreOrder()
        {
            List<T> result = new List<T>();
            PreOrder(root, result);
            return result;
        }

        private void PreOrder(Node node, List<T> result)
        {
            if (node == null)
            {
                return;
            }

            result.Add(node.Value);
            PreOrder(node.Left, result);
            PreOrder(node.Right, result);
        }

        /// <summary>Post-order: left, right, node (children before parents).</summary>
        public List<T> PostOrder()
        {
            List<T> result = new List<T>();
            PostOrder(root, result);
            return result;
        }

        private void PostOrder(Node node, List<T> result)
        {
            if (node == null)
            {
                return;
            }

            PostOrder(node.Left, result);
            PostOrder(node.Right, result);
            result.Add(node.Value);
        }

        /// <summary>Level-order: the tree one level at a time, top to bottom.</summary>
        public List<T> LevelOrder()
        {
            List<T> result = new List<T>();

            if (root == null)
            {
                return result;
            }

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();
                result.Add(current.Value);

                if (current.Left != null)
                {
                    queue.Enqueue(current.Left);
                }

                if (current.Right != null)
                {
                    queue.Enqueue(current.Right);
                }
            }

            return result;
        }

        // ---------- Balancing ----------

        private static int HeightOf(Node node)
        {
            return node == null ? 0 : node.Height;
        }

        private static void UpdateHeight(Node node)
        {
            node.Height = 1 + Math.Max(HeightOf(node.Left), HeightOf(node.Right));
        }

        private static int BalanceFactor(Node node)
        {
            return node == null ? 0 : HeightOf(node.Left) - HeightOf(node.Right);
        }

        private static Node RotateRight(Node y)
        {
            Node x = y.Left;
            y.Left = x.Right;
            x.Right = y;

            UpdateHeight(y);
            UpdateHeight(x);
            return x;
        }

        private static Node RotateLeft(Node x)
        {
            Node y = x.Right;
            x.Right = y.Left;
            y.Left = x;

            UpdateHeight(x);
            UpdateHeight(y);
            return y;
        }

        private static Node Rebalance(Node node)
        {
            UpdateHeight(node);
            int balance = BalanceFactor(node);

            // Left heavy
            if (balance > 1)
            {
                if (BalanceFactor(node.Left) < 0)
                {
                    node.Left = RotateLeft(node.Left);
                }

                return RotateRight(node);
            }

            // Right heavy
            if (balance < -1)
            {
                if (BalanceFactor(node.Right) > 0)
                {
                    node.Right = RotateRight(node.Right);
                }

                return RotateLeft(node);
            }

            return node;
        }
    }
}
