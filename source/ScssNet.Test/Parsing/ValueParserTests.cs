using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class ValueParserTests : ParserTestBase
{
	internal static IEnumerable<object[]> UnitValueParams => TestValues.Units.ToParams();

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

	internal static IEnumerable<object[]> IdentifierValueParams => TestValues.IdentifierValues.ToParams();

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

	internal static IEnumerable<object[]> StringValueParams => TestValues.StringValues.ToParams();

	[TestMethod]
	[DynamicData(nameof(StringValueParams))]
	public void ShouldParseStringValue(string valueString)
	{
		ShouldParseValue<StringToken>(valueString);
	}

	internal static IEnumerable<object[]> HexValueParams => TestValues.HexColorValues.ToParams();

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
