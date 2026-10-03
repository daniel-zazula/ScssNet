using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using ScssNet.Structures;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class AtImportParserTests : ParserTestBase
{
	private const string StringUrl = "\"styles.css\"";
	private const string FunctionUrl = $"url({StringUrl})";

	[TestMethod]
	[DataRow(StringUrl)]
	[DataRow(FunctionUrl)]
	public void ShouldParseAtImport(string urlString)
	{
		var source = $"@import {urlString};";
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<TokenReader>();
		var atRuleParser = provider.GetRequiredService<AtRuleParser>();

		var atRule = atRuleParser.Parse(tokenReader);
		atRule.ShouldNotBeNull();
		atRule.Issues.ShouldBeEmpty();

		tokenReader.End.ShouldBeTrue();

		var atImport = atRule.ShouldBeOfType<AtImport>();
		atImport.Url.AssertUrl(urlString);
	}
}
