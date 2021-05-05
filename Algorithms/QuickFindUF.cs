using System;

namespace Algorithms
{
    public class QuickFindUF
    {
        private readonly int[] _id;
        private int _count;
        
        public QuickFindUF(int n)
        {
            _id = new int[n];
            for (var i = 0; i < n; i++)
            {
                _id[i] = i;
            }

            _count = n;
        }
        
        public void Union(int p, int q)
        {
            var pId = Find(p);
            var qId = Find(q);

            if (pId == qId) return;
            
            for (var k = 0; k < _id.Length; k++)
            {
                if (_id[k] == pId)
                {
                    _id[k] = qId;
                }
            }

            _count--;
        }

        public int Find(int p)
        {
            return _id[p];
        }

        public bool Connected(int p, int q)
        {
            return Find(p) == Find(q);
        }

        public int Count()
        {
            return _count;
        }
        
        /// <summary>
        /// Reads in a an integer <c>N</c> and a sequence of pairs of integers
        /// (between <c>0</c> and <c>N-1</c>) from standard input, where each integer
        /// in the pair represents some site;
        /// if the sites are in different components, merge the two components
        /// and print the pair to standard output.</summary>
        /// <param name="args">Place holder for user arguments</param>
        ///
        [HelpText("Algorithms.Cmd UF < tinyUF.txt", "N followed by p q pairs")]
        public static void MainTest(string[] args)
        {
            var stdIn = new TextInput();
            var n = stdIn.ReadInt();
            var uf = new QuickFindUF(n);
            while (!stdIn.IsEmpty)
            {
                var p = stdIn.ReadInt();
                var q = stdIn.ReadInt();
                if (uf.Connected(p, q)) continue;
                uf.Union(p, q);
                Console.WriteLine(p + " " + q);
            }
            Console.WriteLine($"{uf.Count()} components");
        }
    }
}