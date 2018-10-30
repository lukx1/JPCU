using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libcommon.Sorting
{
    public static class Sorts
    {

        public static void MergeSort(int[] arr)
        {
            MergeSort(arr, 0, arr.Length);
        }

        private static void Merge(int[] arr, int left,int mid, int right)
        {
            int[] larr = new int[mid - left + 1];
            int[] rarr = new int[right - mid];

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
            
            while(lpos < larr.Length && rpos < rarr.Length)
            {
                if(larr[lpos] <= rarr[rpos])
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

        private static void MergeSort(int[] arr,int left,int right)
        {
            if (right > 1)
            {
                int mid = (left + right) / 2;
                if (left < right)
                {
                    MergeSort(arr, left, mid);
                    MergeSort(arr, mid + 1, right);
                    Merge(arr, left, mid, right);
                }
            }
        }
    }
}
