using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/*
https://leetcode.com/problems/string-to-integer-atoi/
Implement atoi which converts a string to an integer.
The function first discards as many whitespace characters as necessary until the first
non-whitespace character is found. Then, starting from this character, takes an optional 
initial plus or minus sign followed by as many numerical digits as possible, and interprets
them as a numerical value.

The string can contain additional characters after those that form the integral number, 
which are ignored and have no effect on the behavior of this function.

If the first sequence of non-whitespace characters in str is not a valid integral number, 
or if no such sequence exists because either str is empty or it contains only whitespace
characters, no conversion is performed.
If no valid conversion could be performed, a zero value is returned.

Note:
Only the space character ' ' is considered as whitespace character.
Assume we are dealing with an environment which could only store integers within the 32-bit
signed integer range: [−2^31,  2^31 − 1]. If the numerical value is out of the range of
representable values, INT_MAX (2^31 − 1) or INT_MIN (−2^31) is returned.

Example 1:
Input: "42"
Output: 42

Example 2:
Input: "   -42"
Output: -42
Explanation: The first non-whitespace character is '-', which is the minus sign.
             Then take as many numerical digits as possible, which gets 42.

Example 3:
Input: "4193 with words"
Output: 4193
Explanation: Conversion stops at digit '3' as the next character is not a numerical digit.

Example 4:
Input: "words and 987"
Output: 0
Explanation: The first non-whitespace character is 'w', which is not a numerical 
             digit or a +/- sign. Therefore no valid conversion could be performed.

Example 5:
Input: "-91283472332"
Output: -2147483648
Explanation: The number "-91283472332" is out of the range of a 32-bit signed integer.
             Thefore INT_MIN (−2^31) is returned. 
*/
namespace _0008
{
    class Solution
    {
        static void Main(string[] args)
        {
            Console.WriteLine(MyAtoi3("-91283472332"));
        }

        /* 正则表达式法 & int.Parse*/
        public static int MyAtoi(string str)
        {
            var pattern = @"^\ *(?<sign>[\+\-]?)(?<value>\d+)\D*";
            var rgx = new Regex(pattern);
            var match = rgx.Match(str);

            if (!match.Success)
            {
                return 0;
            }

            var sign = match.Groups["sign"].Value;
            sign = string.IsNullOrEmpty(sign) ? "+" : sign;
            var value = match.Groups["value"].Value;
            try
            {
                return int.Parse(sign + value);
            }
            catch (OverflowException)
            {
                switch (sign)
                {
                    case "+":
                        return int.MaxValue;
                    case "-":
                        return int.MinValue;
                    default:
                        return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /* 正则表达式 + 自写parse方法  
        速度和内存使用和MyAtoi1差不多- -||| */
        public static int MyAtoi2(string str)
        {
            var pattern = @"^\ *(?<sign>[\+\-]?)(?<value>\d+)\D*";
            var rgx = new Regex(pattern);
            var match = rgx.Match(str);

            if (!match.Success)
            {
                return 0;
            }

            var sign = match.Groups["sign"].Value;
            sign = string.IsNullOrEmpty(sign) ? "+" : sign;
            var value = match.Groups["value"].Value;

            long result = 0;
            foreach (var c in value)
            {
                result = result * 10 + (long)char.GetNumericValue(c);
                if (sign == "+" && result >= int.MaxValue)
                {
                    return int.MaxValue;
                }
                else if (sign == "-" && -result <= int.MinValue)
                {
                    return int.MinValue;
                }
            }
            return sign == "+" ? (int)result : (int)-result;
        }

        /*  不用正则表达式，按字符读取字符串，判断匹配与否的同时计算整数结果 */
        public static int MyAtoi3(string str)
        {
            // 符号位
            char sign = '+';
            // 是否已经开始匹配数字
            bool started = false;
            //结果
            long result = 0;

            foreach (var c in str)
            {
                // char.GetNumericValue（c)返回-1时说明该字符c不是一个数字字符
                var value = char.GetNumericValue(c);
                if (!started)
                {
                    if (c == '+' || c == '-')
                    {
                        sign = c;
                        started = true;
                        continue;
                    }
                    if (c == ' ')
                    {
                        continue;
                    }
                    if (value == -1)
                    {
                        break;
                    }
                    else
                    {
                        result = result * 10 + getValue(sign, value);
                        started = true;
                        continue;
                    }
                }
                else
                {
                    if (value == -1)
                    {
                        break;
                    }
                    else
                    {
                        result = result * 10 + getValue(sign, value);
                        if (result >= int.MaxValue)
                        {
                            return int.MaxValue;
                        }
                        else if (result <= int.MinValue)
                        {
                            return int.MinValue;
                        }
                        continue;
                    }

                }
            }
            return (int)result;
        }

        static long getValue(char sign, double value)
        {
            return sign == '+' ? (long)value : -(long)value;
        }
    }
}
