using System;
using System.Diagnostics;
using System.Linq;

namespace Algorithms
{
    public class MergeSort : SortBase
    {
        public override void Sort(IComparable[] a)
        {
            Aux = new IComparable[a.Length];
            Sort(a, 0, a.Length - 1);
        }

        private void Sort(IComparable[] a, int lo, int hi)
        {
            if (hi <= lo)
                return;

            var mid = lo + (hi - lo) / 2;
            Sort(a, lo, mid);
            Sort(a, mid + 1, hi);
            Merge(a, lo, mid, hi);
        }
        
        [HelpText("algscmd MergeSort < words3.txt", "Input strings to be printed in sorted order")]
        public static void MainTest(string[] args)
        {
            var stdIn = new TextInput();
            var a = stdIn.ReadAllStrings().ToArray<IComparable>();
            var ss = new MergeSort();
            ss.Sort(a);
            Debug.Assert(IsSorted(a));
            Show(a);
        }
    }
}