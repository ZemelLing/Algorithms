using System;

namespace Algorithms
{
    public class QuickUnionUF
    {
        private readonly int[] _id;
        private int _count;
        
        public QuickUnionUF(int n)
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
            var pRoot = Find(p);
            var qRoot = Find(q);

            if (pRoot == qRoot) return;
            
            // 其中一个分量的根触点指向另一个分量的根触点
            _id[pRoot] = qRoot;

            _count--;
        }

        public int Find(int p)
        {
            while (p != _id[p])
            {
                p = _id[p];
            }

            return p;
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
            var uf = new QuickUnionUF(n);
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