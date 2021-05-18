using System;
using System.Diagnostics;
using System.Linq;

namespace Algorithms
{
    public class QuickSort : SortBase
    {
        public override void Sort(IComparable[] a)
        {
            Sort(a, 0, a.Length - 1);
        }

        private void Sort(IComparable[] a, int lo, int hi)
        {
            if (hi <= lo) return;
            
            var anchor = Partition(a, lo, hi);
            Sort(a, lo, anchor - 1);
            Sort(a, anchor + 1, hi);
        }
        
        private static int Partition(IComparable[] a, int lo, int hi)
        {
            if(hi <= lo) return lo;
            
            var anchor = a[lo];
            var left = lo;
            var right = hi + 1;
            while (true)
            {
                // 从左向右扫描，直到碰到不小于锚点的元素
                while (Less(a[++left], anchor))
                {
                    if (left == hi) break;
                }
                
                // 从右向左扫描，直到碰到不大于锚点的元素
                while (Less(anchor, a[--right]))
                {
                    if (right == lo) break;
                }

                // 交汇点
                if (left >= right) break;
                
                // 此时 right元素不大于锚点元素，且 left元素不小于锚点元素
                // 交换元素后继续扫描
                Exchange(a, left, right);
            }
            Exchange(a, lo, right);

            return right;
        }
        
        [HelpText("algscmd MergeSort < words3.txt", "Input strings to be printed in sorted order")]
        public static void MainTest(string[] args)
        {
            var stdIn = new TextInput();
            var a = stdIn.ReadAllStrings().ToArray<IComparable>();
            var ss = new QuickSort();
            ss.Sort(a);
            Debug.Assert(IsSorted(a));
            Show(a);
        }
    }
}