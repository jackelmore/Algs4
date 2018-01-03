using System;
using System.Diagnostics;
using jackel.Stdlib;

namespace TripleSum
{
    class TripleSum
    {
        public static int count(int[] a)
        {
            int N = a.Length;
            int count = 0;
            for (int i = 0; i < N; i++)
                for (int j = i + 1; j < N; j++)
                    for (int k = j + 1; k < N; k++)
                        if (a[i] + a[j] + a[k] == 0)
                        count++;
            return count;

        }
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            int[] a = In.ReadInts(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + args[0]);
            sw.Start();
            Console.WriteLine(count(a));
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds + "ms");
        }
    }
}
