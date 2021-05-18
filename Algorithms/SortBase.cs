using System;
using System.Collections.Generic;

namespace Algorithms
{
    public abstract class SortBase
    {
        public abstract void Sort(IComparable[] a);

        protected static bool Less(IComparable v, IComparable w)
        {
            return v.CompareTo(w) < 0;
        }

        protected static void Exchange(IList<IComparable> a, int i, int j)
        {
            var t = a[i];
            a[i] = a[j];
            a[j] = t;
        }

        protected static void Show(IEnumerable<IComparable> a)
        {
            foreach (var t in a)
            {
                Console.Write($"{t} ");
            }

            Console.WriteLine();
        }

        protected static bool IsSorted(IComparable[] a)
        {
            for (var i = 1; i < a.Length; i++)
            {
                if (Less(a[i], a[i - 1]))
                {
                    return false;
                }
            }

            return true;
        }
        
        protected static IComparable[] Aux;

        protected static void Merge(IComparable[] source, int lo, int mid, int hi)
        {
            int i = lo, j = mid + 1;
            
            // 将指定a复制到b
            for (int k = lo; k <= hi; k++)
            {
                Aux[k] = source[k];
            }
            
            for (int k = lo; k <= hi; k++)
            {
                if (i > mid)
                {
                    source[k] = Aux[j++];
                }
                else if (j > hi)
                {
                    source[k] = Aux[i++];
                }
                else if (Less(Aux[j], Aux[i]))
                {
                    source[k] = Aux[j++];
                }
                else
                {
                    source[k] = Aux[i++];
                }
            }
        }
        
        /*protected void InternalMainTest(string[] args)
        {
            var stdIn = new TextInput();
            var a = stdIn.ReadAllStrings().ToArray<IComparable>();
            Sort(a); 
            Debug.Assert(IsSorted(a));
            Show(a);
        }*/
    }
}