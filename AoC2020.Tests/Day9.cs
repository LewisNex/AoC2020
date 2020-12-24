using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

using static AoC2020.Code.Encoding;

namespace AoC2020.Tests
{
	class Day9
	{
		private const string TestInputPath = @"C:\Users\Lewis\source\repos\AoC2020\AoC2020.Tests\Inputs\Day9_test1.txt";
		private const string InputPath = @"C:\Users\Lewis\source\repos\AoC2020\AoC2020.Tests\Inputs\Day9_1.txt";

		[Test]
		public void Test1()
		{
			var input = File.ReadAllLines(TestInputPath).Select(x => long.Parse(x));

			var result = FindInvalids(5, input);
			Assert.AreEqual(1, result.Count());
			Assert.AreEqual(127L, result.First());
		}

		[Test]
		public void Real1()
		{
			var input = File.ReadAllLines(InputPath).Select(x => long.Parse(x));

			var result = FindInvalids(25, input);
			var first = result.First();
			Assert.AreEqual(31161678L, first);

			var potentials = input.Where(x => x != first);
			var weaknesses = GetWeaknesses(first, potentials);
			Assert.AreEqual(5453868, weaknesses.Min() + weaknesses.Max());
		}
	}
}
