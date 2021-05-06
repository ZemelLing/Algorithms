using System;
using System.Diagnostics;
using System.Linq;

namespace Algorithms
{
    /// <summary>
    /// 插入排序，始终确保左侧分量有序，一次将第 i 个元素与 0 ~ i-1 元素比较交换。
    /// 适合以下输入：
    /// 1. 分量中的元素与其目标位置的距离都不远
    /// 2. 分量由一个有序的大分量和小分量组成
    /// 3. 分量中只有几个元素的位置不正确
    /// </summary>
    public class InsertionSort : SortBase
    {
        public override void Sort(IComparable[] a)
        {
            for (int i = 1; i < a.Length; i++)
            {
                for (var j = i; j > 0 && Less(a[j], a[j - 1]); j--)
                {
                    Exchange(a, j, j - 1);
                }
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