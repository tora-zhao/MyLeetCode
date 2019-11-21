using System;
using System.Collections.Generic;


/*
https://leetcode.com/problems/integer-to-roman/
Roman numerals are represented by seven different symbols: I, V, X, L, C, D and M.

Symbol       Value
I             1
V             5
X             10
L             50
C             100
D             500
M             1000
For example, two is written as II in Roman numeral, just two one's added together. 
Twelve is written as, XII, which is simply X + II. 
The number twenty seven is written as XXVII, which is XX + V + II.

Roman numerals are usually written largest to smallest from left to right. 
However, the numeral for four is not IIII. Instead, the number four is written as IV. 
Because the one is before the five we subtract it making four. 
The same principle applies to the number nine, which is written as IX. There are six instances where subtraction is used:

I can be placed before V (5) and X (10) to make 4 and 9. 
X can be placed before L (50) and C (100) to make 40 and 90. 
C can be placed before D (500) and M (1000) to make 400 and 900.
Given an integer, convert it to a roman numeral. Input is guaranteed to be within the range from 1 to 3999.

Example 1:
Input: 3
Output: "III"

Example 2:
Input: 4
Output: "IV"

Example 3:
Input: 9
Output: "IX"

Example 4:
Input: 58
Output: "LVIII"
Explanation: L = 50, V = 5, III = 3.

Example 5:
Input: 1994
Output: "MCMXCIV"
Explanation: M = 1000, CM = 900, XC = 90 and IV = 4. */
namespace _0012
{
    class Solution
    {
        static void Main(string[] args)
        {
            Console.WriteLine(IntToRoman2(1993));
        }

        /* 循环除以10，然后分类讨论 */
        public static string IntToRoman(int num)
        {
            if (num <=0 || num > 3999)
            {
                throw new ArgumentOutOfRangeException();
            }

            var divisor = 1000;
            var result = string.Empty;
            var dic = new Dictionary<int, char>(){
                {1000, 'M'},
                {500, 'D'},
                {100, 'C'},
                {50, 'L'},
                {10, 'X'},
                {5, 'V'},
                {1, 'I'},
            };

            while (divisor > 0)
            {
                var q = num / divisor; // quotient
                var r = num % divisor; // reminder 

                if (q < 4 && q > 0)
                {
                    for (var i = 0; i < q; i++)
                    {
                        result += dic[divisor];
                    }
                }
                else if (q == 4)
                {
                    result += string.Concat(dic[divisor], dic[divisor * 5]);
                }
                else if (q < 9 && q >= 5)
                {
                    result += dic[divisor * 5];
                    for (var i = 0; i < q - 5; i++)
                    {
                        result += dic[divisor];
                    }
                }
                else if (q == 9)
                {
                    result += string.Concat(dic[divisor], dic[divisor * 10]);
                }

                num = r;
                divisor /= 10;
            }

            return result;
        }


        /* 方法1的模组化 */
        public static string IntToRoman2(int num)
        {
            if (num <=0 || num > 3999)
            {
                throw new ArgumentOutOfRangeException();
            }

            int q; // quotient
            string result = string.Empty;

            q = num / 1000;
            result += UnitsToRoman(q, '\0', '\0', 'M');
            num %= 1000; 

            q = num / 100;
            result += UnitsToRoman(q, 'M', 'D', 'C');
            num %= 100; 

            q = num / 10;
            num %= 10; 
            result += UnitsToRoman(q, 'C', 'L', 'X');

            q = num / 1;
            result += UnitsToRoman(q, 'X', 'V', 'I');
            num %= 1; 

            return result;
        }

        private static string UnitsToRoman(int q, char ten, char five, char one)
        {
            string result = string.Empty;
            if (q < 4 && q > 0)
            {
                for (var i = 0; i < q; i++)
                {
                    result += one;
                }
            }
            else if (q == 4)
            {
                result += string.Concat(one, five);
            }
            else if (q < 9 && q >= 5)
            {
                result += five;
                for (var i = 0; i < q - 5; i++)
                {
                    result += one;
                }
            }
            else if (q == 9)
            {
                result += string.Concat(one, ten);
            }
            return result;
        }
    }
}
