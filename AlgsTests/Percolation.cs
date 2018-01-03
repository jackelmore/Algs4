using System;
using jackel.Stdlib;
using jackel.UnionFind;

namespace jackel.Percolation
{
    public class Percolation
    {
        private WeightedQuickUnion uf;
        private bool[] openArray;
        private const int vTop = 0;
        private readonly int vBottom;
        private int numNodes;
        private int n;

        public Percolation(int N)
        {           // create N-by-N grid, with all sites blocked
            n = N;
            numNodes = (N * N) + 2;
            uf = new WeightedQuickUnion(numNodes);
            openArray = new bool[numNodes-1];
            vBottom = (N * N) + 1;
            for (int x = 0; x < numNodes-1; x++)
                openArray[x] = false;
        }

        public void Open(int i, int j)
        {   // open site (row i, column j) if it is not open already
            if (IsOpen(i, j))
                return;
            int index = ToIndex(i, j), above, below, left, right;
            above = SiteAbove(i, j);
            below = SiteBelow(i, j);
            left = SiteLeft(i, j);
            right = SiteRight(i, j);
            openArray[index] = true;
            if (above != -1)
                if (openArray[above] == true)
                    uf.union(index, above);
            if (below != -1)
                if (openArray[below] == true)
                    uf.union(index, below);
            if (left != -1)
                if (openArray[left] == true)
                    uf.union(index, left);
            if (right != -1)
                if (openArray[right] == true)
                    uf.union(index, right);
            if (i == 1)
                uf.union(index, vTop);      // connect top row to vTop (position 0)
            else if (i == n)
                uf.union(index, vBottom);   // connect bottom row to vBottom
        }
        private int ToIndex(int i, int j)           // return index value for (row i, column j)
        {
            // check for out of bounds
            if (IsValid(i, j))
                return ((i - 1) * n) + j;
            return -1;
        }
        private bool IsValid(int i, int j)
        {
            if (i < 1 || i > n || j < 1 || j > n)
                throw new IndexOutOfRangeException();
            return true;
        }
        private int SiteAbove(int i, int j)
        {
            if (IsValid(i, j))
            {
                if (i == 1)         // there is no row above i==1
                    return -1;
                return ToIndex(i - 1, j);
            }
            return -1;
        }
        private int SiteBelow(int i, int j)
        {
            if (IsValid(i, j))
            {
                if (i > (n - 1))
                    return -1;
                return ToIndex(i + 1, j);
            }
            return -1;
        }
        private int SiteLeft(int i, int j)
        {
            if (IsValid(i, j))
            {
                if (j == 1)
                    return -1;
                return ToIndex(i, j - 1);
            }
            return -1;
        }
        private int SiteRight(int i, int j)
        {
            if (IsValid(i, j))
            {
                if (j == n)
                    return -1;
                return ToIndex(i, j + 1);
            }
            return -1;
        }
        public bool IsOpen(int i, int j) // is site (row i, column j) open?
        {
            return openArray[ToIndex(i, j)];
        }
        public bool IsFull(int i, int j) // is site (row i, column j) full?
        {
            if (openArray[ToIndex(i, j)] == true) // this is an open site
                if (uf.connected(vTop, ToIndex(i, j)))
                    return true;
            return false;
        }
        public bool Percolates()         // does the system percolate?
        {
            // check bottom row
            //for (int i = (n * n) - n + 1; i <= (n * n); i++)
            //    if (uf.connected(vTop, i))
            //        return true;
            //return false;
            return (uf.connected(vTop, vBottom));
        }
        public static void PercolationTestMain(String[] args)
        {
            In file = new In(args[0]);
            int N = file.ReadInt(), row, column;
            Percolation p = new Percolation(N);
            while (!file.IsEmpty())
            {
                row = file.ReadInt();
                column = file.ReadInt();
                p.Open(row, column);
                if (p.Percolates())
                    Console.WriteLine("System percolates");
                else
                    Console.WriteLine("System does not percolate");
            }
            Console.ReadLine();
        }
    }
}

