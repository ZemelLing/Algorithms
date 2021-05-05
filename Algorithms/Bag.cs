using System;

namespace Algorithms
{
    public class Bag<T> : LinkedCollection<T>
    {
        public void Add(T item)
        {
            InternalAdd(item);
        }

        [HelpText(@"Algorithms.Cmd Bag < tobe.txt", "Items separated by space or new line")]
        public static void MainTest()
        {
            var stdIn = new TextInput();
            var bag = new Bag<string>();
            while (!stdIn.IsEmpty)
            {
                var item = stdIn.ReadString();
                bag.InternalAdd(item);
            }

            Console.WriteLine("Size of bag = " + bag.Count);
            foreach (var s in bag) Console.WriteLine(s);
        }
    }
}