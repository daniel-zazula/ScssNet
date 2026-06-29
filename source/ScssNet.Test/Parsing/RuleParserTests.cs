using System;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class RuleParserTests : ParserTestBase
{
	[TestMethod]
	[DataRow(";")]
	[DataRow("")]
	public void ShouldParseRule(string semiColon)
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

	[TestMethod]
	[DataRow("color: red !important;")]
	[DataRow("color: red !IMPORTANT;")]
	[DataRow("color: red !Important;")]
	[DataRow("color: red !important")]
	[DataRow("color: red  !important;")]
	public void ShouldParseRuleWithImportant(string source)
	{
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var ruleParser = provider.GetRequiredService<RuleParser>();

		var rule = ruleParser.Parse(tokenReader);
		rule.ShouldNotBeNull();
		rule!.Property.Text.ShouldBe("color");

		var value = rule.Value.ShouldBeOfType<IdentifierToken>();
		value.Text.ShouldBe("red");

		rule.Important.ShouldNotBeNull();
		rule.Important!.Exclamation.Symbol.ShouldBe(Symbol.Exclamation);
		rule.Important.Important.Text.ShouldBe("important", StringComparer.OrdinalIgnoreCase);

		rule.Issues.ShouldBeEmpty();
		tokenReader.End.ShouldBeTrue();
	}
}
