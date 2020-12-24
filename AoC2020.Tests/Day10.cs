using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using static AoC2020.Code.Joltage;

namespace AoC2020.Tests
{
	class Day10
	{
		private const string TestInputPath1 = @"C:\Users\Lewis\source\repos\AoC2020\AoC2020.Tests\Inputs\Day10_test1.txt";
		private const string TestInputPath2 = @"C:\Users\Lewis\source\repos\AoC2020\AoC2020.Tests\Inputs\Day10_test2.txt";
		private const string InputPath = @"C:\Users\Lewis\source\repos\AoC2020\AoC2020.Tests\Inputs\Day10_1.txt";

		[Test]
		public void Test1()
		{
			var input = File.ReadLines(TestInputPath1).Select(x => int.Parse(x));

			var differences = GetJoltageJumps(input);

			Assert.AreEqual(7, differences[1]);
			Assert.AreEqual(5, differences[3]);
		}

		[Test]
		public void Test2()
		{
			var input = File.ReadLines(TestInputPath2).Select(x => int.Parse(x));

			var differences = GetJoltageJumps(input);

			Assert.AreEqual(22, differences[1]);
			Assert.AreEqual(10, differences[3]);
		}

		[Test]
		public void Real1()
		{
			var input = File.ReadLines(InputPath).Select(x => int.Parse(x));

			var differences = GetJoltageJumps(input);

			Assert.AreEqual(74, differences[1]);
			Assert.AreEqual(41, differences[3]);

			Assert.AreEqual(3034, differences[1] * differences[3]);
		}


		[Test]
		public void Test3()
		{
			var input = File.ReadLines(TestInputPath1).Select(x => int.Parse(x));

			var options = GetOptions(input);

			Assert.AreEqual(8, options);
		}

		[Test]
		public void Test3_long()
		{
			var input = File.ReadLines(TestInputPath2).Select(x => int.Parse(x));

			var options = GetOptions(input);

			Assert.AreEqual(19208, options);
		}


		[Test]
		public void Real2()
		{
			var input = File.ReadLines(InputPath).Select(x => int.Parse(x));

			var options = GetOptions(input);

			Assert.AreEqual(959315968, options); // wrong - not sure how these are been multiplied incorrectly. It works by hand
		}
	}
}
