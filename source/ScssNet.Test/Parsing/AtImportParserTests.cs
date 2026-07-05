using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using ScssNet.Structures;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class AtImportParserTests : ParserTestBase
{
	[TestMethod]
	public void ShouldParseAtImport()
	{
		const string importPath = "\"styles.css\"";
		var source = $"@import {importPath};";
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var atRuleParser = provider.GetRequiredService<AtRuleParser>();

		var atRule = atRuleParser.Parse(tokenReader);
		atRule.ShouldNotBeNull();

		var atImport = atRule.ShouldBeOfType<AtImport>();
		atImport.Path.Text.ShouldBe(importPath);
		atImport.Issues.ShouldBeEmpty();

		tokenReader.End.ShouldBeTrue();
	}
}
