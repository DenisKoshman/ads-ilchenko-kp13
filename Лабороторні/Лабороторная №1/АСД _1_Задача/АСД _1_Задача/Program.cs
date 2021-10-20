using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using static System.Math;

namespace АСД__1_Задача
{
    class Program
    {
        static void Main(string[] args)
        {
            try 
            {
                double x = Convert.ToDouble(ReadLine());
                double y = Convert.ToDouble(ReadLine());

                double a, b;
                a = Pow(Abs(Sin((x) - Cos(y))), 1.0 / 3);
                b = Cos(Sin(a * a));

                WriteLine("a = {0:N3}", a);
                WriteLine("b = {0:N3}", b);
            }
            catch
            {
                WriteLine("Wring input data");
            }


            ReadKey();
        }
    }
}
