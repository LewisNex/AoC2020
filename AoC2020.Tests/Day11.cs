using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using static AoC2020.Code.Seating;

namespace AoC2020.Tests
{
	class Day11
	{
		private const string TestInputPath1 = @"C:\Users\Lewis\source\repos\AoC2020\AoC2020.Tests\Inputs\Day11_test1.txt";
		private const string InputPath = @"C:\Users\Lewis\source\repos\AoC2020\AoC2020.Tests\Inputs\Day11_1.txt";

		[Test]
		public void Test1()
		{
			var input = File.ReadLines(TestInputPath1);

			var map = new Layout(input);
			var ticks = 0;

			while (map.HasChanged)
			{
				map.Tick();
				ticks++;
			}

			var result = map.OccupiedCount;

			Assert.AreEqual(37, result);
			Assert.AreEqual(6, ticks);
		}

		[Test]
		public void Test2()
		{
			var input = File.ReadLines(TestInputPath1);
			var map = ParseIntoMap(input);

			var room = ParseMapIntoRoom(map);

			var ticks = 0;

			while (room.Tick())
			{
				ticks++;
			}

			var result = room.OccupiedCount();

			Assert.AreEqual(26, result);

		}

		[Test]
		public void Real1()
		{
			var input = File.ReadLines(InputPath);
			var map = new Layout(input);
			var ticks = 0;

			while (map.HasChanged)
			{
				map.Tick();
				ticks++;
			}

			var result = map.OccupiedCount;

			Assert.AreEqual(37, result);
		}

		[Test]
		public void Real2()
		{
			var input = File.ReadLines(InputPath);
			var map = ParseIntoMap(input);

			var room = ParseMapIntoRoom(map);

			var ticks = 0;

			while (room.Tick())
			{
				ticks++;
			}

			var result = room.OccupiedCount();

			Assert.AreEqual(2131, result);
		}
	}
}
