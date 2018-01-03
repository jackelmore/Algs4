using System;
using System.Diagnostics;
using jackel.Stdlib;
using jackel.UnionFind;

namespace jackel.UnionFindTests
{
    public class UFConsole
    {
        public enum AlgType { QuickFind, QuickUnion, WeightedQuickUnion, QuickUnionPathComp, WeightedQuickUnionPathComp };
        long RunFind(string filePath, AlgType aType)
        {
            string[] strArr;
            Stopwatch sw = new Stopwatch();
            QuickFind uf = null;

            In Stdin = new In(filePath);
            int N = Int32.Parse(Stdin.ReadLine());
            Debug.WriteLine($"N == {N}");
            switch (aType)
            {
                case AlgType.QuickFind:
                    uf = new QuickFind(N);
                    break;
                case AlgType.QuickUnion:
                    uf = new QuickUnion(N);
                    break;
                case AlgType.QuickUnionPathComp:
                    uf = new QuickUnionPathComp(N);
                    break;
                case AlgType.WeightedQuickUnion:
                    uf = new WeightedQuickUnion(N);
                    break;
                case AlgType.WeightedQuickUnionPathComp:
                    uf = new WeightedQuickUnionPathComp(N);
                    break;
            }
            Console.WriteLine($"{uf.GetType().ToString()}: Starting...");
            sw.Start();
            do
            {
                if (!Stdin.IsEmpty())
                {
                    strArr = Stdin.ReadLine().Split(' ');

                    int p = int.Parse(strArr[0]);
                    int q = int.Parse(strArr[1]);
                    if (uf.connected(p, q))
                        continue;
                    uf.union(p, q);
                }
                else
                {
                    break;
                }
            } while (true);
            sw.Stop();
            Stdin.Close();
            Console.WriteLine($"{uf.GetType().ToString()}: {uf.count()} components, {sw.ElapsedMilliseconds}ms");
            return sw.ElapsedMilliseconds;
        }
        public static void UnionFindTestsMain(string[] args)
        {
            AlgType type = AlgType.WeightedQuickUnionPathComp;
            string fileName;
            long millis = 0L;

            if (args.Length == 0)
            {
                Console.Write("File name: ");
                fileName = Console.ReadLine();
            }
            else
            {
                fileName = args[0];
            }

            do
            {
                Console.Write("Choose find: Quick[F]ind, Quick[U]nion, QuickUnionP[A]thComp, [W]eightedQU, WeightedQU[P]athComp, [Q]uit: ");
                char c = char.ToUpper(Console.ReadKey().KeyChar);
                Console.Write("\n");
                switch (c)
                {
                    case 'F':
                        type = AlgType.QuickFind;
                        break;
                    case 'U':
                        type = AlgType.QuickUnion;
                        break;
                    case 'A':
                        type = AlgType.QuickUnionPathComp;
                        break;
                    case 'W':
                        type = AlgType.WeightedQuickUnion;
                        break;
                    case 'P':
                        type = AlgType.WeightedQuickUnionPathComp;
                        break;
                    case 'Q':
                        return;
                    default:
                        break;
                }
                millis = new UFConsole().RunFind(fileName, type);
                Console.WriteLine("Total runtime: {0}ms", millis);
            } while (true);
        }
    }
}

