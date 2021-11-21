using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Задача_1
{

   

    class Program
    {
        static Random rnd = new Random();

        static void Filling(int[,] Massive, int column, int row, bool direction = false, int n = 1)
        {
            if (!(row == Massive.GetLength(0) / 2 && column == Massive.GetLength(1)))
            {
                if (direction == false)
                {
                    Massive[row, column] = rnd.Next(10, 100);
                    try
                    {
                        Filling(Massive, column + 1, row + 1, false, n + 1);
                    }
                    catch
                    { 
                        try
                        {
                            Filling(Massive, column + 1, row, true, n + 1);
                        }
                        catch
                        {

                            Filling(Massive, column, row - 1, true, n + 1);

                        }
                    }
                }
                else
                {
                    Massive[row, column] = rnd.Next(10, 100);
                    try 
                    {
                        if(row == Massive.GetLength(0) / 2)
                        {
                            Filling(Massive, column + 1, row, false, n + 1);
                        }
                        else
                        {
                            Filling(Massive, column - 1, row - 1, true, n + 1);
                        }

                    }
                    catch
                    {
                        try
                        {
                            if (row == Massive.GetLength(0) / 2)
                            {
                                Filling(Massive, column + 1, row, false, n + 1);
                            }
                            else
                            {
                                Filling(Massive, column, row - 1, false, n + 1);
                            }
                        }
                        catch
                        {

                            Filling(Massive, column + 1, row, false, n + 1);

                        }
                    }
                }
            }
            else
            {
                for (int i = row-1; i >= 0; i--)
                {
                    if (i % 2 == 0)
                    {
                        for (int j = Massive.GetLength(1)-1; j >= 0; j--)
                        {
                            Massive[i,j] = rnd.Next(10, 100);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < Massive.GetLength(1); j++)
                        {
                            Massive[i, j] = rnd.Next(10, 100);
                        }
                    }
                }
            }
            
        }

        static void Output(int[,] Massive, int column, int row, string direction = "down", int max = 0)
        {
            if (!(row == Massive.GetLength(0) / 2 && column == Massive.GetLength(1)))
            {
                if (direction == "down")
                {
                    Write($"{Massive[row, column],4} ");
                    if (Massive[row, column] > max)
                        max = Massive[row, column];
                    try
                    {
                        Output(Massive, column + 1, row + 1, "down", max);

                    }
                    catch
                    {
                        try
                        {
                            Output(Massive, column + 1, row, "up", max);
                        }
                        catch
                        {

                            Output(Massive, column, row - 1, "up", max);

                        }
                    }
                }
                else
                {
                    Write($"{Massive[row, column],4} ");
                    if (Massive[row, column] > max)
                        max = Massive[row, column];
                    try
                    {
                        if (row == Massive.GetLength(0) / 2)
                        {
                            Output(Massive, column + 1, row, "down", max);
                        }
                        else
                        {
                            Output(Massive, column - 1, row - 1, "up", max);
                        }

                    }
                    catch
                    {
                        try
                        {
                            if (row == Massive.GetLength(0) / 2)
                            {

                                Output(Massive, column + 1, row, "down", max);
                            }
                            else
                            {
                                Output(Massive, column, row - 1, "down", max);
                            }
                        }
                        catch
                        {

                            Output(Massive, column + 1, row, "down", max);

                        }
                    }
                }
            }
            else
            {
                List<int[]> Indexes = new List<int[]>();

                for (int i = row - 1; i >= 0; i--)
                {
                    if (i % 2 == 1)
                    {
                        for (int j = Massive.GetLength(1) - 1; j >= 0; j--)
                        {
                            if (max < Massive[i, j])
                            {
                                int[] index = new int[] {i,j};
                                Indexes.Add(index);
                            }
                            Write($"{Massive[i, j],4} ");
                        }
                    }
                    else
                    {
                        for (int j = 0; j < Massive.GetLength(1); j++)
                        {
                            if (max < Massive[i, j])
                            {
                                int[] index = new int[] { i, j };
                                Indexes.Add(index);
                            }
                            Write($"{Massive[i, j],4} ");
                        }
                    }
                }
                WriteLine();
                if(Indexes.Count == 0)
                {
                    WriteLine("No elements was found");
                }
                else
                {
                    Write("Indexes: ");
                    for (int i = 0; i< Indexes.Count; i++)
                    {
                        Write($"({Indexes[i][0]}, {Indexes[i][1]}) ");
                    }
                    WriteLine();
                }
                WriteLine("Max = " + max);
            }

        }

        static void Main(string[] args)
        {
            int Rows = rnd.Next(2, 9) * 2;
            int Columns = rnd.Next(4, 11);

            int[,] Massive = new int[Rows,Columns];

            int diagonals = Columns + Rows - 1;

            Filling(Massive, 0, Rows-1, false);

            for (int i = 0; i < Rows; i++)
            {
                for(int j = 0; j < Columns; j++)
                {
                    Write($"{Massive[i, j], 4}  ");
                }
                WriteLine();
            }


            Output(Massive, 0, Rows - 1, "down");



            ReadKey();
        }
    }
}
