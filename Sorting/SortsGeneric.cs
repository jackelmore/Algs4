using System;
using System.Collections.Generic;
using jackel.Stdlib;

namespace jackel.Sorting.Generic
{
	public static class TypeHelpers
	{
		public static bool IsSorted(this string[] s) => AbstractSort<string>.IsSorted(s);
		public static bool IsSorted(this int[] i) => AbstractSort<int>.IsSorted(i);
	}
	public abstract class AbstractSort<T> where T : IComparable<T>
	{
		protected static bool Less(T v, T w) => v.CompareTo(w) < 0;
		protected static bool Less(T a, T b, IComparer<T> comparer) => (comparer.Compare(a, b) < 0);
		protected static void Exch(T[] a, int i, int j)
		{
			if (i == j) // don't swap self
				return;
			T t = a[i];
			a[i] = a[j];
			a[j] = t;
		}
		public static bool IsSorted(T[] a)
		{
			for (int i = 1; i < a.Length; i++)
				if (Less(a[i], a[i - 1]))
					return false;
			return true;
		}
		public static bool IsSorted(T[] a, int lo, int hi)
		{
			for (int i = lo + 1; i <= hi; i++)
				if (Less(a[i], a[i - 1]))
					return false;
			return true;
		}
		public static bool IsSorted(T[] a, IComparer<T> comp) => IsSorted(a, 0, a.Length - 1, comp);
		public static bool IsSorted(T[] a, int lo, int hi, IComparer<T> comp)
		{
			for (int i = lo + 1; i <= hi; i++)
				if (Less(a[i], a[i - 1], comp))
					return false;
			return true;
		}
	}
	public class SelectionSort<T> : AbstractSort<T> where T : IComparable<T>
	{
		public static void Sort(T[] a)
		{
			int N = a.Length;
			for (int i = 0; i < N; i++)
			{
				int min = i;
				// i proceeds linearly through the array from 0 to x, j finds the element that is the smallest compared to i, then swaps it
				for (int j = i + 1; j < N; j++)
					if (Less(a[j], a[min]))
						min = j; // there is a new minimum value
				Exch(a, i, min);
			}
		}
	}
	public class InsertionSort<T> : AbstractSort<T> where T: IComparable<T>
	{
		public static void Sort(T[] a)
		{
			Sort(a, 0, a.Length - 1);
		}
		public static void Sort(T[] a, int lo, int hi)
		{
			for (int i = lo; i <= hi; i++)
				for (int j = i; j > lo && Less(a[j], a[j - 1]); j--)
					Exch(a, j, j - 1);
		}
	}
	public class MergeSort<T> : AbstractSort<T> where T : IComparable<T>
	{
		private const int CUTOFF = 7;

