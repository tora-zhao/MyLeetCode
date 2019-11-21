using System;
using System.Text;
using System.Collections.Generic;

/* 
https://leetcode.com/problems/longest-common-prefix/
Write a function to find the longest common prefix string amongst an array of strings.

If there is no common prefix, return an empty string "".

Example 1:
Input: ["flower","flow","flight"]
Output: "fl"

Example 2:
Input: ["dog","racecar","car"]
Output: ""
Explanation: There is no common prefix among the input strings.

Note:
All given inputs are in lowercase letters a-z. */
namespace _0014
{
    class Solution
    {
        static void Main(string[] args)
        {
            Console.WriteLine(LongestCommonPrefix2(new string[] { "flower", "flow", "flight" }));
        }

        /*  直接比较法, 时间复杂度O(n^2) */
        public static string LongestCommonPrefix(string[] strs)
        {
            if (strs == null || strs.Length == 0)
            {
                return "";
            }

            var minLength = int.MaxValue;
            foreach (var str in strs)
            {
                minLength = str.Length < minLength ? str.Length : minLength;
            }

            var commonPrefix = new StringBuilder();
            bool noCommonChar = false;
            for (var i = 0; i < minLength; i++)
            {
                var comparator = strs[0][i];
                foreach (var str in strs)
                {
                    if (str[i] != comparator)
                    {
                        noCommonChar = true;
                        break;
                    }
                }

                if (noCommonChar)
                {
                    break;
                }

                commonPrefix.Append(comparator);
            }

            return commonPrefix.ToString();
        }


        /* 
        递归,时间复杂度O(S) where S = m * n 
        空间复杂度 O(m * logn) 
        复杂度解释参见参考答案4 */
        public static string LongestCommonPrefix2(string[] strs)
        {
            if (strs == null || strs.Length == 0)
            {
                return "";
            }

            var list = new List<string>(strs);
            return LongestCommonPrefix(list);
        }

        private static string LongestCommonPrefix(List<string> list)
        {
            if (list.Count == 1)
            {
                return list[0];
            }

            if (list.Count == 2)
            {
                return LongestCommonPrefix(list[0], list[1]);
            }

            return LongestCommonPrefix(LongestCommonPrefix(list.GetRange(1, list.Count - 1)), list[0]);
        }

        private static string LongestCommonPrefix(string s1, string s2)
        {
            var commonPrefix = new StringBuilder();
            var i = 0;
            while (i < s1.Length && i < s2.Length)
            {
                if (s1[i] == s2[i])
                {
                    commonPrefix.Append(s1[i]);
                    i++;
                }
                else
                {
                    break;
                }
            }

            return commonPrefix.ToString();
        }

        /* 参考答案1 “水平扫描法” 最优解？
        时间复杂度O(S) where S = m * n 
        空间复杂度 O(1) 
         */
        public static string LongestCommonPrefix3(string[] strs)
        {
            if (strs == null || strs.Length == 0)
            {
                return "";
            }

            var prefix = strs[0];
            for (int i = 1; i < strs.Length; i++)
            {
                while (strs[i].IndexOf(prefix) != 0)
                {
                    prefix = prefix.Substring(0, prefix.Length - 1);
                    if (string.IsNullOrEmpty(prefix)) return "";
                }
            }

            return prefix;
        }
    }
}
