using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Parsing;

public class RuleParserTests : ParserTestBase
{
	[DataTestMethod]
	[DataRow(";")]
	[DataRow("")]
	public void ShouldParseIdSelectorInRule(string semiColon)
	{
		var source = $"-moz-color: red{semiColon}";
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var ruleParser = provider.GetRequiredService<RuleParser>();

		var rule = ruleParser.Parse(tokenReader);
		rule.ShouldNotBeNull();
		rule!.Property.Text.ShouldBe("-moz-color");
		
		var value = rule.Value.ShouldBeOfType<IdentifierToken>();
		value.Text.ShouldBe("red");

		if (semiColon == ";")
			rule.SemiColon.ShouldNotBeNull();
		else
			rule.SemiColon.ShouldBeNull();

		rule.Issues.ShouldBeEmpty();
		tokenReader.End.ShouldBeTrue();
	}
}
