using MoreLinq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using static AoC2020.Code.BitMasks;

namespace AoC2020.Tests
{
	class Day14
	{
		private const string TestInputPath = @"C:\Users\Lewis\source\repos\AoC2020\AoC2020.Tests\Inputs\Day14_test1.txt";
		private const string InputPath = @"C:\Users\Lewis\source\repos\AoC2020\AoC2020.Tests\Inputs\Day14_1.txt";

		[TestCase(0,  "000000000000000000000000000000000000")]
		[TestCase(2,  "000000000000000000000000000000000010")]
		[TestCase(3,  "000000000000000000000000000000000011")]
		[TestCase(4,  "000000000000000000000000000000000100")]
		[TestCase(11, "000000000000000000000000000000001011")]
		[TestCase(73, "000000000000000000000000000001001001")]
		public void Test1(long n, string expected)
		{
			var result = NumberTo34BitString(n);
			Assert.AreEqual(expected, result);
		}

		[TestCase(11,  "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X", 73)]
		[TestCase(101, "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X", 101)]
		[TestCase(0,   "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X", 64)]
		public void TestApplyMask(long n, string mask, long expected)
		{
			var result = MaskNumber(mask, n);
			Assert.AreEqual(expected, result);
		}

		[TestCase(TestInputPath, 165)]
		[TestCase(InputPath, 11179633149677)]
		public void IntegrationTest(string path, long ans)
		{
			var text = File.ReadAllLines(path);
			var parsed = text.Select(x => ParseInstruction(x));

			var state = new State("");

			parsed.ForEach(x => state.Process(x));

			Assert.AreEqual(ans, state.SumMemory());
		}
	}
}
