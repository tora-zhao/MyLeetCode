using System;
using System.Collections.Generic;

/*
https://leetcode.com/problems/add-two-numbers/
You are given two non-empty linked lists representing two non-negative integers.
The digits are stored in reverse order and each of their nodes contain a single digit. 
Add the two numbers and return it as a linked list.
You may assume the two numbers do not contain any leading zero, except the number 0 itself.

Example:
Input: (2 -> 4 -> 3) + (5 -> 6 -> 4)
Output: 7 -> 0 -> 8
Explanation: 342 + 465 = 807.
*/
namespace _0002
{
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x) { val = x; }

		// 追加build方法，方便生成list
        public static ListNode Build(int[] nums)
        {
            if (nums.Length == 0)
            {
                throw new ArgumentException("Empty input!");
            }

            var startNode = new ListNode(nums[0]);
            var node = startNode;
            for (int i = 1; i < nums.Length; i++)
            {
                node.next = new ListNode(nums[i]);
                node = node.next;
            }
            return startNode;
        }

		// 重写一个tostring，方便打印观察结果
        public override string ToString()
        {
            var print = val.ToString();
            var node = next;
            while (node != null)
            {
                print += " -> " + node.val.ToString();
                node = node.next;
            }
            return print;
        }
    }
    public class Solution
    {

        public static void Main(string[] args)
        {
            //var num1 = ListNode.Build(new int[] {2, 4, 3});
            //var num2 = ListNode.Build(new int[] {5, 6, 7, 2});
            var num1 = ListNode.Build(new int[] { 5 });
            var num2 = ListNode.Build(new int[] { 5 });
            //Console.WriteLine(AddTwoNumbers(num1, num2).ToString());
            Console.WriteLine(AddTwoNumbers2(num1, num2).ToString());
        }

		/* 基本思想： 指针追踪处理，把结果归并到其中一个list上从而避免了new一个新list */
        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            var node1 = l1;
            var node2 = l2;
            var hasCarry = false;  // 有进位
            int sum;

			// 从l1，l2的前段开始，处理对位同时有值的加法
            while (l1 != null && l2 != null)
            {
				// 处理进位
                sum = hasCarry ? l1.val + l2.val + 1 : l1.val + l2.val;
                hasCarry = sum > 9;
                l1.val = l2.val = hasCarry ? sum - 10 : sum;

				// l1，l2均无下一位但有进位时需要特殊处理
                if (l1.next == null && l2.next == null && hasCarry)
                {
                    l1.next = new ListNode(1);
                    hasCarry = false;
                }
                l1 = l1.next;
                l2 = l2.next;
            }

			// 如果l1处理完l2仍有剩余，就把l1指针指向l2，继续在l2上操作
			// 否则继续处理原先的l1
            if (l1 == null)
            {
                l1 = l2;
                node1 = node2;
            }

			// 处理单list
            while (l1 != null)
            {
				// 处理进位
                sum = hasCarry ? l1.val + 1 : l1.val;
                hasCarry = sum > 9;
                l1.val = hasCarry ? sum - 10 : sum;

				// 下一位为空但有进位时需要特殊处理
                if (l1.next == null && hasCarry)
                {
                    l1.next = new ListNode(1);
                    hasCarry = false;
                }

                l1 = l1.next;
            }

            return node1;
        }

		/* 递归大法好！ 只站在单个node的角度考虑怎么生成这个结点*/
		public static ListNode AddTwoNumbers2(ListNode l1, ListNode l2) 
		{
			return AddTwoNode(l1, l2, false);
		}

		public static ListNode AddTwoNode(ListNode l1, ListNode l2, bool hasCarry)
		{
			// 退出条件： 均为null且没有进位，已无法继续计算
			if(l1 == null && l2 == null && !hasCarry)
			{
				return null;
			}

			// null的结点时按0值算
			var val1 = l1 == null ? 0 : l1.val;
			var val2 = l2 == null ? 0 : l2.val;
			// 处理进位
			var sum = hasCarry ? val1 + val2 + 1 : val1 + val2;
			hasCarry = sum > 9;
			var resultVal = hasCarry ? sum - 10 : sum;

			// 关键：调用自身生成next，最后再返回自己
			var node =  new ListNode(resultVal);
			var next1 = l1 == null ? null : l1.next;
			var next2 = l2 == null ? null : l2.next;
			node.next = AddTwoNode(next1, next2, hasCarry);

			return node;
		}
    }
}
