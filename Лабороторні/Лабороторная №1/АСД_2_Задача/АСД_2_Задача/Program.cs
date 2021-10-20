using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;
using static System.Console;

namespace АСД_2_Задача
{
    class Program
    {

        public static bool isPrime(double x)
        {
            double sqrtX = Math.Sqrt(x);
            for (int i = 2; i <= sqrtX; i++)
                if (x % i == 0) 
                    return false;
            return true;
        }
        static void Main(string[] args)
        {


            try
            {
                int n = Convert.ToInt32(ReadLine());
                int pow = 1;

                for (int i = 2; (i - 1) <= n; i *= 2, pow++)
                {
                    bool isPrimeX = false;
                    isPrimeX = isPrime(i - 1);

                    if (isPrimeX)
                    {
                        isPrimeX = false;
                        isPrimeX = isPrime(pow);

                        if (isPrimeX)
                        {
                            WriteLine(i - 1);
                        }
                    }
                }
            }
            catch
            {
                WriteLine("Not a number or n < 1");
            }

            Console.ReadKey();
        }
    }
}
