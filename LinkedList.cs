using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class ListNode
    {
        public int Value;
        public ListNode Next;

        public ListNode(int value = 0, ListNode next = null)
        {
            Value = value;
            Next = next;
        }
    }
}
