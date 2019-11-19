using System;
using System.Collections.Generic;

/*
https://leetcode.com/problems/regular-expression-matching/
Given an input string (s) and a pattern (p), implement regular expression matching with support for '.' and '*'.
'.' Matches any single character.
'*' Matches zero or more of the preceding element.
The matching should cover the entire input string (not partial).

Note:
s could be empty and contains only lowercase letters a-z.
p could be empty and contains only lowercase letters a-z, and characters like . or *.

Example 1:
Input:
s = "aa"
p = "a"
Output: false
Explanation: "a" does not match the entire string "aa".

Example 2:
Input:
s = "aa"
p = "a*"
Output: true
Explanation: '*' means zero or more of the preceding element, 'a'. Therefore, by repeating 'a' once, it becomes "aa".

Example 3:
Input:
s = "ab"
p = ".*"
Output: true
Explanation: ".*" means "zero or more (*) of any character (.)".

Example 4:
Input:
s = "aab"
p = "c*a*b"
Output: true
Explanation: c can be repeated 0 times, a can be repeated 1 time. Therefore, it matches "aab".

Example 5:
Input:
s = "mississippi"
p = "mis*is*p*."
Output: false  */
/* NOT SOLVED */
namespace _0010
{
    class Solution
    {
        static void Main(string[] args)
        {
            Console.WriteLine(IsMatch("aaa", "a*a")); 
            Console.WriteLine(IsMatch("aa", "a"));
            Console.WriteLine(IsMatch("aa", "a*"));
            Console.WriteLine(IsMatch("ab", ".*"));
            Console.WriteLine(IsMatch("aab", "c*a*b"));
            Console.WriteLine(IsMatch("aab", "c*a*ba*b*"));
            Console.WriteLine(IsMatch("mississippi", "mis*is*p*."));
            Console.WriteLine("aaa"[2..]);
        }


        /* 参考答案1: 递归 */
        public static bool IsMatch(string s, string p)
        {
            if(string.IsNullOrEmpty(p)) 
            {
                return string.IsNullOrEmpty(s); 
            }

            bool firstMatch = (
                !string.IsNullOrEmpty(s) 
                && (s[0] == p[0] || p[0] == '.'));

            if(p.Length >= 2 && p[1] == '*')
            {
                return (IsMatch(s, p[2..]) || (firstMatch && IsMatch(s[1..], p)));
            }
            else
            {
                return firstMatch && IsMatch(s[1..], p[1..]);
            }
        }


        // /* 简化的无限自动状态机 */
        // public class Status
        // {
        //     public char Symbol { get; }
        //     public bool IsStar { get; private set; }
        //     public List<Status>  nextStatus { get; private set;}

        //     public Status(char symbol, bool isStar = false)
        //     {
        //         this.Symbol = symbol;
        //         this.IsStar = isStar;
        //     }


        //     /* pattern解析器，将pattern抽象为一系列状态
        //     例如： mis*is*p*.  将会解析为7个状态， m i s* i s* p* . */
        //     public static List<Status> Parse(string p)
        //     {
        //         var sList = new List<Status>();
        //         for (int i = 0; i < p.Length; i++)
        //         {
        //             if (p[i] == '*')
        //             {
        //                 // 处理连续出现*的情况
        //                 if (i == 0 || p[i - 1] == '*')
        //                 {
        //                     throw new ArgumentException("Illegal pattern");
        //                 }
        //                 else
        //                 {
        //                     sList[sList.Count - 1].IsStar = true;
        //                 }
        //             }
        //             else
        //             {
        //                 var s = new Status(p[i]);
        //                 sList.Add(s);
        //             }
        //         }
        //         return sList;
        //     }
        // }
    }
}


/*
            Console.WriteLine(IsMatch("aaa", "a* a")); 
            Console.WriteLine(IsMatch("aa", "a"));
            Console.WriteLine(IsMatch("aa", "a*"));
            Console.WriteLine(IsMatch("ab", ".*"));
            Console.WriteLine(IsMatch("aab", "c*a* b"));
            Console.WriteLine(IsMatch("aab", "c*a* b a*b*"));
            Console.WriteLine(IsMatch("mississippi", "1:m 2:i 3:s* 4:i 5:s* 6:p* 7:."));

            
            {1}  {2} {3, 4}  {4, 5} -> {5, 6, 7} -> {accept}
 */