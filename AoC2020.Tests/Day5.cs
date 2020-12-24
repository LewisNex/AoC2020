using Microsoft.FSharp.Collections;
using Microsoft.FSharp.Core;
using NUnit.Framework;
using System.IO;
using System.Linq;
using static AoC2020.Code.Boarding;

namespace AoC2020.Tests
{
	internal class Day5
	{

		private const string InputPath = @"C:\Users\Lewis\source\repos\AoC2020\AoC2020.Tests\Inputs\Day5_1.txt";


		[Test]
		public void TestPartitionValue()
		{
			CompareTuple((0, 63), Partition(0, 127, Direction.Negative));
			CompareTuple((32, 63), Partition(0, 63, Direction.Positive));
			CompareTuple((32, 47), Partition(32, 63, Direction.Negative));
			CompareTuple((40, 47), Partition(32, 47, Direction.Positive));
			CompareTuple((44, 47), Partition(40, 47, Direction.Positive));
			CompareTuple((44, 45), Partition(44, 47, Direction.Negative));
		}

		[Test]
		public void TestSomePasses()
		{
			var p1 = new BoardingPass("FBFBBFFRLR");
			Assert.AreEqual(44, p1.Row);
			Assert.AreEqual(5, p1.Column);
			Assert.AreEqual(357, p1.SeatId);

			var p2 = new BoardingPass("BFFFBBFRRR");
			Assert.AreEqual(70, p2.Row);
			Assert.AreEqual(7, p2.Column);
			Assert.AreEqual(567, p2.SeatId);
		}

		[Test]
		public void Real()
		{
			var input = File.ReadLines(InputPath);

			var passes = input.Select(x => new BoardingPass(x));

			Assert.AreEqual(944, passes.Select(x => x.SeatId).Max());
			var missing = FindMissingSeat(SeqModule.ToList(passes));
			Assert.AreEqual(554, missing.Single());
		}

		private void CompareTuple((int, int)expected, System.Tuple<int, int> actual)
		{
			Assert.AreEqual(expected.Item1, actual.Item1);
			Assert.AreEqual(expected.Item2, actual.Item2);
		}
	}
}
