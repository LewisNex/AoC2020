using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using static AoC2020.Code.TimeTables;

namespace AoC2020.Tests
{
	class Day13
	{
		private const string InputPath = @"C:\Users\Lewis\source\repos\AoC2020\AoC2020.Tests\Inputs\Day13_1.txt";

		[TestCase(59, 5)]
		[TestCase(7, 6)]
		[TestCase(13, 10)]
		public void TestHowLongToWait(int id, int expected)
		{
			Assert.AreEqual(expected, HowLongToWait(939, id));
		}

		[Test]
		public void TestGetEarliestBus()
		{
			var input = new[]
			{
				7,13,59,31,19
			};
			var result = GetEarliestBus(input, 939);
			var id = result.Item1;
			var time = result.Item2;
			Assert.AreEqual(59, id);
			Assert.AreEqual(5, time);
		}


		[Test]
		public void Real1()
		{
			var text = File.ReadAllLines(InputPath);
			var timestamp = int.Parse(text[0]);
			var input = text[1].Split(",");//.Where(x => !string.Equals("x", x)).Select(int.Parse);
			//var result = GetEarliestBus(input, timestamp);
			//var id = result.Item1;
			//var time = result.Item2;
			//Assert.AreEqual(59, id*time);
		}
	}
}
