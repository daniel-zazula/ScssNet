using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class ValueParserTests : ParserTestBase
{
	[TestMethod]
	[DataRow("1cm")]
	[DataRow("2mm")]
	[DataRow("2Q")]
	[DataRow("3in")]
	[DataRow("4pc")]
	[DataRow("5pt")]
	[DataRow("6px")]
	[DataRow("7em")]
	[DataRow("8rem")]
	[DataRow("9%")]
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

	[TestMethod]
	[DataRow("red")]
	[DataRow("flex-start")]
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
}
