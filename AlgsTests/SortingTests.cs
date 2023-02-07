using System;
using System.Diagnostics;
using jackel.Sorting.Generic;
using System.Threading.Tasks;
using System.Threading;
using jackel.Stdlib;
using System.Collections.Generic;

namespace jackel.SortTests
{
	public enum SortType
	{
		Selection, Insertion, Shell, Merge, MergeBU, MergeX, Heap, Quick, Quick3
	};
	public struct sortEntry
	{
		public string sortName;
		public SortType sortType;
		public Action<int[]> sortDelegate;
		public sortEntry(string sn, SortType st, Action<int[]> sd)
		{
			sortName = sn;
			sortType = st;
			sortDelegate = sd;
		}
	}
    public class SortConsole
    {
        public static readonly sortEntry[] sortList;

        public static void WriteColor(ConsoleColor cc, string s)
        {
            ConsoleColor existing = Console.ForegroundColor;
            Console.ForegroundColor = cc;
            Console.Write(s);
            Console.ForegroundColor = existing;
        }
        public static void Show(string[] a, int x, int y)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (i == x)
                    WriteColor(ConsoleColor.Green, a[i] + " ");
                else if (i == y)
                    WriteColor(ConsoleColor.Yellow, a[i] + " ");
                else
                    Console.Write(a[i] + " ");
            }
            Console.WriteLine();
        }
        public static void Show(string[] a)
        {
            if (a.Length > 100)
                ShowFirstLast(a, 3);
            else
            {
                for (int i = 0; i < a.Length; i++)
                    Console.Write(a[i] + " ");
                Console.WriteLine();
            }
        }
        public static void ShowFirstLast(string[] a, int num)
        {
            if (a.Length >= num)
            {
                Console.WriteLine($"First {num}:");
                for (int i = 0; i < num; i++)
                    Console.WriteLine("  {0,6}: {1}", i, a[i]);
                Console.WriteLine($"Last {num}:");
                for (int i = a.Length - num; i < a.Length; i++)
                    Console.WriteLine("  {0,6}: {1}", i, a[i]);
            }
        }
        static SortConsole()
		{
			sortList = new sortEntry[] {
				new sortEntry("Selection", SortType.Selection, SelectionSort<int>.Sort),
				new sortEntry("Insertion", SortType.Insertion, InsertionSort<int>.Sort),
				new sortEntry("Shell", SortType.Shell, ShellSort<int>.Sort),
				new sortEntry("Merge", SortType.Merge, MergeSort<int>.Sort),
				new sortEntry("Merge Bottom Up", SortType.MergeBU, MergeSort<int>.SortBU),
				new sortEntry("Merge X", SortType.MergeX, MergeSort<int>.SortX),
				new sortEntry("Heap", SortType.Heap, HeapSort<int>.Sort),
				new sortEntry("Quick", SortType.Quick, QuickSort<int>.Sort),
				new sortEntry("Quick3", SortType.Quick3, QuickSort<int>.Sort3way)
			};
		}
		static readonly Dictionary<SortType, Action<string[]>> strSortingDelegates = new Dictionary<SortType, Action<string[]>>()
        {
            { SortType.Selection,	SelectionSort<string>.Sort },
			{ SortType.Insertion,	InsertionSort<string>.Sort },
			{ SortType.Shell,		ShellSort<string>.Sort },
			{ SortType.Merge,		MergeSort<string>.Sort },
			{ SortType.MergeBU,		MergeSort<string>.SortBU },
			{ SortType.MergeX,		MergeSort<string>.SortX },
			{ SortType.Heap,		HeapSort<string>.Sort },
			{ SortType.Quick,		QuickSort<string>.Sort },
			{ SortType.Quick3,      QuickSort<string>.Sort3way }
		};

		static long RunSortString(SortType st, string[] a)
		{
			Console.WriteLine("Number of words: {0}", a.Length);

			Stopwatch sw = new Stopwatch();
			Console.Write("Running " + Enum.GetName(typeof(SortType), st) + " sort\nStart : ");
			Show(a);
			sw.Start();
			strSortingDelegates[st]?.Invoke(a);
			sw.Stop();
			Console.Write("Final : ");
			Show(a);
			Debug.Assert(a.IsSorted(), "Assert failed: Array is not sorted.");
			Console.WriteLine("Run time: {0}ms", sw.ElapsedMilliseconds);
			return sw.ElapsedMilliseconds;
		}
		static void GenerateRandomInts(int[] intArray)
		{
			Random r = new Random();
			for (int i = 0; i < intArray.Length; i++)
				intArray[i] = r.Next();
		}
		static void IntSortingSync(int size)
		{
			int[] intArray = new int[size];
			Stopwatch sw = new Stopwatch();
			GenerateRandomInts(intArray);

			foreach (sortEntry se in sortList)
			{
				int[] tempArray = (int[])intArray.Clone();
				Console.Write($"Using sort {se.sortName}...");
                sw.Start();
				se.sortDelegate?.Invoke(tempArray);
                sw.Stop();
                Console.WriteLine($"{sw.ElapsedMilliseconds}ms");
			}
		}
		static async Task RunSortAsync(SortType st, int[] intArray)
		{
			await Task.Run( () =>
		   {
			   Action<int[]> sorter;
			   switch(st)
			   {
				   case SortType.Selection:
					   sorter = SelectionSort<int>.Sort;
					   break;
				   case SortType.Insertion:
					   sorter = InsertionSort<int>.Sort;
					   break;
				   case SortType.Heap:
					   sorter = HeapSort<int>.Sort;
					   break;
				   case SortType.Quick:
					   sorter = QuickSort<int>.Sort;
					   break;
				   case SortType.Shell:
					   sorter = ShellSort<int>.Sort;
					   break;
				   case SortType.Merge:
					   sorter = MergeSort<int>.Sort;
					   break;
				   case SortType.MergeX:
					   sorter = MergeSort<int>.SortX;
					   break;
				   case SortType.MergeBU:
					   sorter = MergeSort<int>.SortBU;
					   break;
				   default:
					   sorter = null;
					   break;
			   }
               sorter(intArray);
		   });

		}
		static void IntSortingAsync(int size)
		{
			int[] intArray = new int[size];
			GenerateRandomInts(intArray);

			int[][] intArrays = new int[8][];
			for (int i = 0; i < intArrays.Length; i++)
				intArrays[i] = (int[])intArray.Clone();

			Task[] taskArray = new Task[]
			{
				RunSortAsync(SortType.Selection, intArrays[0]),
				RunSortAsync(SortType.Insertion, intArrays[1]),
				RunSortAsync(SortType.Shell, intArrays[2]),
				RunSortAsync(SortType.Heap, intArrays[3]),
				RunSortAsync(SortType.Quick, intArrays[4]),
				RunSortAsync(SortType.Merge, intArrays[5]),
				RunSortAsync(SortType.MergeX, intArrays[6]),
				RunSortAsync(SortType.MergeBU, intArrays[7])
			};
			Task.WaitAll(taskArray);
			for (int i = 0; i < intArrays.Length; i++)
				Debug.Assert(intArrays[i].IsSorted());
			Debug.WriteLine("Finished Wait");
		}
        public static void SortTestMain(string[] args)
        {
            string[] original = In.ReadStrings(args[0]);

            char inputKey;
            Debug.WriteLine("Console running on ID {0}", Thread.CurrentThread.ManagedThreadId);
            do
            {
                string[] a = (string[])original.Clone();
                Console.Write("[S]election, [I]nsertion, s[H]ell, [M]erge, Merge[B]U, Merge[X], [Q]uick, Hea[P], [*]int, [Z]=Quit:");
                inputKey = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();
                switch (inputKey)
                {
                    case 'S':
                        RunSortString(SortType.Selection, a);
                        break;
                    case 'I':
                        RunSortString(SortType.Insertion, a);
                        break;
                    case 'H':
                        RunSortString(SortType.Shell, a);
                        break;
                    case 'M':
                        RunSortString(SortType.Merge, a);
                        break;
                    case 'B':
                        RunSortString(SortType.MergeBU, a);
                        break;
                    case 'P':
                        RunSortString(SortType.Heap, a);
                        break;
                    case 'X':
                        RunSortString(SortType.MergeX, a);
                        break;
                    case 'Q':
                        RunSortString(SortType.Quick, a);
                        break;
                    case '*':
                        IntSortingSync(15000);
                        IntSortingAsync(15000);
                        break;
                }
            } while (inputKey != 'Z');
        }
    }
}
