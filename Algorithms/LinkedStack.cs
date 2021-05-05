using System;

namespace Algorithms
{
    /// <summary>
    ///     Linked Stack (LIFO)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LinkedStack<T> : LinkedCollection<T>
    {
        public void Push(T item)
        {
            InternalAdd(item);
        }

        public T Pop()
        {
            if (IsEmpty)
                throw new IndexOutOfRangeException("The stack is empty can't pop.");

            var oldFirst = FirstNode;
            FirstNode = oldFirst.Next;
            Count--;
            return oldFirst.Item;
        }

        /// <summary>
        ///     Demo test the <c>LinkedStack</c> data type.
        /// </summary>
        /// <param name="args">Place holder for user arguments</param>
        [HelpText("Algorithms.Cmd LinkedStack < tobe.txt", "Items separated by space or new line")]
        public static void MainTest(string[] args)
        {
            var s = new LinkedStack<string>();

            var stdIn = new TextInput();
            while (!stdIn.IsEmpty)
            {
                var item = stdIn.ReadString();
                if (!item.Equals("-")) s.Push(item);
                else if (!s.IsEmpty) Console.Write(s.Pop() + " ");
            }

            Console.WriteLine("(" + s.Count + " left on stack)");
        }
    }
}