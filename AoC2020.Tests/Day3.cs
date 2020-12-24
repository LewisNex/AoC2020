using Microsoft.FSharp.Collections;
using NUnit.Framework;
using System.IO;
using static AoC2020.Code.Sledding;

namespace AoC2020.Tests
{
	internal class Day3
	{
		private const string TestInputPath = @"C:\Users\Lewis\source\repos\AoC2020\AoC2020.Tests\Inputs\Day3_test1.txt";
		private const string InputPath = @"C:\Users\Lewis\source\repos\AoC2020\AoC2020.Tests\Inputs\Day3_1.txt";

		[Test]
		public void TestArrayBuiltFromMap()
		{
			var input1 = new[]
			{
				"..#",
				"...",
				"..."
			};
			var map = new Map(SeqModule.ToList(input1));

			Assert.IsTrue(map.HasTree(2,0));
			Assert.IsTrue(map.HasTree(5,0));
			Assert.IsTrue(map.HasTree(8,0));

			Assert.IsFalse(map.HasTree(0, 0));
			Assert.IsFalse(map.HasTree(0, 1));
			Assert.IsFalse(map.HasTree(0, 2));
			Assert.IsFalse(map.HasTree(1, 0));
			Assert.IsFalse(map.HasTree(1, 1));
			Assert.IsFalse(map.HasTree(1, 2));
			Assert.IsFalse(map.HasTree(2, 1));
			Assert.IsFalse(map.HasTree(2, 2));
		}

		[Test]
		public void TestTreeCounts()
		{

			var input1 = new[]
			{
				"...",
				"#..",
				"#.."
			};

			var map1 = new Map(SeqModule.ToList(input1));

			Assert.AreEqual(2, map1.CountTrees(0, 0, 0, 1));
			Assert.AreEqual(0, map1.CountTrees(0, 0, 1, 1));


			var input2 = new[]
			{
				"#..",
				"#..",
				"#.."
			};

			var map2 = new Map(SeqModule.ToList(input2));

			Assert.AreEqual(2, map2.CountTrees(0, 0, 0, 1));

			var input3 = new[]
			{
				"#..",
				".#.",
				"..#"
			};

			var map3 = new Map(SeqModule.ToList(input3));

			Assert.AreEqual(2, map3.CountTrees(0, 0, 1, 1));
			Assert.AreEqual(0, map3.CountTrees(0, 0, 0, 1));
			Assert.AreEqual(1, map3.CountTrees(2, 0, -1, 1));
		}

		[Test]
		public void Test1()
		{

			var input = File.ReadAllLines(TestInputPath);

			var map = new Map(SeqModule.ToList(input));

			Assert.AreEqual(7, map.CountTrees(0, 0, 3, 1));
		}

		[Test]
		[Explicit]
		public void Real1()
		{
			var input = File.ReadAllLines(InputPath);

			var map = new Map(SeqModule.ToList(input));

			Assert.AreEqual(237, map.CountTrees(0, 0, 3, 1));

			var prodOfSlops = map.CountTrees(0, 0, 1, 1)
				* map.CountTrees(0, 0, 3, 1)
				* map.CountTrees(0, 0, 5, 1)
				* map.CountTrees(0, 0, 7, 1)
				* map.CountTrees(0, 0, 1, 2);

			Assert.AreEqual(2106818610, prodOfSlops);

		}
	}
}
