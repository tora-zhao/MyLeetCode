using System;
using System.Collections.Generic;

/*
https://leetcode.com/problems/median-of-two-sorted-arrays/
There are two sorted arrays nums1 and nums2 of size m and n respectively.
Find the median of the two sorted arrays. The overall run time complexity should be O(log (m+n)).
You may assume nums1 and nums2 cannot be both empty.

Example 1:
nums1 = [1, 3]
nums2 = [2]

The median is 2.0
Example 2:

nums1 = [1, 2]
nums2 = [3, 4]
The median is (2 + 3)/2 = 2.5 
*/
namespace _0004
{
    class Solution
    {
        public static void Main(string[] args)
        {
            int[] nums1 = { 1, 3 };
            int[] nums2 = { 2 };
            Console.WriteLine(FindMedianSortedArrays2(nums1, nums2));
        }

        /* 两个指针指向两个数组，比较指针指向的两个数并向后移动较小一方的指针，直至两个指针全部移动到最后 
        时间复杂度为O(m+n)，不符合题目要求*/
        public static double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            var merge = new List<int>(nums1.Length + nums2.Length);
            int i = 0, j = 0;
            while (i < nums1.Length || j < nums2.Length)
            {
                if (i < nums1.Length && j < nums2.Length)
                {
                    if (nums1[i] < nums2[j])
                    {
                        merge.Add(nums1[i++]);
                    }
                    else if (nums1[i] > nums2[j])
                    {
                        merge.Add(nums2[j++]);
                    }
                    else
                    {
                        merge.Add(nums1[i++]);
                        merge.Add(nums2[j++]);
                    }
                }
                else if (i < nums1.Length)
                {
                    merge.Add(nums1[i++]);
                }
                else if (j < nums2.Length)
                {
                    merge.Add(nums2[j++]);
                }
            }

            var len = merge.Count;
            if (len % 2 == 0)
            {
                return (merge[(len >> 1) - 1] + merge[len >> 1]) / 2.0;
            }
            else
            {
                return merge[(len - 1) >> 1];
            }
        }

        public static double FindMedianSortedArrays2(int[] nums1, int[] nums2)
        {
            int i = 0, j = 0;
            int curr = 0, prev = 0, count = 0;
            int len = nums1.Length + nums2.Length;
            while (i < nums1.Length || j < nums2.Length)
            {
                prev = curr;
                if (i < nums1.Length && j < nums2.Length)
                {
                    if (nums1[i] < nums2[j])
                    {
                        curr = nums1[i++];
                    }
                    else if (nums1[i] > nums2[j])
                    {
                        curr = nums2[j++];
                    }
                    else
                    {
                        prev = nums1[i++];
                        curr = nums2[j++];
                        count ++;
                    }
                }
                else if (i < nums1.Length)
                {
                    curr = nums1[i++];
                }
                else if (j < nums2.Length)
                {
                    curr = nums2[j++];
                }

                count ++;
                if (len % 2 == 0 && count == (len >> 1) + 1) 
                {
                    return (curr + prev) / 2.0;
                }
                else if (len % 2 != 0 && count == (len + 1) >> 1) 
                {
                    return curr;
                }
            }
            return 0;
        }
        // [1,2,5] [3,4,6]
        // [1,2,3] [5,4,6]
    }
}
