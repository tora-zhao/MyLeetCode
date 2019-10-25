using System;

/*
https://leetcode.com/problems/longest-palindromic-substring/
Given a string s, find the longest palindromic substring in s. You may assume that the maximum length of s is 1000.

Example 1:

Input: "babad"
Output: "bab"
Note: "aba" is also a valid answer.
Example 2:

Input: "cbbd"
Output: "bb" 
*/
namespace _0005
{
    class Solution
    {
        static void Main(string[] args)
        {
            Console.WriteLine(LongestPalindrome2("cbbd"));
        }

        /* 遍历字符串中的字符，寻找每个字符为中心的最长字符串 */
        public static string LongestPalindrome(string s)
        {
            var result = "";
            for(var i = 0; i < s.Length; i ++)
            {
                var temp = getLongestPalindromic(s, i);
                result = result.Length >= temp.Length ? result : temp;
            }
            return result;
        }

        /* 遍历字符串中的字符，寻找每个字符为中心的最长字符串 */
        public static string LongestPalindrome2(string s)
        {
            int[] result = {0, 0};
            for(var i = 0; i < s.Length; i ++)
            {
                var temp = getLongestPalindromicIndice(s, i);
                result = result[1] >= temp[1] ? result : temp;
            }
            return s.Substring(result[0], result[1]);
        }

        /* 从一个字符串中s以位置position为中心的最长回文字符串 */
        public static string getLongestPalindromic(string s, int position) 
        {
            if(string.IsNullOrEmpty(s))
            {
                return "";
            }
            if(position < 0 || position >= s.Length)
            {
                throw new IndexOutOfRangeException();
            }
            
            // 以position为正中心的奇数回文字符串
            var i = 0;
            while( position - 1 - i >= 0 && position + 1 + i < s.Length
            && s[position - 1 - i] == s[position + 1 + i])
            {
                i ++;
            }
            var result1 = s.Substring(position - i, (i << 1) + 1);
            
            // 以position为中心左侧的偶数回文字符串
            var j = 0;
            while(position - j >= 0 && position + 1 + j < s.Length
            && s[position - j] == s[position + 1 + j] )
            {
                j ++;
            }
            var result2 = j == 0 ? "" : s.Substring(position - j + 1, (j << 1));

            return result1.Length >= result2.Length ? result1 : result2;
        }

         /* 从一个字符串中s以位置position为中心的最长回文字符串
         只返回最长回文子字符串的起始下标和长度，略微节约一些内存 */
        public static int[] getLongestPalindromicIndice(string s, int position) 
        {
            if(string.IsNullOrEmpty(s))
            {
                throw new ArgumentNullException();
            }
            if(position < 0 || position >= s.Length)
            {
                throw new IndexOutOfRangeException();
            }
            
            // 以position为正中心的奇数回文字符串
            var i = 0;
            while( position - 1 - i >= 0 && position + 1 + i < s.Length
            && s[position - 1 - i] == s[position + 1 + i])
            {
                i ++;
            }
            
            // 以position为中心左侧的偶数回文字符串
            var j = 0;
            while(position - j >= 0 && position + 1 + j < s.Length
            && s[position - j] == s[position + 1 + j] )
            {
                j ++;
            }

            return (i << 1) + 1 >= j << 1 
            ? new int[] {position - i, (i << 1) + 1}
            : new int[] {position - j + 1, j << 1};
        }

        /* 递归判断字符串是否为回文 */
        public static bool isPalindromicRecursion(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return false;
            }

            if (s.Length == 1)
            {
                return true;
            }

            if (s.Length == 2 && s[0] == s[1])
            {
                return true;
            }

            if (s.Length == 2 && s[0] != s[1])
            {
                return false;
            }

            if (s.Length >= 2 && s[0] != s[s.Length - 1])
            {
                return false;
            }

            return isPalindromicRecursion(s.Substring(1, s.Length - 2));
        }

        public static bool isPalindromic(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return false;
            }

            if (s.Length == 1)
            {
                return true;
            }

            for (var i = 0; i < s.Length / 2; i++)
            {
                if (s[i] != s[s.Length - 1 - i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}