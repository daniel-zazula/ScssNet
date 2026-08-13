using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using ScssNet.Structures;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class AtMediaParserTests : ParserTestBase
{
	[TestMethod]
	public void ShouldParseAtMediaWithRuleSet()
	{
		var source = "@media screen { prop: val; }";
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<TokenReader>();
		var atRuleParser = provider.GetRequiredService<AtRuleParser>();

		var atRule = atRuleParser.Parse(tokenReader);
		atRule.ShouldNotBeNull();
		atRule.Issues.ShouldBeEmpty();
		tokenReader.End.ShouldBeTrue();

		var atMedia = atRule.ShouldBeOfType<AtMedia>();
		atMedia.MediaQuery.ShouldNotBeNull();
		atMedia.Block.ShouldNotBeNull();
		atMedia.Block.Rules.ShouldNotBeEmpty();
	}
}
