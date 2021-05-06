using System;
using System.Diagnostics;
using System.Linq;

namespace Algorithms
{
    /// <summary>
    /// 希尔排序，本质是多次插入排序，每次插入排序，都使得元素更接近目标位置
    /// </summary>
    public class ShellSort : SortBase
    {
        public override void Sort(IComparable[] a)
        {
            var n = a.Length;
            var h = 1;
            while (h < n/3)
            {
                h = 3 * h + 1;
            }

            while (h >= 1)
            {
                for (var i = h; i < n; i++)
                {
                    for (var j = i; j >= h && Less(a[j], a[j - h]); j -= h)
                    {
                        Exchange(a, j, j - h);
                    }
                }
                
                h /= 3;
            }
        }
        
        [HelpText("algscmd Heap < words3.txt", "Input strings to be printed in sorted order")]
        public static void MainTest(string[] args)
        {
            var stdIn = new TextInput();
            var a = stdIn.ReadAllStrings().ToArray<IComparable>();
            var ss = new ShellSort();
            ss.Sort(a);
            Debug.Assert(IsSorted(a));
            Show(a);
        }
    }
}