using System;
using Combinatorics.Collections;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using ScssNet.Structures;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class ValueListParserTests : ParserTestBase
{
	[TestMethod]
	[DynamicData(nameof(BuildValuePermutations))]
	public void ShouldParseValueListSeparatedBySpace(string[] values)
	{
		ShouldParseValueList(values, " ", AssertComma);

		static void AssertComma(SymbolToken? comma, bool lastItem)
		{
			comma.ShouldBeNull();
		}
	}

	[TestMethod]
	[DynamicData(nameof(BuildValuePermutations))]
	public void ShouldParseValueListSeparatedByComma(string[] values)
	{
		ShouldParseValueList(values, ", ", AssertComma);

		static void AssertComma(SymbolToken? comma, bool isLastItem)
		{
			if(!isLastItem)
				comma.ShouldNotBeNull();
			else
				comma.ShouldBeNull();
		}
	}

	public void ShouldParseValueList(string[] values, string separator, Action<SymbolToken?, bool> assertComma)
	{
		var source = string.Join(separator, values);
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var valueParser = provider.GetRequiredService<ValueParser>();

		var value = valueParser.Parse(tokenReader);
		value.ShouldNotBeNull();
		var list = value.ShouldBeOfType<ValueList>();

		list.Items.Count.ShouldBe(values.Length);

		for(var i = 0; i < values.Length; i++)
		{
			var item = list.Items[i];
			var isLastItem = i == values.Length - 1;
			assertComma(item.Comma, isLastItem);
			AssertValueTokenType(item.Value, values[i]);
		}

		list.Issues.ShouldBeEmpty();
		tokenReader.End.ShouldBeTrue();
	}

	private static void AssertValueTokenType(IValue value, string source)
	{
		source = source.Trim();
		if(source.StartsWith('"'))
		{
			value.ShouldBeOfType<StringToken>();
			return;
		}

		if(source.StartsWith('#'))
		{
			value.ShouldBeOfType<HashValueToken>();
			return;
		}

		if(char.IsDigit(source[0]))
		{
			value.ShouldBeOfType<UnitValueToken>();
			return;
		}

		value.ShouldBeOfType<IdentifierToken>();
	}

	private static IEnumerable<object[]> BuildValuePermutations()
	{
		// limit size to 3 to keep test matrix reasonable
		return new Combinations<string>(TestValues.OneOfEach, 3, GenerateOption.WithRepetition)
			.Select(p => new object[] { p.ToArray() });
	}
}
