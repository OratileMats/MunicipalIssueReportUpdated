using System;
using System.Collections.Generic;

namespace MunicipalIssueReport
{
    /// <summary>
    /// Binary min-heap. The smallest item (according to the supplied
    /// comparison) is always at the top, so the most urgent service
    /// request can be retrieved in O(log n).
    /// </summary>
    public class MinHeap<T>
    {
        private readonly List<T> items = new List<T>();
        private readonly Comparison<T> comparison;

        public MinHeap(Comparison<T> comparison)
        {
            this.comparison = comparison;
        }

        public int Count
        {
            get { return items.Count; }
        }

        public void Enqueue(T item)
        {
            items.Add(item);
            SiftUp(items.Count - 1);
        }

        public T Peek()
        {
            if (items.Count == 0)
            {
                throw new InvalidOperationException("The heap is empty.");
            }

            return items[0];
        }

        public T Dequeue()
        {
            if (items.Count == 0)
            {
                throw new InvalidOperationException("The heap is empty.");
            }

            T top = items[0];
            int last = items.Count - 1;

            items[0] = items[last];
            items.RemoveAt(last);

            if (items.Count > 0)
            {
                SiftDown(0);
            }

            return top;
        }

        /// <summary>Creates an independent copy so it can be drained for display.</summary>
        public MinHeap<T> Clone()
        {
            MinHeap<T> copy = new MinHeap<T>(comparison);
            copy.items.AddRange(items);
            return copy;
        }

        private void SiftUp(int index)
        {
            while (index > 0)
            {
                int parent = (index - 1) / 2;

                if (comparison(items[index], items[parent]) >= 0)
                {
                    break;
                }

                Swap(index, parent);
                index = parent;
            }
        }

        private void SiftDown(int index)
        {
            int count = items.Count;

            while (true)
            {
                int left = 2 * index + 1;
                int right = left + 1;
                int smallest = index;

                if (left < count && comparison(items[left], items[smallest]) < 0)
                {
                    smallest = left;
                }

                if (right < count && comparison(items[right], items[smallest]) < 0)
                {
                    smallest = right;
                }

                if (smallest == index)
                {
                    break;
                }

                Swap(index, smallest);
                index = smallest;
            }
        }

        private void Swap(int a, int b)
        {
            T temp = items[a];
            items[a] = items[b];
            items[b] = temp;
        }
    }
}
