using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriQueue
{
    public class Transaction : IComparable<Transaction>
    {
        public int CompareTo(Transaction other)
        {
            throw new NotImplementedException();
        }
    }
    public class MaxPQ<Key> where Key : IComparable<Key>
    {
        private Key[] pq;
        private int N = 0;
        public MaxPQ(int maxN)
        {
            pq = new Key[maxN + 1];
        }
        void insert(Key v)
        {
            pq[++N] = v;
            swim(N);
        }
        Key delMax()
        {
            Key max = pq[1];
            exch(1, N--);
            pq[N + 1] = null;
            sink(1);
            return max;
        }
        public bool isEmpty()
        {
            return N == 0;
        }
        public int size()
        {
            return N;
        }
        private void swim(int k)
        {
            while(k > 1 && less(k/2, k))
            {
                exch(k / 2, k);
                k = k / 2;
            }
        }
        private void sink(int k)
        {
            while(2*k <= N)
            {
                int j = 2 * k;
                if (j < N && less(j, j + 1))
                    j++;
                if (!less(k, j))
                    break;
                exch(k, j);
                k = j;
            }
        }
        private bool less(int i, int j)
        {
            return pq[i].CompareTo(pq[j]) < 0;
        }
        private void exch(int i, int j)
        {
            Key t = pq[i];
            pq[i] = pq[j];
            pq[j] = t;
        }
    }


    class TopM
    {
        static void Main(string[] args)
        {
            int M = int.Parse(args[0]);
            MinPQ
        }
    }
}
