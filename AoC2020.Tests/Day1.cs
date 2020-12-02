using Microsoft.FSharp.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static AoC2020.Code.ExpenseReport;

namespace AoC2020.Tests
{
	public class ExpensesTests
	{
		private const int N = 2020;
		private const string InputPath = @"C:\Users\Lewis\source\repos\AoC2020\AoC2020.Tests\Inputs\Day1_1.txt";

		[Test]
		public void Test1()
		{
			var input = new[]
			{
				1721,
				979,
				366,
				299,
				675,
				1456,
			};
			Assert.AreEqual(514579, GetResultOfSummedPair(input));
			Assert.AreEqual(241861950, GetResultOfSummedTriple(input));
		}

		[Test]
		[Explicit]
		public void Real1()
		{
			var input = File.ReadAllLines(InputPath)
				.Select(int.Parse);

			Assert.AreEqual(259716, GetResultOfSummedPair(input));
			Assert.AreEqual(120637440, GetResultOfSummedTriple(input));
		}

		private int GetResultOfSummedPair(IEnumerable<int> input)
		{
			return GetProductOfSummedPair(N, SeqModule.ToList(input));
		}

		private int GetResultOfSummedTriple(IEnumerable<int> input)
		{
			return GetProductOfSummedTriple(N, SeqModule.ToList(input));
		}
	}
}