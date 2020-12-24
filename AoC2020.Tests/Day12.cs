using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using static AoC2020.Code.Navigation;


namespace AoC2020.Tests
{
	class Day12
	{
		private const string TestInputPath = @"C:\Users\Lewis\source\repos\AoC2020\AoC2020.Tests\Inputs\Day12_test1.txt";
		private const string InputPath = @"C:\Users\Lewis\source\repos\AoC2020\AoC2020.Tests\Inputs\Day12_1.txt";


		[Test]
		public void TestParsing()
		{
			var x = ParseInstruction("N10");
			Assert.IsTrue(x.IsNORTH);
			Assert.AreEqual(10, (x as Instruction.NORTH).Item);
		}

		[Test]
		public void TestModulo()
		{
			Assert.AreEqual(270, TurnByDegrees(0, -90));
			Assert.AreEqual(90, TurnByDegrees(0, 90));
			Assert.AreEqual(180, TurnByDegrees(0, -180));
			Assert.AreEqual(180, TurnByDegrees(0, 180));
		}

		[TestCase(0, 1, 90, 1, 0)]
		[TestCase(1, 0, 90, 0, -1)]
		[TestCase(0, -1, 90, -1, 0)]
		[TestCase(-1, 0, 90, 0, 1)]
		public void TestRotateWaypoint(int x, int y, int a, int ex, int ey)
		{
			var result = RotateWaypoint(Tuple.Create(x, y), a);
			Assert.AreEqual(ex, result.Item1);
			Assert.AreEqual(ey, result.Item2);
		}

		[TestCase(TestInputPath, 286)]
		[TestCase(InputPath, 62434)]
		public void IntegrationTest(string path, int ans)
		{
			var input = File.ReadAllLines(path);
			var parsed = input.Select(x => ParseInstruction(x));

			var ship = new Ship(0,0,10,1);
			var result = MoveMany(ship, parsed);

			Assert.AreEqual(ans, result.ManhattanDistance);
		}
	}
}
