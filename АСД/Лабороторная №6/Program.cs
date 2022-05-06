using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лабороторная__6
{
    internal class Program
    {
        public class SLStack
        {
            private class SLNode
            {
                public string Tag;
                public SLNode Prev = null;  
                
                public SLNode(string _Tag, SLNode _Prev = null)
                {
                    this.Tag = _Tag;
                    this.Prev = _Prev;
                }
            }

            SLNode Top = null;
            public int Count = 0;
            private bool IsCorrectHTML = false;
            private void AddCount() { Count++; }
            private void RemoveCount() { Count--; }

            public void Push(string Tag)
            {
                if (this.Top != null)
                {
                    if (IsTagEqualsPrev(Tag))
                        this.Pop();
                    else
                    {
                        Top = new SLNode(Tag, Top);
                        AddCount();
                    }
                }
                else
                {
                    this.Top = new SLNode(Tag);
                    AddCount();
                }
            }

            public void Pop()
            {
                this.Top = this.Top.Prev;
                RemoveCount();
            }

            private bool IsTagEqualsPrev(string _Tag)
            {
                if (_Tag.Contains('/'))
                {
                    string requiredTag = _Tag;
                    requiredTag = requiredTag.Remove(1, 1);
                    if (requiredTag == Top.Tag)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }

            private void PrintError()
            {
                WriteLine("Incorrect input, try again");
            }

/*            public void Clear()
            {
                while (this.Top != null)
                    this.Pop();
            }*/

            public SLStack(string HTML_text)
            {
                bool isOpen = false;
                int openCount = 0;
                int closeCount = 0;
               
                if(HTML_text[0] != '<' || HTML_text[HTML_text.Length-1] != '>')
                {
                    this.PrintError();
                    return;
                }

                for(int i = 0; i < HTML_text.Length; i++)
                {
                    if(HTML_text[i] == '<') 
                    {
                        isOpen = true;
                        openCount++;
                        continue;
                    }
                       

                    if (HTML_text[i] == '>')
                    {
                        isOpen = false;
                        closeCount++;
                        continue;
                    }

                    if (isOpen == true && HTML_text[i] == '<')
                    {
                        this.PrintError();  
                        return;
                    }

                    if (isOpen == false && HTML_text[i] == '>')
                    {
                        this.PrintError();
                        return;
                    }

                    if (isOpen == false && (HTML_text[i] != '>' || HTML_text[i] != '<'))
                    {
                        HTML_text = HTML_text.Remove(i--,1);
                    }       
                }

                if (openCount != closeCount || isOpen == true)
                {
                    this.PrintError();
                    return;
                }

                for(int i = 0; i < HTML_text.Length-1; i++)
                {
                    if (HTML_text[i] == '>')
                        HTML_text= HTML_text.Insert(++i, " ");
                }
                string[] HTML_tags = HTML_text.Split(' ');
                for(int i = 0; i < HTML_tags.Length; i++)
                    this.Push(HTML_tags[i]);

                if (this.Count == 0)
                    this.IsCorrectHTML = true;
                else
                {
                    this.PrintError();
                    return;
                }
            }

            public bool GetIsCorrectHTML()
            {
                return IsCorrectHTML;
            }

        }

        const string HTML_text = "<html><head><title>My title</title></head><body><p>Text for control example</p></body></html>";

        static void Main(string[] args)
        {
            WriteLine("Контрольний приклад: " + HTML_text);

            SLStack CE = new SLStack(HTML_text);
            if (CE.GetIsCorrectHTML())
            {
                Console.WriteLine("Correct");
            }

            bool isCorrect = false;
            while (!isCorrect)
            {
                Write("HTML text = "); string text = ReadLine();

                SLStack stack = new SLStack(text);
                if (stack.GetIsCorrectHTML())
                {
                    Console.WriteLine("Correct");
                }

            }
            ReadKey();
        }

    }

}

        
    

