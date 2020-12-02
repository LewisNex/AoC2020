using Microsoft.FSharp.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using static AoC2020.Code.Passwords;

namespace AoC2020.Tests
{
	public class PasswordTests
	{
		private const string InputPath = @"C:\Users\Lewis\source\repos\AoC2020\AoC2020.Tests\Inputs\Day2_1.txt";

		[Test]
		public void Test1()
		{
			var input = new[]
			{
				"1-3 a: abcde",
				"1-3 b: cdefg",
				"2-9 c: ccccccccc"
			};

			Assert.AreEqual(2, GetValidCountByPolicy(input, Policy.Count));
			Assert.AreEqual(1, GetValidCountByPolicy(input, Policy.Position));
		}

		[Test]
		public void Real1()
		{
			var input = File.ReadAllLines(InputPath);

			Assert.AreEqual(447, GetValidCountByPolicy(input, Policy.Count));
			Assert.AreEqual(249, GetValidCountByPolicy(input, Policy.Position));
		}

		private int GetValidCountByPolicy(IEnumerable<string> input, Policy policy)
		{
			return CountValid(SeqModule.ToList(input), policy);
		}
	}
}
