using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Parsing;

public static class Values
{
	public static readonly string[] Units = ["1cm", "2mm", "2Q", "3in", "4pc", "5pt", "6px", "7em", "8rem", "9%"];

	public static readonly string[] IdentifierValues = ["red", "flex-start"];

	public static readonly string[] StringValues = [@"""Times New Roman""", @"""Courier New"""];

	public static readonly string[] HexColorValues = ["#123", "#abc", "#DEF", "#1a2", "#a1b", "#123456", "#abcdef", "#FEDCBA", "#1a2b3c", "#a1b2c3"];

	public static readonly string[] AllValues = [.. Units, .. IdentifierValues, .. StringValues, .. HexColorValues];
}

[TestClass]
public class ValueParserTests : ParserTestBase
{
	internal static IEnumerable<object[]> UnitValueParams => Values.Units.ToParams();

	[TestMethod]
	[DynamicData(nameof(UnitValueParams))]
	public void ShouldParseUnitValue(string valueString)
	{
		var provider = BuildServiceProvider(valueString);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var valueParser = provider.GetRequiredService<ValueParser>();

		var value = valueParser.Parse(tokenReader);

		value.ShouldNotBeNull();
		value.ShouldBeOfType<UnitValueToken>();
		tokenReader.End.ShouldBeTrue();
	}

	internal static IEnumerable<object[]> IdentifierValueParams => Values.IdentifierValues.ToParams();

	[TestMethod]
	[DynamicData(nameof(IdentifierValueParams))]
	public void ShouldParseIdentifierValue(string valueString)
	{
		var provider = BuildServiceProvider(valueString);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var valueParser = provider.GetRequiredService<ValueParser>();

		var value = valueParser.Parse(tokenReader);

		value.ShouldNotBeNull();
		value.ShouldBeOfType<IdentifierToken>();
		tokenReader.End.ShouldBeTrue();
	}

	internal static IEnumerable<object[]> StringValueParams => Values.StringValues.ToParams();

	[TestMethod]
	[DynamicData(nameof(StringValueParams))]
	public void ShouldParseStringValue(string valueString)
	{
		ShouldParseValue<StringToken>(valueString);
	}

	internal static IEnumerable<object[]> HexValueParams => Values.HexColorValues.ToParams();

	[TestMethod]
	[DynamicData(nameof(HexValueParams))]
	public void ShouldParseHexValue(string valueString)
	{
		ShouldParseValue<HashValueToken>(valueString);
	}

	private static void ShouldParseValue<T>(string valueString) where T : IValueToken
	{
		var provider = BuildServiceProvider(valueString);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var valueParser = provider.GetRequiredService<ValueParser>();

		var value = valueParser.Parse(tokenReader);

		value.ShouldNotBeNull();
		value.ShouldBeOfType<T>();
		tokenReader.End.ShouldBeTrue();
	}
}
