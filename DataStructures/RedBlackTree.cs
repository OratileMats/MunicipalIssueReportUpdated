using System;

namespace MunicipalIssueReport
{
    /// <summary>
    /// Red-Black tree (left-leaning variant). Every node is red or black,
    /// the root is black and no red node has a red child, which keeps the
    /// tree height within 2 x log2(n). Rebalancing uses rotations and
    /// colour flips, so it does fewer rotations than an AVL tree on insert.
    /// </summary>
    public class RedBlackTree<T>
    {
        private const bool Red = true;
        private const bool Black = false;

        private class Node
        {
            public int Key;
            public T Value;
            public Node Left;
            public Node Right;
            public bool Color;
        }

        private Node root;

        public int Count { get; private set; }

        public int Height
        {
            get { return HeightOf(root); }
        }

        public void Insert(int key, T value)
        {
            root = Insert(root, key, value);
            root.Color = Black;   // rule: the root is always black
        }

        private Node Insert(Node node, int key, T value)
        {
            if (node == null)
            {
                Count++;
                return new Node { Key = key, Value = value, Color = Red };
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
                node.Value = value;
            }

            // Restore the red-black rules on the way back up.
            if (IsRed(node.Right) && !IsRed(node.Left))
            {
                node = RotateLeft(node);
            }

            if (IsRed(node.Left) && IsRed(node.Left.Left))
            {
                node = RotateRight(node);
            }

            if (IsRed(node.Left) && IsRed(node.Right))
            {
                FlipColours(node);
            }

            return node;
        }

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

        /// <summary>True if the key's node is red (used for display/teaching).</summary>
        public bool IsRedNode(int key)
        {
            Node current = root;

            while (current != null)
            {
                if (key == current.Key)
                {
                    return current.Color == Red;
                }

                current = key < current.Key ? current.Left : current.Right;
            }

            return false;
        }

        private static bool IsRed(Node node)
        {
            return node != null && node.Color == Red;
        }

        private static Node RotateLeft(Node h)
        {
            Node x = h.Right;
            h.Right = x.Left;
            x.Left = h;
            x.Color = h.Color;
            h.Color = Red;
            return x;
        }

        private static Node RotateRight(Node h)
        {
            Node x = h.Left;
            h.Left = x.Right;
            x.Right = h;
            x.Color = h.Color;
            h.Color = Red;
            return x;
        }

        private static void FlipColours(Node h)
        {
            h.Color = Red;
            h.Left.Color = Black;
            h.Right.Color = Black;
        }

        private static int HeightOf(Node node)
        {
            return node == null ? 0 : 1 + Math.Max(HeightOf(node.Left), HeightOf(node.Right));
        }
    }
}
