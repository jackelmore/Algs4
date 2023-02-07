using System;
using System.IO;
using System.Runtime.Intrinsics.Arm;
using jackel.Percolation;
using jackel.SortTests;
using jackel.UnionFindTests;

internal class Program
{
    private static void Main(string[] args)
    {
        var dataPath = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString()).ToString()).ToString()).ToString() + @"\SampleData";
        Console.WriteLine($"Data path is: {dataPath}");

        char inputkey;
        do
        {
            Console.Write("[P]erc, PercS[t]ats, [S]orting, [U]nionFind, [Z]=Quit: ");
            inputkey = char.ToUpper(Console.ReadKey().KeyChar);
            Console.Write(Environment.NewLine);
            switch (inputkey)
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
            }
        } while (inputkey != 'Z');
    }
}