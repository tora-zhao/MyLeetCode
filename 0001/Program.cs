using System;
using System.Collections.Generic;

/*
https://leetcode.com/problems/two-sum/
Given an array of integers, return indices of the two numbers such that they add up to a specific target.
You may assume that each input would have exactly one solution, and you may not use the same element twice.

Example:

Given nums = [2, 7, 11, 15], target = 9,
Because nums[0] + nums[1] = 2 + 7 = 9,
return [0, 1].
 */
namespace _0001
{
    public class Solution
    {
        static void Main(string[] args)
        {
            int[] nums =  {2, 7, 3, 11, 15};
            var result = TwoSum(nums, 14);
            foreach(var i in result)
            {
                Console.WriteLine(i);
            }
        }

        /* 利用字典保存{数值:位置}信息，两次遍历，tc=O(n) */
		public static int[] TwoSum(int[] nums, int target) 
		{
            var dic = new Dictionary<int, List<int>>(nums.Length);
            for(int i = 0; i < nums.Length; i++)
            {
                if(dic.ContainsKey(nums[i]))
                {
                    dic[nums[i]].Add(i);
                } 
                else 
                {
                    dic.Add(nums[i], new List<int>(1) {i});
                }
            }
            

            foreach(var key in dic.Keys) 
            {
                Console.WriteLine("key: {0}, value:{1}", key, dic[key]);
                if( dic.ContainsKey(target - key)) {
                    if(dic[key].Count > 1)
                    {
                        return dic[key].ToArray();
                    } 
                    else if (target - key != key)
                    {
                        dic[key].AddRange(dic[target-key]);
                        return dic[key].ToArray();
                    }
                }    
            }
            return null;
		}

        /* 最无脑方法，tc=O(n^2) */
        public static int[] TwoSum2(int[] nums, int target) 
        {
            for(int i = 0; i< nums.Length; i++) 
            {
                for(int j = i + 1; j < nums.Length; j++) 
                {
                    if(nums[i] + nums[j] == target) {
                        return new int[] {i, j};
                    }
                }
            }
            return null;
        }

        /* （最优解？）一次遍历即可，
        Dictionary.Add方法不能覆盖已存在的key，所以虽然开始想到了该法但是没有通过测试case */
        public static int[] TwoSum3(int[] nums, int target) 
		{
            var dic = new Dictionary<int, int>(nums.Length);
            for(int i = 0; i < nums.Length; i++)
            {
                if(dic.ContainsKey(target - nums[i]) )
                {
                   return new int[] {i, dic[target - nums[i]]};
                } 
                else 
                {
                    dic[nums[i]] = i;
                }
            }
            return null;
        }
    }
}
