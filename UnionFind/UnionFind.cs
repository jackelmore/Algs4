using System;

namespace jackel.UnionFind
{
    interface IUnionFind
    {
        void union(int p, int q);
        int find(int p);
        bool connected(int p, int q);
        int count();
    }
    public class QuickFind : IUnionFind
    {
        protected int[] parent;
        protected int[] rank;
        protected int iCount;

        public void WriteIds()
        {
            Console.Write("Dump: ");
            for (int i = 0; i < parent.Length; i++)
                Console.Write($"{parent[i]}  ");
            Console.WriteLine();
        }
        public QuickFind(int N)
        {
            if (N < 0)
                throw new ArgumentException($"QuickFind({N}) is invalid and cannot be less than zero!");
            iCount = N;
            parent = new int[N];
            rank = new int[N];
            for (int i = 0; i < N; i++)
            {
                parent[i] = i;
                rank[i] = 0;
            }
        }
        public int count()
        {
            return iCount;
        }
        public bool connected(int p, int q)
        {
            return find(p) == find(q);
        }

        protected void validate(int p)
        {
            int N = parent.Length;
            if (p < 0 || p >= N)
                throw new IndexOutOfRangeException($"index {p} is not between 0 and {N - 1}");
        }
        public virtual int find(int p)
        {
            validate(p);
            return parent[p];
        }

        public virtual void union(int p, int q)
        {
            int pID = find(p);
            int qID = find(q);

            if (pID == qID)
                return;

            for (int i = 0; i < parent.Length; i++)
                if (parent[i] == pID)
                    parent[i] = qID;
            iCount--;
        }
    }

    public class QuickUnion : QuickFind
    {
        public QuickUnion(int N) : base(N) { }

        public override int find(int p)
        {
            validate(p);
            while (p != parent[p])
                p = parent[p];
            return p;
        }
        public override void union(int p, int q)
        {
            int pRoot = find(p);
            int qRoot = find(q);
            if (pRoot == qRoot) return;
            parent[pRoot] = qRoot;

            iCount--;
        }
    }

    public class WeightedQuickUnion : QuickUnion
    {
        public WeightedQuickUnion(int N) : base(N) { }
        public override void union(int p, int q)
        {
            int rootP = find(p);
            int rootQ = find(q);
            if (rootP == rootQ)
                return;
            if (rank[rootP] < rank[rootQ])
                parent[rootP] = rootQ;
            else if (rank[rootP] > rank[rootQ])
                parent[rootQ] = rootP;
            else
            {
                parent[rootQ] = rootP;
                rank[rootP]++;
            }
            iCount--;
        }
    }

    public class QuickUnionPathComp : QuickUnion
    {
        public QuickUnionPathComp(int N) : base(N) { }
        public override int find(int p)
        {
            validate(p);
            while (p != parent[p])
            {
                parent[p] = parent[parent[p]];
                p = parent[p];
            }
            return p;
        }
    }

    public class WeightedQuickUnionPathComp : WeightedQuickUnion
    {
        public WeightedQuickUnionPathComp(int N) : base(N) { }
        public override int find(int p)
        {
            validate(p);
            while (p != parent[p])
            {
                parent[p] = parent[parent[p]];
                p = parent[p];
            }
            return p;
        }
    }
}
