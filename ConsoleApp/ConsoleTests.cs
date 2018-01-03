using System;
using System.IO;
using jackel.Percolation;
using jackel.SortTests;
using jackel.UnionFindTests;

namespace AlgsConsoleTests
{
    class ConsoleTests
    {
        static void Main(string[] args)
        {
            // Following line will need adjustment in production to source data
            string dataPath = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString()).ToString()).ToString()).ToString() + @"\SampleData";
            Console.WriteLine($"Data path is: {dataPath}");
            Console.Write("[P]erc, PercS[t]ats, [S]orting, [U]nionFind: ");
            char c = char.ToUpper(Console.ReadKey().KeyChar);
            Console.Write(System.Environment.NewLine);
            switch (c)
            {
                case 'P':
                    Percolation.PercolationTestMain(new string[] { $"{dataPath}\\Perc\\input50.txt" });
                    break;
                case 'T':
                    PercolationStats.PercStatsTestMain(new string[] { "2", "100000" });
                    break;
                case 'S':
                    SortConsole.SortTestMain(new string[] { $"{dataPath}\\Sort\\words.txt" });
                    break;
                case 'U':
                    UFConsole.UnionFindTestsMain(new string[] { $"{dataPath}\\UF\\largeUF.txt" });
                    break;
                case 'Q':
                    return;
            }
        }
    }
}
