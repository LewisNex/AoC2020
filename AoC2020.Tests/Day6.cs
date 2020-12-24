using Microsoft.FSharp.Collections;
using NUnit.Framework;
using System.IO;
using System.Linq;

using static AoC2020.Code.Customs;

namespace AoC2020.Tests
{
	internal class Day6
	{
		private const string TestInputPath = @"C:\Users\Lewis\source\repos\AoC2020\AoC2020.Tests\Inputs\Day6_test1.txt";
		private const string InputPath = @"C:\Users\Lewis\source\repos\AoC2020\AoC2020.Tests\Inputs\Day6_1.txt";

		[Test]
		public void TestCountIndividualQuestions()
		{
			Assert.AreEqual(3, new Individual("abc").Count);
			Assert.AreEqual(3, new Individual("abcccc").Count);
			Assert.AreEqual(3, new Individual("accbccabcc").Count);
		}

		[Test]
		public void TestCountGroupQuestions()
		{
			Assert.AreEqual(3, new UnionGroup(SeqModule.ToList(new[] { "abc" })).Count);
			Assert.AreEqual(3, new UnionGroup(SeqModule.ToList(new[] { "a", "b", "c" })).Count);
			Assert.AreEqual(3, new UnionGroup(SeqModule.ToList(new[] { "ab", "ac" })).Count);
			Assert.AreEqual(1, new UnionGroup(SeqModule.ToList(new[] { "a", "a", "a" })).Count);
			Assert.AreEqual(1, new UnionGroup(SeqModule.ToList(new[] { "b" })).Count);
		}

		[Test]
		public void TestCountIntersectionGroups()
		{
			Assert.AreEqual(3, new IntersectionGroup(SeqModule.ToList(new[] { "abc" })).Count);
			Assert.AreEqual(0, new IntersectionGroup(SeqModule.ToList(new[] { "a", "b", "c" })).Count);
			Assert.AreEqual(1, new IntersectionGroup(SeqModule.ToList(new[] { "ab", "ac" })).Count);
			Assert.AreEqual(1, new IntersectionGroup(SeqModule.ToList(new[] { "a", "a", "a" })).Count);
			Assert.AreEqual(1, new IntersectionGroup(SeqModule.ToList(new[] { "b" })).Count);
		}

		[Test]
		public void TestFromFile()
		{
			var input = File.ReadAllText(TestInputPath);
			var groups = input
				.Split("\r\n\r\n")
				.Select(x => x.Split("\r\n"))
				.Select(x => new UnionGroup(SeqModule.ToList(x)));

			Assert.AreEqual(11, groups.Select(x => x.Count).Sum());

			var intersectGroups = input
				.Split("\r\n\r\n")
				.Select(x => x.Split("\r\n"))
				.Select(x => new IntersectionGroup(SeqModule.ToList(x)));

			Assert.AreEqual(6, intersectGroups.Select(x => x.Count).Sum());
		}

		[Test]
		public void Real1()
		{
			var input = File.ReadAllText(InputPath);
			var groups = input
				.Split("\r\n\r\n")
				.Select(x => x.Split("\r\n"))
				.Select(x => new UnionGroup(SeqModule.ToList(x)));

			Assert.AreEqual(6297, groups.Select(x => x.Count).Sum());
		}

		[Test]
		public void Real2()
		{
			// Note: Sensitive to new line at end of file
			var input = File.ReadAllText(InputPath);
			var groups = input
				.Split("\r\n\r\n")
				.Select(x => x.Split("\r\n"))
				.Select(x => new IntersectionGroup(SeqModule.ToList(x)));

			Assert.AreEqual(3158, groups.Select(x => x.Count).Sum());
		}

	}
}
