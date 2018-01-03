using System;

namespace jackel.Stdlib
{
    public static class StdStats
    {
        /**
          * Returns the maximum value in the array a[], -infinity if no such value.
          */
        public static double max(double[] a)
        {
            double max = Double.NegativeInfinity;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] > max) max = a[i];
            }
            return max;
        }

        /**
          * Returns the maximum value in the subarray a[lo..hi], -infinity if no such value.
          */
        public static double max(double[] a, int lo, int hi)
        {
            if (lo < 0 || hi >= a.Length || lo > hi)
                throw new InvalidOperationException("Subarray indices out of bounds");
            double max = Double.NegativeInfinity;
            for (int i = lo; i <= hi; i++)
            {
                if (a[i] > max) max = a[i];
            }
            return max;
        }
        /**
          * Returns the maximum value in the array a[], Integer.MIN_VALUE if no such value.
          */
        public static int max(int[] a)
        {
            int max = int.MinValue;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] > max) max = a[i];
            }
            return max;
        }
        /**
          * Returns the minimum value in the array a[], +infinity if no such value.
          */
        public static double min(double[] a)
        {
            double min = Double.PositiveInfinity;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] < min) min = a[i];
            }
            return min;
        }

        /**
          * Returns the minimum value in the subarray a[lo..hi], +infinity if no such value.
          */
        public static double min(double[] a, int lo, int hi)
        {
            if (lo < 0 || hi >= a.Length || lo > hi)
                throw new InvalidOperationException("Subarray indices out of bounds");
            double min = Double.PositiveInfinity;
            for (int i = lo; i <= hi; i++)
            {
                if (a[i] < min) min = a[i];
            }
            return min;
        }

        /**
          * Returns the minimum value in the array a[], Integer.MAX_VALUE if no such value.
          */
        public static int min(int[] a)
        {
            int min = int.MaxValue;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] < min) min = a[i];
            }
            return min;
        }
        /**
          * Returns the average value in the array a[], NaN if no such value.
          */
        public static double mean(double[] a)
        {
            if (a.Length == 0) return Double.NaN;
            double s = sum(a);
            return s / a.Length;
        }
        /**
          * Returns the average value in the array a[], NaN if no such value.
          */
        public static double mean(int[] a)
        {
            if (a.Length == 0) return Double.NaN;
            double sum = 0.0;
            for (int i = 0; i < a.Length; i++)
            {
                sum = sum + a[i];
            }
            return sum / a.Length;
        }
        /**
        * Returns the average value in the subarray a[lo..hi], NaN if no such value.
        */
        public static double mean(double[] a, int lo, int hi)
        {
            int length = hi - lo + 1;
            if (lo < 0 || hi >= a.Length || lo > hi)
                throw new InvalidOperationException("Subarray indices out of bounds");
            if (length == 0) return Double.NaN;
            double s = sum(a, lo, hi);
            return s / length;
        }

        /**
          * Returns the sum of all values in the subarray a[lo..hi].
          */
        public static double sum(double[] a, int lo, int hi)
        {
            if (lo < 0 || hi >= a.Length || lo > hi)
                throw new InvalidOperationException("Subarray indices out of bounds");
            double sum = 0.0;
            for (int i = lo; i <= hi; i++)
            {
                sum += a[i];
            }
            return sum;
        }
        /**
          * Returns the sum of all values in the array a[].
          */
        public static int sum(int[] a)
        {
            int sum = 0;
            for (int i = 0; i < a.Length; i++)
            {
                sum += a[i];
            }
            return sum;
        }
        /**
          * Returns the sum of all values in the array a[].
          */
        public static double sum(double[] a)
        {
            double sum = 0.0;
            for (int i = 0; i < a.Length; i++)
            {
                sum += a[i];
            }
            return sum;
        }
        /**
          * Returns the sample standard deviation in the subarray a[lo..hi], NaN if no such value.
          */
        public static double stddev(double[] a, int lo, int hi)
        {
            return Math.Sqrt(var(a, lo, hi));
        }
        /**
          * Returns the sample standard deviation in the array a[], NaN if no such value.
          */
        public static double stddev(int[] a)
        {
            return Math.Sqrt(var(a));
        }
        /**
          * Returns the sample standard deviation in the array a[], NaN if no such value.
          */
        public static double stddev(double[] a)
        {
            return Math.Sqrt(var(a));
        }
        /**
          * Returns the population standard deviation in the subarray a[lo..hi], NaN if no such value.
          */
        public static double stddevp(double[] a, int lo, int hi)
        {
            return Math.Sqrt(varp(a, lo, hi));
        }
        /**
          * Returns the sample standard deviation in the array a[], NaN if no such value.
          */
        public static double stddevp(double[] a)
        {
            return Math.Sqrt(varp(a));
        }

        /**
          * Returns the sample variance in the array a[], NaN if no such value.
          */
        public static double var(double[] a)
        {
            if (a.Length == 0) return Double.NaN;
            double avg = mean(a);
            double sum = 0.0;
            for (int i = 0; i < a.Length; i++)
            {
                sum += (a[i] - avg) * (a[i] - avg);
            }
            return sum / (a.Length - 1);
        }
        /**
          * Returns the sample variance in the subarray a[lo..hi], NaN if no such value.
          */
        public static double var(double[] a, int lo, int hi)
        {
            int length = hi - lo + 1;
            if (lo < 0 || hi >= a.Length || lo > hi)
                throw new InvalidOperationException("Subarray indices out of bounds");
            if (length == 0) return Double.NaN;
            double avg = mean(a, lo, hi);
            double sum = 0.0;
            for (int i = lo; i <= hi; i++)
            {
                sum += (a[i] - avg) * (a[i] - avg);
            }
            return sum / (length - 1);
        }
        /**
          * Returns the sample variance in the array a[], NaN if no such value.
          */
        public static double var(int[] a)
        {
            if (a.Length == 0) return Double.NaN;
            double avg = mean(a);
            double sum = 0.0;
            for (int i = 0; i < a.Length; i++)
            {
                sum += (a[i] - avg) * (a[i] - avg);
            }
            return sum / (a.Length - 1);
        }
        /**
          * Returns the population variance in the subarray a[lo..hi], NaN if no such value.
          */
        public static double varp(double[] a, int lo, int hi)
        {
            int length = hi - lo + 1;
            if (lo < 0 || hi >= a.Length || lo > hi)
                throw new InvalidOperationException("Subarray indicies out of bounds");
            if (length == 0) return Double.NaN;
            double avg = mean(a, lo, hi);
            double sum = 0.0;
            for (int i = lo; i <= hi; i++)
            {
                sum += (a[i] - avg) * (a[i] - avg);
            }
            return sum / length;
        }
        /**
          * Returns the population variance in the array a[], NaN if no such value.
        */
        public static double varp(double[] a)
        {
            if (a.Length == 0) return Double.NaN;
            double avg = mean(a);
            double sum = 0.0;
            for (int i = 0; i < a.Length; i++)
            {
                sum += (a[i] - avg) * (a[i] - avg);
            }
            return sum / a.Length;
        }

    }
}
