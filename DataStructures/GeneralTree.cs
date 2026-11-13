using System;
using System.Collections.Generic;

namespace MunicipalIssueReport
{
    /// <summary>A node of a general (n-ary) tree: any number of children.</summary>
    public class GeneralTreeNode<T>
    {
        public T Value { get; private set; }
        public List<GeneralTreeNode<T>> Children { get; private set; }

        public GeneralTreeNode(T value)
        {
            Value = value;
            Children = new List<GeneralTreeNode<T>>();
        }

        public GeneralTreeNode<T> AddChild(T value)
        {
            GeneralTreeNode<T> child = new GeneralTreeNode<T>(value);
            Children.Add(child);
            return child;
        }

        public int Height()
        {
            int tallest = 0;

            foreach (GeneralTreeNode<T> child in Children)
            {
                tallest = Math.Max(tallest, child.Height());
            }

            return 1 + tallest;
        }

        public int CountNodes()
        {
            int total = 1;

            foreach (GeneralTreeNode<T> child in Children)
            {
                total += child.CountNodes();
            }

            return total;
        }
    }
}
