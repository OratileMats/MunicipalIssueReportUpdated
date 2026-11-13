using System;
using System.Collections.Generic;

namespace MunicipalIssueReport
{
    /// <summary>
    /// Plain (unbalanced) binary search tree. It is included next to the
    /// AVL and Red-Black trees so the Service Request Status screen can show
    /// why balancing matters: inserting reference numbers in ascending order
    /// turns this tree into a long chain.
    /// </summary>
    public class BinarySearchTree<T>
    {
        private class Node
        {
            public int Key;
            public T Value;
            public Node Left;
            public Node Right;
        }

        private Node root;

        public int Count { get; private set; }

        public int Height
        {
            get { return HeightOf(root); }
        }

        public void Insert(int key, T value)
        {
            if (root == null)
            {
                root = new Node { Key = key, Value = value };
                Count++;
                return;
            }

            Node current = root;

            while (true)
            {
                if (key == current.Key)
                {
                    current.Value = value;
                    return;
                }

                if (key < current.Key)
                {
                    if (current.Left == null)
                    {
                        current.Left = new Node { Key = key, Value = value };
                        Count++;
                        return;
                    }

                    current = current.Left;
                }
                else
                {
                    if (current.Right == null)
                    {
                        current.Right = new Node { Key = key, Value = value };
                        Count++;
                        return;
                    }

                    current = current.Right;
                }
            }
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

        private static int HeightOf(Node node)
        {
            return node == null ? 0 : 1 + Math.Max(HeightOf(node.Left), HeightOf(node.Right));
        }
    }
}
