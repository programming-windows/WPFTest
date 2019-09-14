using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace dll_csharp
{
    public class DLLCsharp
    {
        /*
        [DllImport("dll_cpp.dll", CharSet = CharSet.Ansi)]
        public static extern int Add(int a, int b);

        [DllImport("dll_cpp.dll", CharSet = CharSet.Ansi)]
        public static extern long Fibonacci(long num);

        [DllImport("dll_cpp.dll", CharSet = CharSet.Ansi)]
        public static extern long Factorial(long num);
        */

       

        //加法
        public static int AddF(int a, int b)
        {
            return a + b;
        }

        //斐波那契数列F(1)=1，F(2)=1, F(n)=F(n-1)+F(n-2)
        public static long FibonacciF(long num)
        {
            if (num == 1 || num==2)
                return 1;

            return FibonacciF(num - 1) + FibonacciF(num - 2);
        }

        //阶乘
        public static long FactorialF(long num)
        {
            long faresult = 1;
            if (num > 1)
            {
                for (int i = 1; i <= num; i++)
                {
                    faresult = faresult * i;
                }
            }
            return (faresult);
        }
    }
}
