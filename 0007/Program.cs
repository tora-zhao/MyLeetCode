using System;

/*
https://leetcode.com/problems/reverse-integer/
Given a 32-bit signed integer, reverse digits of an integer.

Example 1:
Input: 123
Output: 321

Example 2:
Input: -123
Output: -321

Example 3:
Input: 120
Output: 21

Note:
Assume we are dealing with an environment which could only store integers within the 32-bit
signed integer range: [−2^31,  2^31 − 1]. For the purpose of this problem, 
assume that your function returns 0 when the reversed integer overflows. 
*/
namespace _0007
{
    class Solution
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Reverse3(-123));
        }

        /* 转换成字符串，再reverse，再parse成int
        容易想但是效率差*/
        public static int Reverse(int x)
        {
            if(x >= 0)
            {
                var xChars = x.ToString().ToCharArray();
                Array.Reverse(xChars);
                if (int.TryParse(xChars, out x))
                {
                    return x;
                }
            }
            else
            {
                var xChars = (-x).ToString().ToCharArray();
                                Array.Reverse(xChars);
                if (int.TryParse(xChars, out x))
                {
                    return -x;
                }
            }
            return 0;
        }

        /* x除以10，取其余数为新整数的最高位，依次循环变可得到反转数
        注意正负处理和int32边界处理，特别是反转时有可能直接数值溢出 */
        public static int Reverse2(int x)
        {
            if(x >= int.MaxValue || x <= int.MinValue)
            {
                return 0;
            }

            if(x >= 0)
            {
                return ReverseUnsigedInt(x);
            }
            else
            {
                return - ReverseUnsigedInt(-x);
            }
        }

        private static int ReverseUnsigedInt(int x)
        {
            if(x < 0)
            {
                throw new ArgumentException();
            }

            long y = 0;
            while(x > 0)
            {
                y = y * 10 + x % 10;
                if(y >= int.MaxValue)
                {
                    return 0;
                }
                x /= 10;
            }
            return (int) y; 
        }

        /* 相比Reverse2改一下while条件就不需要分正负性处理了！ */
        public static int Reverse3(int x)
        {
            if(x >= int.MaxValue || x <= int.MinValue)
            {
                return 0;
            }

            long y = 0;
            while(x != 0)
            {
                y = y * 10 + x % 10;
                if(y >= int.MaxValue || y <= int.MinValue)
                {
                    return 0;
                }
                x /= 10;
            }
            return (int) y; 
        }
    }
}
