using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab07
{
    internal class LinkedListcs<type>
    {
        class SLNode
        {
            public SLNode next;
            public type data;

            public SLNode(type data, SLNode next)
            {
                this.next = next;
                this.data = data;
            }

            public SLNode(type data)
            {
                this.next = null;
                this.data = data;
            }
            public SLNode()
            {
                this.next = null;
            }
        }

        SLNode head = null;

        public void AddFirst(type data)
        {
            SLNode newNode = new SLNode(data);
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                newNode.next = head;
                head = newNode;
            }
        }

        public void AddLast(type data)
        {
            SLNode current = new SLNode();
            current = head;

            while (current.next != null)
                current = current.next;

            SLNode newNode = new SLNode(data);
            current.next = newNode;
        }

        public void AddAtPosition(type data, int pos)
        {
            SLNode current = new SLNode();
            current = head;

            for (int i = 1; i < pos - 1; i++)
            {
                if (current == null && (i + 1) < (pos - 1))
                {
                    this.AddLast(data);
                }
                else
                {
                    current = current.next;
                }
            }

            SLNode newNode = new SLNode(data, current.next);
            current.next = newNode;

        }

        public void DeleteFirst()
        {
            head = head.next;
        }

        public void DeleteLast()
        {
            SLNode current = new SLNode();
            current = head;

            while (current.next.next != null)
                current = current.next;

            current.next = null;
        }

        public void DeleteAtPosition(int pos)
        {
            SLNode current = new SLNode();
            current = head;

            for (int i = 1; i < pos - 1; i++)
                current = current.next;

            current.next = current.next.next;
        }
    }
}
