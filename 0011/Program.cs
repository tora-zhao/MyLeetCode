using System;

/*
https://leetcode.com/problems/container-with-most-water/
Given n non-negative integers a1, a2, ..., an, where each represents a point at coordinate (i, ai). 
n vertical lines are drawn such that the two endpoints of line i is at (i, ai) and (i, 0). 
Find two lines, which together with x-axis forms a container, 
such that the container contains the most water.

Note: You may not slant the container and n is at least 2. */
namespace _0011
{
    class Solution
    {
        static void Main(string[] args)
        {
            Console.WriteLine(MaxArea2(new int[] {1,8,6,2,5,4,8,3,7}));
        }

        /* 穷举法 */
        public static int MaxArea(int[] height)
        {
            int max = 0;

            for(var i = 0; i < height.Length; i++)
            {
                for(var j = i; j < height.Length; j++)
                {
                    int area = (j-i) * Math.Min(height[i], height[j]);
                    max = area > max ? area : max;
                }
            }
            return max;
        }


        /* 两端逼近法： 
        1.两指针分别指向首尾
        2.比较首尾处的值，将较小的一方的指针向中间移动一位
        3.重复2，直到两指针重合
        关于该算法正确性的基于矩阵的证明: 
        https://leetcode.com/problems/container-with-most-water/discuss/6099/yet-another-way-to-see-what-happens-in-the-on-algorithm
        ps: 因为不明白数学原理，所以考虑出出该方法。*/
        public static int MaxArea2(int[] height)
        {
            int i = 0, j = height.Length - 1, maxArea = 0;
            while(i < j)
            {
                if(height[i] <= height[j])
                {
                    var area = height[i] * (j-i);
                    maxArea = area > maxArea ? area : maxArea;
                    i++;
                }
                else
                {
                    var area = height[j] * (j-i);
                    maxArea = area > maxArea ? area : maxArea;
                    j--;
                }
            }
            return maxArea;
        }
    }
}
