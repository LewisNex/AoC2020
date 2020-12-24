using AoC2020.Code;
using Microsoft.FSharp.Collections;
using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static AoC2020.Code.Types;
using static AoC2020.Code.Parsers;
using static AoC2020.Code.Errors;
using System;

namespace AoC2020.Tests
{
	internal class Day8
	{
		private const string TestInputPath = @"C:\Users\Lewis\source\repos\AoC2020\AoC2020.Tests\Inputs\Day8_test1.txt";
		private const string InputPath = @"C:\Users\Lewis\source\repos\AoC2020\AoC2020.Tests\Inputs\Day8_1.txt";


		[TestCase("nop +1", 1)]
		[TestCase("nop -1", -1)]
		[TestCase("nop +0", 0)]
		[TestCase("nop +1000", 1000)]
		public void TestParseNop(string input, int value)
		{
			var parsed = ParseInstruction(input) as Operation.NOP;
			Assert.AreEqual(value, parsed.Item);
		}

		[TestCase("acc +1", 1)]
		[TestCase("acc -1", -1)]
		[TestCase("acc +0", 0)]
		[TestCase("acc +1000", 1000)]
		public void TestParseAcc(string input, int value)
		{
			var parsed = ParseInstruction(input) as Operation.ACC;
			Assert.AreEqual(value, parsed.Item);
		}

		[TestCase("jmp +1", 1)]
		[TestCase("jmp -1", -1)]
		[TestCase("jmp +0", 0)]
		[TestCase("jmp +1000", 1000)]
		public void TestParseJmp(string input, int value)
		{
			var parsed = ParseInstruction(input) as Operation.JMP;
			Assert.AreEqual(value, parsed.Item);
		}


		[Test]
		public void Test1()
		{
			var input = File.ReadAllLines(TestInputPath);

			var computer = Parsers.BuildProgram(input);

			try
			{
				while (true)
				{
					computer.Tick();
				}
			}
			catch
			{
				Assert.AreEqual(5, computer.Accumulator);
			}
		}

		[Test]
		public void Real1()
		{
			var input = File.ReadAllLines(InputPath);

			var computer = Parsers.BuildProgram(input);

			try
			{
				while (true)
				{
					computer.Tick();
				}
			}
			catch (InfiniteLoopException e)
			{
				Assert.AreEqual(1553, e.Data1);
			}
		}

		[Test]
		public void Real2()
		{
			var input = File.ReadAllLines(InputPath);

			try
			{
				for (int i = 0; i < input.Length; i++)
				{
					var computer = Parsers.BuildProgram(input);
					var opp = computer.GetOperationAtPointer(i);
					Operation newOpp;
					if (opp is Operation.NOP nop)
					{
						newOpp = Operation.NewJMP(nop.Item);
						computer.SetOperationAtPointer(newOpp, i);
					}
					if (opp is Operation.JMP jmp)
					{
						newOpp = Operation.NewNOP(jmp.Item);
						computer.SetOperationAtPointer(newOpp, i);
					}
					if (opp is Operation.ACC)
					{
						continue;
					}

					try
					{
						while (true)
						{
							computer.Tick();
						}
					}
					catch (InfiniteLoopException) { }
				}
			} catch (OutOfRangeException e)
			{
				Assert.AreEqual(1877, e.Data1);
			}
		}
	}
}
