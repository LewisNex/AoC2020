using AoC2020.Code;
using Microsoft.FSharp.Collections;
using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static AoC2020.Code.Luggage;

namespace AoC2020.Tests
{
	internal class Day7
	{
		private const string TestInputPath = @"C:\Users\Lewis\source\repos\AoC2020\AoC2020.Tests\Inputs\Day7_test1.txt";
		private const string TestInputPath2 = @"C:\Users\Lewis\source\repos\AoC2020\AoC2020.Tests\Inputs\Day7_test2.txt";
		private const string InputPath = @"C:\Users\Lewis\source\repos\AoC2020\AoC2020.Tests\Inputs\Day7_1.txt";

		[Test]
		public void TestGetChildrenAndParents()
		{
			var rule = "light red bags contain 1 bright white bag, 2 muted yellow bags.";
			var result = GetParentAndChildrenFromRuleEmpty(rule);

			var children = result.Item1;
			var parents = result.Item2;

			Assert.AreEqual(1, children.Count);
			Assert.AreEqual(2, children["light red"].Count());
			Assert.IsTrue(children["light red"].Select(x => x.Item1).Contains("bright white"));
			Assert.IsTrue(children["light red"].Select(x => x.Item1).Contains("muted yellow"));


			Assert.AreEqual(2, parents.Count);
			Assert.AreEqual(1, parents["bright white"].Count());
			Assert.AreEqual(1, parents["muted yellow"].Count());
			Assert.IsTrue(parents["bright white"].Contains("light red"));
			Assert.IsTrue(parents["muted yellow"].Contains("light red"));

		}

		[Test]
		public void TestGetAllParents()
		{
			var input = File.ReadAllLines(TestInputPath);

			var result = ParentAndChildrenFromRules(SeqModule.ToList(input));

			var parents = result.Item2;

			var parentsOfGold = GetAllParents("shiny gold", parents);

			Assert.AreEqual(4, parentsOfGold.Count);
		}

		[Test]
		public void GetCountOfChildren()
		{
			var input = File.ReadAllLines(TestInputPath2);

			var result = ParentAndChildrenFromRules(SeqModule.ToList(input));

			var children = result.Item1;
			Assert.AreEqual(126, Luggage.GetCountOfChildren("shiny gold", children));

		}

		[Test]
		public void TestRegex()
		{
			var rule = "light red bags contain 1 bright white bag, 2 muted yellow bags.";
			var pattern = @"(?<key>\b[\d\w\s]+) bags contain(?<vals>( \d+ \b[\d\w\s]+ bag[s]?[, |.])+)";

			var match = Regex.Match(rule, pattern);
			Assert.AreEqual(true, match.Success);
		}


		[Test]
		public void Real1()
		{
			var input = File.ReadAllLines(InputPath);

			var result = ParentAndChildrenFromRules(SeqModule.ToList(input));

			var children = result.Item1;
			var parents = result.Item2;

			var parentsOfGold = GetAllParents("shiny gold", parents);

			Assert.AreEqual(326, parentsOfGold.Count);
			Assert.AreEqual(5635, Luggage.GetCountOfChildren("shiny gold", children));
		}
	}
}
