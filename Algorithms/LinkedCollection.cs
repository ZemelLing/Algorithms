using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    public abstract class LinkedCollection<T> : IEnumerable<T>
    {
        protected Node FirstNode;

        protected LinkedCollection()
        {
            Count = 0;
            FirstNode = null;
        }

        public int Count { get; protected set; }

        public bool IsEmpty => FirstNode == null;

        protected virtual void InternalAdd(T item)
        {
            var oldFirstNode = FirstNode;
            FirstNode = new Node {Item = item, Next = oldFirstNode};
            Count++;
        }

        public override string ToString()
        {
            if (IsEmpty) return string.Empty;

            var sb = new StringBuilder();
            var current = FirstNode;
            while (current != null)
            {
                sb.Append($"{current.Item} ");
                current = current.Next;
            }

            sb.Remove(sb.Length - 1, 1); // remove last space
            return sb.ToString();
        }

        protected sealed class Node
        {
            public Node Next { get; set; }
            public T Item { get; set; }
        }

        #region IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            return new CollectionIEnumerator(FirstNode);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class CollectionIEnumerator : IEnumerator<T>
        {
            private readonly Node _first;
            private Node _current;
            private bool _isFirstCall = true;

            public CollectionIEnumerator(Node first)
            {
                _first = first;
            }

            public bool MoveNext()
            {
                if (_isFirstCall)
                {
                    _current = _first;
                    _isFirstCall = false;
                    return _current != null;
                }

                if (_current == null) return false;

                _current = _current.Next;
                return _current != null;
            }

            public void Reset()
            {
                _isFirstCall = true;
                _current = null;
            }

            public T Current
            {
                get
                {
                    if (_current == null)
                        throw new InvalidOperationException("Past end of collection!");
                    return _current.Item;
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }
        }

        #endregion
    }
}