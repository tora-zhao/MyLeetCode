using System;

/*
https://leetcode.com/problems/palindrome-number/
Determine whether an integer is a palindrome. 
An integer is a palindrome when it reads the same backward as forward.

Example 1:
Input: 121
Output: true

Example 2:
Input: -121
Output: false
Explanation: From left to right, it reads -121. From right to left, 
it becomes 121-. Therefore it is not a palindrome.

Example 3:
Input: 10
Output: false
Explanation: Reads 01 from right to left. Therefore it is not a palindrome.
 */
namespace _0009
{
    class Solution
    {
        static void Main(string[] args)
        {
            Console.WriteLine(IsPalindrome2(121));
        }

        public static bool IsPalindrome(int x)
        {
            if(x < 0) 
            {
                return false;
            }

            long y = 0;
            long n = x;
            while(n > 0)
            {
                y = y * 10 + n % 10;
                n /= 10;
            }

            return x == y;
        }

        /*
        官方推荐答案，不用将x除尽，只需比较x的后半与前半 
        但注意需要特殊处理但情况较多*/
        public static bool IsPalindrome2(int x)
        {
            if(x == 0)
            {
                return true;
            }   
            if(x < 0 || x % 10 == 0) 
            {
                return false;
            }

            int y = 0;
            while(x > y)
            {
                y = y * 10 + x % 10;
                x /= 10;
            }

            // 注意奇数位回文数判断条件位 x == y/10
            return x == y || x == y / 10;
        }
    }
}
