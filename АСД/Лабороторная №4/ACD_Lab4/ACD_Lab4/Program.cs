using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace ACD_Lab4
{
    class Program
    {
        public class MyList
        {
            class SLNode
            {
                public SLNode next;
                public int data;

                public SLNode(int data, SLNode next)
                {
                    this.next = next;
                    this.data = data;
                }
                
                public SLNode(int data)
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

            public void AddFirst (int data)
            {
                SLNode newNode = new SLNode(data);
               if(head == null)
                {
                    head = newNode;
                }
                else
                {
                    newNode.next = head;
                    head = newNode;
                }
            }

            public void AddLast(int data)
            {
                SLNode current = new SLNode();
                current = head;

                while (current.next != null)
                    current = current.next;

                SLNode newNode = new SLNode(data);
                current.next = newNode;
            }

            public void AddAtPosition(int data, int pos)
            {
                SLNode current = new SLNode();
                current = head;

                for (int i = 1; i < pos - 1; i++)
                {
                    if (current == null && (i+1) < (pos - 1))
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

            public void AddMyTask(int data)
            {
                SLNode current = new SLNode();
                current = head;

                int pos = 1;
                bool breaked = false;


                while(current != null)
                {
                    if (current.data == data)
                    {
                        breaked = true;
                        break;
                    }
                    else
                    {
                        current = current.next;
                        pos++;
                    }
                }

                if (breaked)
                {
                    this.AddAtPosition(data, pos + 1);
                }
                else
                {
                    this.AddFirst(data);
                }
            }
            

            public void Print()
            {
                SLNode current = new SLNode();
                current = head;
                Write("List: ");
                while (current != null)
                {
                    if (current.next == null)
                    {
                        WriteLine(current.data + " ");
                        current = current.next;
                    }
                    else
                    {
                        Console.Write(current.data + " ");
                        current = current.next;
                    }
                }
                
            }
        }

        static int tryGetCommand()
        {

            int command = 0;

            try
            {
                Write("Command = "); command = Convert.ToInt32(ReadLine());
                return command;
            }
            catch
            {
                WriteLine("Wrong input data");
            }

            return tryGetCommand();
        }

        static int tryGetData()
        {
            int data = 0;
            Write("data = ");
            try
            {
                data = Convert.ToInt32(ReadLine());
                return data;
            }
            catch
            {
                WriteLine("Wrong input data");
            }

            return tryGetData();
        }
        static int tryGetPos()
        {
            int pos = 0;
            Write("pos = ");
            try
            {
                pos = Convert.ToInt32(ReadLine());
                return pos;
            }
            catch
            {
                WriteLine("Wrong input data");
            }

            return tryGetPos(); ;
        }

        static void ExecuteCommand(MyList list ,int command)
        {
            int data;
            int pos;

            switch (command)
            {
                case 1:
                    data = tryGetData();
                    list.AddFirst(data);
                    break;
                case 2:
                    data = tryGetData();
                    list.AddLast(data);
                    break;
                case 3:
                    data = tryGetData();
                    pos = tryGetPos();
                    list.AddAtPosition(data, pos);
                    break;
                case 4:
                    list.DeleteFirst();
                    break;
                case 5:
                    list.DeleteLast();
                    break;
                case 6:
                    pos = tryGetPos();
                    list.DeleteAtPosition(pos);
                    break;
                case 7:
                    data = tryGetData();
                    list.AddMyTask(data);
                    break;
                default:
                    WriteLine("Wrong input data");
                    ExecuteCommand(list, tryGetCommand());
                    break;
            }
        }

        static bool tryGetIsFinished()
        {
            bool isFinished = false;
            Write("Finished? (\"true\" to end, anything ele to continue) = ");
            try
            {
                if (ReadLine() == "true")
                {
                    isFinished = true;
                }
                else
                    isFinished = false;
            }
            catch
            {
                tryGetIsFinished();
            }

            return isFinished;
        }

        static void Main(string[] args)
        {
            MyList list = new MyList();

            bool isFinished = false;

            WriteLine("Choose a comand by entering a number");
            WriteLine("AddFirst(int data) – 1");
            WriteLine("AddLast(int data) – 2");
            WriteLine("AddAtPosition(int data, int pos) – 3");
            WriteLine("DeleteFirst() – 4");
            WriteLine("DeleteLast() – 5");
            WriteLine("DeleteAtPosition(int pos) – 6");
            WriteLine("AddMyTask(int data) (Личное задание) – 7");

            while (isFinished != true)
            {

                ExecuteCommand(list, tryGetCommand());
                list.Print();
                isFinished = tryGetIsFinished();
                
            }
        }
    }
}
