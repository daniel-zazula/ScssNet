using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using ScssNet.Structures;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class AtCharsetParserTests : ParserTestBase
{
	[TestMethod]
	public void ShouldParseAtCharset()
	{
		const string charsetString = "\"UTF-8\"";
		var source = $"@charset {charsetString};";
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var atRuleParser = provider.GetRequiredService<AtRuleParser>();

		var atRule = atRuleParser.Parse(tokenReader);
		atRule.ShouldNotBeNull();

		var atCharset = atRule.ShouldBeOfType<AtCharset>();
		atCharset.CharsetName.Text.ShouldBe(charsetString);
		atCharset.Issues.ShouldBeEmpty();

		tokenReader.End.ShouldBeTrue();
	}
}
