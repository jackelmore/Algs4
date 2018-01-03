using System;
using jackel.Stdlib;

namespace jackel.Percolation
{
    public class PercolationStats
    {
        double[] tally;

        private void RunSimulation(int N, int T)
        {
            tally = new double[T];
            int i;
            for (int expnum = 0; expnum < T; expnum++)
            {
                i = 0;
                int row, column;
                Percolation p = new Percolation(N);
                while (!p.Percolates())
                {
                    row = StdRandom.Uniform(1, N + 1);
                    column = StdRandom.Uniform(1, N + 1);
                    if (!p.IsOpen(row, column))
                    {
                        p.Open(row, column);
                        i++;
                    }
                }
                tally[expnum] = (double)i / (N * N); // number of open sites divided by total sites
            }
            double mean = StdStats.mean(tally);
            double stddev = StdStats.stddev(tally);
            double confidence = (double)((1.96 * stddev) / Math.Sqrt(T));
            double confHi = (double)mean + confidence;
            double confLo = (double)mean - confidence;
            Console.WriteLine("mean()                  = {0}", mean);
            Console.WriteLine("stddev()                = {0}", stddev);
            Console.WriteLine("95% confidence interval = {0}, {1}", confLo, confHi);
        }

        public PercolationStats(int N, int T)
        {
            if (N <= 0 || T <= 0)
                throw new ArgumentException();
            RunSimulation(N, T);
        }
        public double Mean()
        {
            return StdStats.mean(tally);
        }
        public double Stddev()
        {
            return StdStats.stddev(tally);
        }
        public double confidenceLo()
        {
            return 0;
        }
        public double ConfidenceHi()
        {
            return 0;
        }

        public static void PercStatsTestMain(String[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("NotEnoughArguments -- usage: PercolationStats N T\n");
            }
            int N = int.Parse(args[0]);
            int T = int.Parse(args[1]);
            PercolationStats pstat = new PercolationStats(N, T);
            Console.ReadLine();
        }
   }
}
