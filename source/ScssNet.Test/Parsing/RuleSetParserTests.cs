using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class RuleSetParserTests : ParserTestBase
{
	[TestMethod]
	public void ShouldParseRuleSet()
	{
		var source = "div { color: blue; }";
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var ruleSetParser = provider.GetRequiredService<RuleSetParser>();

		var ruleSet = ruleSetParser.Parse(tokenReader);
		ruleSet.ShouldNotBeNull();
		ruleSet.SelectorList.Selectors.ShouldHaveSingleItem();
		ruleSet.RuleBlock.Rules.ShouldHaveSingleItem();

		ruleSet.Issues.ShouldBeEmpty();
		tokenReader.End.ShouldBeTrue();
	}
}
