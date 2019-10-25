using System;
using System.Collections.Generic;

/*
https://leetcode.com/problems/longest-substring-without-repeating-characters/
Given a string, find the length of the longest substring without repeating characters.

Example 1:
Input: "abcabcbb"
Output: 3 
Explanation: The answer is "abc", with the length of 3. 

Example 2:
Input: "bbbbb"
Output: 1
Explanation: The answer is "b", with the length of 1.

Example 3:
Input: "pwwkew"
Output: 3
Explanation: The answer is "wke", with the length of 3. 
             Note that the answer must be a substring, "pwke" is a subsequence and not a substring. 
*/
namespace _0003
{
    public class Solution
    {
        static void Main(string[] args)
        {
            Console.WriteLine(LengthOfLongestSubstring3("tmmzuxt"));
        }

        /* 穷举法，从第一个字符开始，利用一个hashset检查之后的字符是否重复，
        若重复则停止，从下一个字符开始再次检查 */
        public static int LengthOfLongestSubstring(string s)
        {
            if(string.IsNullOrEmpty(s))
            {
                return 0;
            }

            var max = 1;
            var dict = new HashSet<char>(s.Length);
            for(var i = 0; i< s.Length; i++)
            {
               for(var j  = i; j < s.Length; j++) 
               {
                   if(!dict.Contains(s[j]))
                   {
                       dict.Add(s[j]);
                   }
                   else
                   {
                       max = dict.Count > max ? dict.Count : max;
                       dict.Clear();
                       break;
                   }
               }
               max = dict.Count > max ? dict.Count : max;
            } 

            return max;
        }

        /* abcbce  两个指针i、j， i指向子字符串首，j指向子字符串尾。
        向后移动j，当j字符与之前的某字符c重复时，则把i挪至重复字符c之后，同时记下该不重复子字符串长度。
        然后继续后移j，以此类推找出最长的不重复子字符串。
        用到一个字典来保存字符的位置信息 */
        public static int LengthOfLongestSubstring2(string s)
        {
            if(string.IsNullOrEmpty(s))
            {
                return 0;
            }

            var dict = new Dictionary<char, int>(s.Length);
            int max = 0, len = 0;
            for(int i = 0, j = 0; j < s.Length; j++)
            {
                if(!dict.ContainsKey(s[j]))
                {
                    dict[s[j]] = j;
                    len = j - i + 1;
                    max = len > max ? len : max;
                }
                else
                {
                    // 出现重复字符时
                    // 需要将重复字符第一次出现的位置之前的字符全部从字典中删除
                    var k = dict[s[j]] + 1;
                    while(i < k)
                    {
                        dict.Remove(s[i++]);
                    }
                    dict[s[j]] = j;
                }

            }

            return max;
        }

        /* leetcode推荐最佳解答 */
        public static int LengthOfLongestSubstring3(string s)
        {
            if(string.IsNullOrEmpty(s))
            {
                return 0;
            }

            var dict = new Dictionary<char, int>(s.Length);
            int ans = 0 ;
            for(int i = 0, j = 0; j < s.Length; j++)
            {
                if(dict.ContainsKey(s[j]))
                {
                   i = Math.Max(dict[s[j]], i);
                }
                 ans = Math.Max(ans, j-i+1);
                 dict[s[j]] = j + 1;
            }

            return ans;
        }
    }
}
