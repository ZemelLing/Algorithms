using System;

namespace Algorithms
{
    public class LinkedQueue<T> : LinkedCollection<T>
    {
        private Node _lastNode;

        public LinkedQueue()
        {
            _lastNode = null;
        }

        public void Enqueue(T item)
        {
            var oldLast = _lastNode;
            _lastNode = new Node {Item = item, Next = null};
            if (IsEmpty)
                FirstNode = _lastNode;
            else
                oldLast.Next = _lastNode;

            Count++;
        }

        public T Dequeue()
        {
            if (IsEmpty)
                throw new IndexOutOfRangeException("The queue is empty can't dequeue.");

            var result = FirstNode.Item;
            FirstNode = FirstNode.Next;
            if (IsEmpty) _lastNode = FirstNode;
            Count--;
            return result;
        }

        /// <summary>
        ///     Demo test the <c>LinkedQueue</c> data type.
        /// </summary>
        /// <param name="args">Place holder for user arguments</param>
        [HelpText("Algorithms.Cmd LinkedQueue < tobe.txt", "Items separated by space or new line")]
        public static void MainTest(string[] args)
        {
            var q = new LinkedQueue<string>();
            var StdIn = new TextInput();
            while (!StdIn.IsEmpty)
            {
                var item = StdIn.ReadString();
                if (!item.Equals("-")) q.Enqueue(item);
                else if (!q.IsEmpty) Console.Write(q.Dequeue() + " ");
            }

            Console.WriteLine("(" + q.Count + " left on queue)");
        }
    }
}