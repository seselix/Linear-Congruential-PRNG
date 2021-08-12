using System;
using System.Collections.Generic;
using System.Diagnostics;

// http://www.codeabbey.com/index/task_view/linear-congruential-generator
// https://www.math.arizona.edu/~tgk/mc/book_chap3.pdf
// http://statmath.wu.ac.at/software/prng/doc/prng.html#Table_005fLCG <- constant equation numbers are from here

namespace LinearCongruential
{
	class LinearCGen
	{
		static double a = 1583458089;
		static double c = 0;
		static double m = Math.Pow(2, 31) - 1;
		static double xCur = 1;
		static double n = 1000;
		static double xNext = 0;

		static void Main(string[] args)
		{
			Stopwatch sw = new Stopwatch();
			sw.Restart();

			List<Range> ranges = new List<Range>
			{
				new Range(1, 10), new Range(210, 300), new Range(4100, 5000), new Range(61000, 70000), new Range(810000, 900000),
			};

			for (int i = 0; i < n; i++)
			{
				Console.WriteLine(RandomRanges(ranges));
			}

			sw.Stop();
			Console.WriteLine("\n\nTime Elapsed: ");
			Console.WriteLine(sw.ElapsedMilliseconds + " ms");
			Console.WriteLine(sw.ElapsedTicks + " ticks");
		}

		static double LCG()
		{
			xNext = (a * xCur + c) % m;
			xCur = xNext;
			return xNext;
		}

		static int RandomInt(int min, int max)
		{
			return (int)Math.Floor(LCG() / m * (max - min + 1)) + (min);
		}

		static double RandomDouble()
		{
			return LCG() / m;
		}

		static int RandomRanges(List<Range> ranges)
		{
			double result = RandomDouble();
			double z = 1.0 / ranges.Count;
			for (int i = 0; i < ranges.Count; i++)
			{
				//Console.WriteLine("Range " + (i + 1) + ": ");
				//Console.WriteLine(z + (z * i) + " > " + result);
				if (result < z + (z * i))
				{
					return RandomInt(ranges[i].min, ranges[i].max);
				}
			}
			return 0;
		}
	}

	class Range
	{
		public int min, max;
		public Range(int min, int max)
		{
			this.min = min;
			this.max = max;
		}
	}
}
