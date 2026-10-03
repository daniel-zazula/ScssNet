using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using ScssNet.Structures;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class AtNamespaceParserTests : ParserTestBase
{
	private const string XhtmlPath = "\"http://www.w3.org/1999/xhtml\"";
	private const string SvgPath = "\"http://www.w3.org/2000/svg\"";

	[TestMethod]
	[DataRow("", XhtmlPath)]
	[DataRow("svg", SvgPath)]
	[DataRow("", $"url({XhtmlPath})")]
	[DataRow("svg", $"url({SvgPath})")]
	public void ShouldParseAtNamespace(string prefixStr, string urlString)
	{
		var source = $"@namespace {prefixStr} {urlString};";
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<TokenReader>();
		var atRuleParser = provider.GetRequiredService<AtRuleParser>();

		var atRule = atRuleParser.Parse(tokenReader);
		atRule.ShouldNotBeNull();
		atRule.Issues.ShouldBeEmpty();

		tokenReader.End.ShouldBeTrue();

		var atNamespace = atRule.ShouldBeOfType<AtNamespace>();
		if (prefixStr.Length > 0)
		{
			var prefix = atNamespace.Prefix.ShouldNotBeNull();
			prefix.Text.ShouldBe(prefixStr);
		}

		atNamespace.Url.AssertUrl(urlString);
	}
}
