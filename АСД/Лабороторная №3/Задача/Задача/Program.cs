using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Задача
{
    class Program
    {

       // Предупреждаю что из-за способа создания массива с уникальными значениями размер массива ограничен сверзу числом 999
       // Надеюсь это не критично

        static Random rnd = new Random();
        static void Main(string[] args)
        {
            Write("N = "); int N = int.Parse(ReadLine());
            WriteLine();

            int[] Massive = new int[N];

            for (int i = 0; i < Massive.Length; i++)
            {
                int random = rnd.Next(1, 1000);

                while(Massive.Contains(random))
                    random = rnd.Next(1, 1000);

                Massive[i] = random;
            }

            int first = Massive[0];

            for (int i = 0; i < Massive.Length; i++)
            {
                if (Massive[i] < first)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Write(Massive[i] + " ");
                }
                if (Massive[i] == first)
                {
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    Write(Massive[i] + " ");
                }
                if (Massive[i] > first)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Write(Massive[i] + " ");
                }
            }

            WriteLine();

            int AmountOfElementsBiggerFirst = 0;

            for (int i = 0; i < Massive.Length; i++)
                if (Massive[i] > first)
                    AmountOfElementsBiggerFirst++;

            int[] PathUp = new int[AmountOfElementsBiggerFirst];

            int indexOfBigger = 0;

            for (int i = 0; i < Massive.Length; i++)
            {
                if (Massive[i] > first)
                {
                    PathUp[indexOfBigger] = i;
                    indexOfBigger++;
                }
            }


            bool sorted = false;
            while (!sorted)
            {
                sorted = true;
                for(int i = 1; i<= PathUp.Length - 2; i = i + 2)
                {
                    if(Massive[PathUp[i]] > Massive[PathUp[i + 1]])
                    {
                        int temp = Massive[PathUp[i]];
                        Massive[PathUp[i]] = Massive[PathUp[i + 1]];
                        Massive[PathUp[i + 1]] = temp;

                        sorted = false;
                    }
                }
                for (int i = 0; i <= PathUp.Length - 2; i = i + 2)
                {
                    if (Massive[PathUp[i]] > Massive[PathUp[i + 1]])
                    {
                        int temp = Massive[PathUp[i]];
                        Massive[PathUp[i]] = Massive[PathUp[i + 1]];
                        Massive[PathUp[i + 1]] = temp;

                        sorted = false;
                    }
                }
            }

            int AmountOfElementsLessFirst = 0;

            for (int i = 0; i < Massive.Length; i++)
                if (Massive[i] < first)
                    AmountOfElementsLessFirst++;

            int[] PathDown = new int[AmountOfElementsLessFirst];
            int indexOfLess = 0;
            for (int i = 0; i < Massive.Length; i++)
            {
                if (Massive[i] < first)
                {
                    PathDown[indexOfLess] = i;
                    indexOfLess++;
                }
            }
            sorted = false;
            while (!sorted)
            {
                sorted = true;
                for (int i = 1; i <= PathDown.Length - 2; i = i + 2)
                {
                    if (Massive[PathDown[i]] < Massive[PathDown[i + 1]])
                    {
                        int temp = Massive[PathDown[i]];
                        Massive[PathDown[i]] = Massive[PathDown[i + 1]];
                        Massive[PathDown[i + 1]] = temp;

                        sorted = false;
                    }
                }
                for (int i = 0; i <= PathDown.Length - 2; i = i + 2)
                {
                    if (Massive[PathDown[i]] < Massive[PathDown[i + 1]])
                    {
                        int temp = Massive[PathDown[i]];
                        Massive[PathDown[i]] = Massive[PathDown[i + 1]];
                        Massive[PathDown[i + 1]] = temp;

                        sorted = false;
                    }
                }
            }



            int[] Bigger = Array.FindAll<int>(Massive, x => x > first);
            int[] Less = Array.FindAll<int>(Massive, x => x < first);

            int[] output = new int[N];

            for (int i = 0; i < N; i++)
            {
                if (i < Less.Length)
                {
                    output[i] = Less[i];
                }
                else
                {
                    if (i == Less.Length)
                        output[i] = first;
                    else
                        output[i] = Bigger[i - Less.Length - 1];
                }
            }

            WriteLine();

            for (int i = 0; i < output.Length; i++)
            {
                if(i < Less.Length)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Write(output[i] + " ");
                }
                if(i == Less.Length)
                {
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    Write(output[i] + " ");
                }
                if(i > Less.Length)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Write(output[i] + " ");
                }
            }

            Console.BackgroundColor = ConsoleColor.Black;

            ReadKey();
        }
    }
}
