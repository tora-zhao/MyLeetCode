using System;
using System.Text;

/*
https://leetcode.com/problems/zigzag-conversion/
The string "PAYPALISHIRING" is written in a zigzag pattern on a given number of rows 
like this: (you may want to display this pattern in a fixed font for better legibility)

P   A   H   N
A P L S I I G
Y   I   R
And then read line by line: "PAHNAPLSIIGYIR"
Write the code that will take a string and make this conversion given a number of rows:
string convert(string s, int numRows);

Example 1:
Input: s = "PAYPALISHIRING", numRows = 3
Output: "PAHNAPLSIIGYIR"

Example 2:
Input: s = "PAYPALISHIRING", numRows = 4
Output: "PINALSIGYAHRPI"
Explanation:
P     I    N
A   L S  I G
Y A   H R
P     I
 */
namespace _0006
{
    class Solution
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Convert2("PAYPALISHIRING", 4));
        }

        /*
        假设目标为一个二位字符数组
        P     I     N
        A   L S   I G
        Y A   H R
        P     I
        则可按照如下分组循环处理，每组有part = numRows + （numRows - 2）个元素
        P           I         N
        A   L       S   I     G
        Y A         H R 
        P           I   
        然后遍历给定字符串，根据numRows决定当中每个字符的二维数组坐标
        */
        public static string Convert(string s, int numRows)
        {
            // 特殊处理
            if(numRows <= 0)
            {
                throw new ArgumentException();
            }
            if(string.IsNullOrEmpty(s))
            {
                return "";
            }
            if(numRows == 1 )
            {
                return s;
            }

            // 分组后每组的字符数
            var part = numRows + (numRows - 2);
            // 计算列数
            var columns = (s.Length / part + 1) * (numRows - 1);
            char[,] zigzag = new char[numRows, columns];
            for (var i = 0; i < s.Length; i++)
            {
                var j = i / part;
                var k = i % part;
                // 每组处在第一竖排中的字符处理
                if (k < numRows)
                {
                    zigzag[k, j * (numRows - 1)] = s[i];
                }
                // 每组处在斜上方向中（竖排之后）的字符处理
                else
                {
                    zigzag[part - k, j * (numRows - 1) + (k - numRows) + 1] = s[i];
                }
            }

            //PrintZigZag(zigzag, numRows, columns);
            // 遍历二维数组，去除空元素并返回字符串
            return BackToString(zigzag, numRows, columns);
        }

        private static string BackToString(char[,] zigzag, int rows, int columns)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (zigzag[i, j] != '\0')
                    {
                       sb.Append(zigzag[i, j]);
                    }
                }
            }
            return sb.ToString();
        }

        private static void PrintZigZag(char[,] zigzag, int rows, int columns)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (zigzag[i, j] == '\0')
                    {
                        Console.Write(' ');
                    }
                    else
                    {
                        Console.Write(zigzag[i, j]);
                    }
                }
                Console.WriteLine();
            }
        }

        /* 
        事实上并不需要二维数组，一维数组即可搞定
        思路： 遍历结果的每一行，计算原字符在新字符串中的下标即可
        和二维数组处理法一样如下分组考虑，每组第一行和最后一行只有一个元素
        而中间各行会有两个元素，需要分开处理
        P           I         N
        A   L       S   I     G
        Y A         H R 
        P           I   
        */
        public static string Convert2(string s, int numRows)
        {
            // 特殊处理
            if(numRows <= 0)
            {
                throw new ArgumentException();
            }
            if(string.IsNullOrEmpty(s))
            {
                return "";
            }
            if(numRows == 1 )
            {
                return s;
            }

            // 分组后每组的字符数
            var part = numRows + (numRows - 2);
            var zigzag = new StringBuilder();

            for(var i = 0; i < numRows; i ++)
            {
                // 每一行利用一个循环处理
                var j = i;
                while(j < s.Length)
                {
                    // 第一行和最后一行需要特殊处理
                    if(i == 0 || i == numRows - 1)
                    {
                        zigzag.Append(s[j]);
                        j += part;
                    }
                    // 中间各行
                    else
                    {
                        // 先处理该行第一个字符，注意j的增量
                        zigzag.Append(s[j]);;
                        j += (numRows - i - 1 ) << 1; 
                        if(j >= s.Length)
                        {
                            break;
                        }
                        // 该行第二个字符,，注意j的增量
                        zigzag.Append(s[j]);
                        j += i << 1;
                    }
                }
            }
            return zigzag.ToString();
        }
    }
}
