using System;
using System.Diagnostics;
using System.Linq;

namespace Algorithms
{
    /// <summary>
    /// 选择排序，始终在未排序的分量里查找最小元素，与未排序的分量首元素交换，直到分量排序完成。
    /// 特点：
    /// 1. 与输入的无关（并不是与大小无关，而是输入的已有顺序无关）
    /// 2. 最少的数据移动
    /// </summary>
    public class SelectionSort : SortBase
    {
        /// <summary>
        /// 假设 a 长度为 n，选择排序大约需要 (n^2)/2 比较和 n 次交换
        /// </summary>
        /// <param name="a"></param>
        public override void Sort(IComparable[] a)
        {
            for (var i = 0; i < a.Length - 1; i++)
            {
                var min = i;
                for (var j = i + 1; j < a.Length; j++)
                {
                    if (Less(a[j], a[min]))
                    {
                        min = j;
                    }
                }
                Exchange(a, min, i);
            }
        }
        
        [HelpText("algscmd Heap < words3.txt", "Input strings to be printed in sorted order")]
        public static void MainTest(string[] args)
        {
            var stdIn = new TextInput();
            var a = stdIn.ReadAllStrings().ToArray<IComparable>();
            var ss = new SelectionSort();
            ss.Sort(a);
            Debug.Assert(IsSorted(a));
            Show(a);
        }
    }
}