using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab05
{
    class Program
    {
        static Random rnd = new Random();

        

        public static void InsertionSort(int[] Massive, int low, int high, bool increase)
        {
            if (increase)
            {
                for (int i = low; i < Massive.Length; i++)
                {
                    for (int j = i; j > 0 && j<= high && Massive[j - 1] > Massive[j];j--)
                    {
                        Swap(ref Massive[j - 1], ref Massive[j]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < Massive.Length; i++)
                {
                    for (int j = i; j > 0 && j <= high && Massive[j - 1] < Massive[j];j--)
                    {
                        Swap(ref Massive[j - 1], ref Massive[j]);
                    }
                }
            }
            
        }
        public static void QuickSort(int[] Massive, int low, int high,int CONST, bool increase)
        {
            if (high - low > CONST)
            {
                InsertionSort(Massive, low, high, increase);
            }
            else
            {
                int pivot = Massive[(low + high) / 2];
                int i = low;
                int j = high;

                if (increase)
                {
                    while (i <= j)
                    {
                        while (Massive[i] < pivot)
                            i++;
                        while (Massive[j] > pivot)
                            j--;

                        if (i <= j)
                            Swap(ref Massive[i++], ref Massive[j--]);
                    }
                }
                else
                {
                    while (i <= j)
                    {
                        while (Massive[i] > pivot)
                            i++;
                        while (Massive[j] < pivot)
                            j--;

                        if (i <= j)
                            Swap(ref Massive[i++], ref Massive[j--]);
                    }
                }

                if (low < j)
                    QuickSort(Massive, low, j, CONST, increase);
                if (high > i)
                    QuickSort(Massive, i, high, CONST, increase);
            }
        }

        public static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }
             
        public static void PrintMatrix (int[,] Matrix)
        {
            WriteLine();
            for(int i = 0; i < Matrix.GetLength(0); i++)
            {
                for(int j = 0; j<Matrix.GetLength(1); j++)
                {
                    if (i % 2 == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Write(Matrix[i, j] + "\t");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else 
                    {
                        if (j % 2 == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Write(Matrix[i, j] + "\t");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Write(Matrix[i, j] + "\t");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                    
                }
                WriteLine();
            }
        }

        public static int[] GetNumbersToIncrease(int[,] Matrix)
        {
            int count = 0;
            for (int i = 1; i < Matrix.GetLength(0); i += 2)
            {
                for (int j = 0; j < Matrix.GetLength(1); j += 2)
                {
                    count++;
                }
            }

            int[] Result = new int[count];

            int currentIndex = 0;
            for (int i = 1; i < Matrix.GetLength(0); i += 2)
            {
                for (int j = 0; j < Matrix.GetLength(1); j += 2)
                {
                    Result[currentIndex++] = Matrix[i, j];
                }
            }

            return Result;
        }

        public static int[] GetNumbersToDecrease (int[,] Matrix)
        {
            int count = 0;
            for (int i = 1; i < Matrix.GetLength(0); i += 2)
            {
                for (int j = 0; j < Matrix.GetLength(1); j += 2)
                {
                    count++;
                }
            }

            int[] Result = new int[Matrix.GetLength(0)*Matrix.GetLength(1) - count];

            int currentIndex = 0;
            for (int i = 0; i < Matrix.GetLength(0); i += 1)
            {
                for (int j = i % 2 == 0 ? 0 : 1; j < Matrix.GetLength(1); j += i % 2 == 0 ? 1 : 2)
                {
                    Result[currentIndex++] = Matrix[i, j];
                }
            }

            return Result;
        }
        static void Main(string[] args)
        {
            int colls, rows;

        int[,] Example = new int[,]
        {
            {30, 5 , 79, 56, 16},
            {94, 41, 33, 12, 13},
            {4 , 78, 92, 64, 97},
            {70, 76, 19, 90, 58},
            {32, 7 , 45, 46, 72}
        };

            WriteLine("Контрольный приклад: ");
            PrintMatrix(Example);
            WriteLine();

            int[] _ValuesToDecrease = GetNumbersToDecrease(Example);
            int[] _ValuesToIncrease = GetNumbersToIncrease(Example);

            QuickSort(_ValuesToDecrease, 0, _ValuesToDecrease.Length - 1, Example.GetLength(0), false);
            QuickSort(_ValuesToIncrease, 0, _ValuesToIncrease.Length - 1, Example.GetLength(0), true);

            CompaundMatrix(Example, _ValuesToDecrease, _ValuesToIncrease);

            PrintMatrix(Example);
            WriteLine();



            Write("Number of rows: "); rows = Convert.ToInt32(ReadLine());
            Write("Number of colls: "); colls = Convert.ToInt32(ReadLine()); 


            int[,] Matrix = new int[rows, colls];

            int k = 1;
            for(int i = 0; i<rows; i++)
            {
                for(int j = 0; j < colls; j++)
                {
                    Matrix[i, j] = k++;
                }
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < colls; j++)
                {
                    int randRow = rnd.Next(0, rows);
                    int randColl = rnd.Next(0, colls);
                    Swap(ref Matrix[i, j], ref Matrix[randRow, randColl]);
                }
            }

            WriteLine();
            PrintMatrix(Matrix);
            WriteLine();

            int[] NumbersToIncrease = GetNumbersToIncrease(Matrix);
            QuickSort(NumbersToIncrease, 0, NumbersToIncrease.Length-1, rows, true);


            int[] NumbersToDecrease = GetNumbersToDecrease(Matrix);
            QuickSort(NumbersToDecrease, 0, NumbersToDecrease.Length-1, rows, false);

            CompaundMatrix(Matrix, NumbersToDecrease, NumbersToIncrease);

            PrintMatrix(Matrix);

            Console.ReadKey();

        }

        public static void CompaundMatrix(int[,] Matrix, int[] ValuesToDecrease, int[] ValuesToIncrease)
        {
            int currentIndex = 0;
            for (int i = 1; i < Matrix.GetLength(0); i += 2)
            {
                for (int j = 0; j < Matrix.GetLength(1); j += 2)
                {
                    Matrix[i, j] = ValuesToIncrease[currentIndex++];
                }
            }
            currentIndex = 0;

            
            currentIndex = 0;
            for (int i = 0; i < Matrix.GetLength(0); i += 1)
            {
                for (int j = i % 2 == 0 ? 0 : 1; j < Matrix.GetLength(1); j += i % 2 == 0 ? 1 : 2)
                {
                    Matrix[i, j] = ValuesToDecrease[currentIndex++];
                }
            }
        }
    }
}