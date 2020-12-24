using Microsoft.FSharp.Core;
using NUnit.Framework;
using System.IO;
using System.Linq;
using static AoC2020.Code.Passports;

namespace AoC2020.Tests
{
	internal class Day4
	{
		const string TestInputPath = @"C:\Users\Lewis\source\repos\AoC2020\AoC2020.Tests\Inputs\Day4_test1.txt";
		const string InputPath = @"C:\Users\Lewis\source\repos\AoC2020\AoC2020.Tests\Inputs\Day4_1.txt";

		[Test]
		public void TestRegexMatchValue()
		{
			var passport = new WeakPassport("hcl:#ae17e1 iyr:2013");

			Assert.AreEqual(FSharpOption<string>.None, passport.BirthYear);

			Assert.AreEqual("#ae17e1", passport.HairColour.Value);
			Assert.AreEqual("2013", passport.IssueYear.Value);
		}

		[Test]
		public void TestParsePassport()
		{
			var passport = new WeakPassport(@"hcl:#ae17e1 iyr:2013
eyr:2024
ecl:brn pid:760753108 byr:1931
hgt:179cm");

			Assert.AreEqual(FSharpOption<string>.None, passport.CountryId);

			Assert.AreEqual("#ae17e1", passport.HairColour.Value);
			Assert.AreEqual("2013", passport.IssueYear.Value);
			Assert.AreEqual("2024", passport.ExpireYear.Value);
			Assert.AreEqual("brn", passport.EyeColour.Value);
			Assert.AreEqual("760753108", passport.PassportId.Value);
			Assert.AreEqual("1931", passport.BirthYear.Value);
			Assert.AreEqual("179cm", passport.Height.Value);
		}

		[Test]
		public void TestParseInputToPassports()
		{
			var input = File.ReadAllText(TestInputPath);

			var split = input.Split("\r\n\r\n");

			Assert.AreEqual(4, split.Length);
		}

		[Test]
		public void TestIsValid()
		{
			var input = File.ReadAllText(TestInputPath);

			var split = input.Split("\r\n\r\n");

			var passports = split.Select(x => new WeakPassport(x));

			Assert.AreEqual(2, passports.Count(x => x.IsValid));
		}

		[Test]
		public void Real()
		{
			var input = File.ReadAllText(InputPath);

			var split = input.Split("\r\n\r\n");

			var passports = split.Select(x => new WeakPassport(x));

			Assert.AreEqual(235, passports.Count(x => x.IsValid));
		}

		[Test]
		public void TestAllFake()
		{
			var input = @"eyr:1972 cid:100
hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926

iyr:2019
hcl:#602927 eyr:1967 hgt:170cm
ecl:grn pid:012533040 byr:1946

hcl:dab227 iyr:2012
ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277

hgt:59cm ecl:zzz
eyr:2038 hcl:74454a iyr:2023
pid:3556412378 byr:2007";
			var split = input.Split("\r\n\r\n");

			var passports = split.Select(x => new StrictPassport(x));

			Assert.IsFalse(passports.Any(x => x.IsValid));
		}

		[Test]
		public void TestAllReal()
		{
			var input = @"pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980
hcl:#623a2f

eyr:2029 ecl:blu cid:129 byr:1989
iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm

hcl:#888785
hgt:164cm byr:2001 iyr:2015 cid:88
pid:545766238 ecl:hzl
eyr:2022

iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719 ";
			var split = input.Split("\r\n\r\n");

			var passports = split.Select(x => new StrictPassport(x));

			Assert.IsTrue(passports.All(x => x.IsValid));
		}

		[Test]
		public void TestSomeEdgeCases()
		{
			Assert.IsFalse(new StrictPassport("pid:0123456789").IsPassportIdValid);
		}

		[Test]
		public void RealStrict()
		{
			var input = File.ReadAllText(InputPath);

			var split = input.Split("\r\n\r\n");

			var passports = split.Select(x => new StrictPassport(x));

			Assert.AreEqual(194, passports.Count(x => x.IsValid));
		}
	}
}