		public static void Sort(T[] a)
		{
			T[] aux = new T[a.Length];
			Sort(a, aux, 0, a.Length - 1);
		}
		private static void Sort(T[] a, T[] aux, int lo, int hi)
		{
			if (hi <= lo)
				return;
			int mid = lo + (hi - lo) / 2;
			Sort(a, aux, lo, mid); // sort lower half
			Sort(a, aux, mid + 1, hi); // sort upper half
			Merge(a, aux, lo, mid, hi); // merge together
		}
		private static void Merge(T[] a, T[] aux, int lo, int mid, int hi)
		{
			int i = lo, j = mid + 1;

			Array.Copy(a, lo, aux, lo, hi - lo + 1);
			for (int k = lo; k <= hi; k++)
			{
				if (i > mid)
					a[k] = aux[j++];
				else if (j > hi)
					a[k] = aux[i++];
				else if (Less(aux[j], aux[i]))
					a[k] = aux[j++];
				else
					a[k] = aux[i++];
			}
		}
		public static void SortBU(T[] a)
		{
			int N = a.Length;
			T[] aux = new T[N];
			for (int sz = 1; sz < N; sz = sz + sz)
				for (int lo = 0; lo < N - sz; lo += sz + sz)
					Merge(a, aux, lo, lo + sz - 1, Math.Min(lo + sz + sz - 1, N - 1));
		}
		private static void MergeX(T[] src, T[] dst, int lo, int mid, int hi)
		{
			//Debug.Assert(isSorted(src, lo, mid));
			//Debug.Assert(isSorted(src, mid + 1, hi));

			int i = lo, j = mid + 1;
			for (int k = lo; k <= hi; k++)
			{
				if (i > mid)
					dst[k] = src[j++];
				else if (j > hi)
					dst[k] = src[i++];
				else if (Less(src[j], src[i]))
					dst[k] = src[j++];
				else
					dst[k] = src[i++];
			}
			//Debug.Assert(isSorted(dst, lo, hi));
		}
		private static void SortX(T[] src, T[] dst, int lo, int hi)
		{
			if (hi <= lo + CUTOFF)
			{
				InsertionSort<T>.Sort(dst, lo, hi);
				return;
			}
			int mid = lo + (hi - lo) / 2;
			SortX(dst, src, lo, mid);
			SortX(dst, src, mid + 1, hi);

			if (!Less(src[mid + 1], src[mid]))
			{
				Array.Copy(src, lo, dst, lo, hi - lo + 1);
				return;
			}
			MergeX(src, dst, lo, mid, hi);
		}
		public static void SortX(T[] a)
		{
			T[] aux = (T[])a.Clone();
			SortX(aux, a, 0, a.Length - 1);
			//Debug.Assert(isSorted(a));
		}
	}
	public class ShellSort<T> : AbstractSort<T> where T : IComparable<T>
	{
		public static void Sort(T[] a)
		{
			int N = a.Length;
			int h = 1;
			while (h < N / 3)
				h = (3 * h) + 1;
			while (h >= 1)
			{
				for (int i = h; i < N; i++)
				{
					for (int j = i; j >= h && Less(a[j], a[j - h]); j -= h)
						Exch(a, j, j - h);
				}
				h = h / 3;
			}
		}
	}
	public class HeapSort<T> : AbstractSort<T> where T : IComparable<T>
	{
		private static bool LessOffset1(T[] pq, int i, int j) => pq[i - 1].CompareTo(pq[j - 1]) < 0;
		public static void Sort(T[] pq)
		{
			int N = pq.Length;
			for (int k = N / 2; k >= 1; k--)
				Sink(pq, k, N);
			while (N > 1)
			{
				Exch(pq, 0, (N--) - 1); // originally exchHS(pq, 1, N--), offset by 1 (algorithm is 1-based, array is 0-based)
				Sink(pq, 1, N);
			}
		}
		private static void Sink(T[] pq, int k, int N)
		{
			while (2 * k <= N)
			{
				int j = 2 * k;
				if (j < N && LessOffset1(pq, j, j + 1))
					j++;
				if (!LessOffset1(pq, k, j))
					break;
				Exch(pq, k - 1, j - 1); // originally exchHS(pq, k, j), offset by 1 (algorithm is 1-based, array is 0-based)
				k = j;
			}
		}
	}
	public class QuickSort<T> : AbstractSort<T> where T : IComparable<T>
	{
		private static int PartitionQS(T[] a, int lo, int hi)
		{
			int i = lo, j = hi + 1;
			T v = a[lo];
			while (true)
			{
				while (Less(a[++i], v))
					if (i == hi)
						break;
				while (Less(v, a[--j]))
					if (j == lo)
						break;
				if (i >= j)
					break;
				Exch(a, i, j);
			}
			Exch(a, lo, j);
			return j;
		}
		public static void Sort(T[] a)
		{
			StdRandom.Shuffle(a);
			Sort(a, 0, a.Length - 1);
		}
		public static void Sort3way(T[] a)
		{
			StdRandom.Shuffle(a);
			Sort3way(a, 0, a.Length - 1);
		}
		private static void Sort(T[] a, int lo, int hi)
		{
			if (hi <= lo)
				return;
			int j = PartitionQS(a, lo, hi);
			Sort(a, lo, j - 1);
			Sort(a, j + 1, hi);
		}
		private static void Sort3way(T[] a, int lo, int hi)
		{
			if (hi <= lo)
				return;
			int lt = lo, gt = hi;
			T v = a[lo];
			int i = lo;
			while (i <= gt)
			{
				int cmp = a[i].CompareTo(v);
				if (cmp < 0)
					Exch(a, lt++, i++);
				else if (cmp > 0)
					Exch(a, i, gt--);
				else
					i++;
			}
			Sort3way(a, lo, lt - 1);
			Sort3way(a, gt + 1, hi);
		}
	}
}
