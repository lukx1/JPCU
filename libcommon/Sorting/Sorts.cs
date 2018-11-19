using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libcommon.Sorting
{
    public static class Sorts
    {

        /*public static void MergeSort(int[] arr)
        {
            MergeSort(arr, 0, arr.Length-1);
        }
        */
        private static void Merge<T>(T[] arr, int left, int mid, int right, Func<T,T,bool> comparator)
        {
            T[] larr = new T[mid - left + 1];
            T[] rarr = new T[right - mid];

            for (int i = 0; i < larr.Length; i++)
            {
                larr[i] = arr[left + i];
            }

            for (int i = 0; i < rarr.Length; i++)
            {
                rarr[i] = arr[mid + 1 + i];
            }

            int lpos = 0;
            int rpos = 0;
            int opos = left;

            /*for (; lpos < larr.Length && rpos < rarr.Length;opos++)
            {
                if (comparator(larr[lpos], rarr[rpos]))
                {
                    arr[opos] = larr[lpos++];
                }
                else
                {
                    arr[opos] = rarr[rpos++];
                }
            }*/

            while (lpos < larr.Length && rpos < rarr.Length)
            {
                if (comparator(larr[lpos],rarr[rpos]))
                {
                    arr[opos++] = larr[lpos++];
                }
                else
                {
                    arr[opos++] = rarr[rpos++];
                }
            }

            for (; lpos < larr.Length; lpos++)
            {
                arr[opos++] = larr[lpos];
            }

            for (; rpos < rarr.Length; rpos++)
            {
                arr[opos++] = rarr[rpos];
            }
        }

        public static void MergeSort(int[] arr)
        {
            MergeSort(arr, 0, arr.Length - 1, (int a, int b) => a <= b);
        }

        public static void MergeSort<T>(T[] arr, Func<T,T,bool> comparator)
        {
            MergeSort(arr, 0, arr.Length - 1,comparator);
        }

        public static void MergeSort<T>(T[] arr, int left,  int right, Func<T, T, bool> comparator)
        {
            if (left < right)
            {
                int mid = (left + right) / 2;
                MergeSort(arr, left, mid,comparator);
                MergeSort(arr, mid + 1, right,comparator);
                Merge<T>(arr, left, mid, right,comparator);
            }
        }

    }
}
