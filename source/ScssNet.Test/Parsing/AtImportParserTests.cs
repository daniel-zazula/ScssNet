using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using ScssNet.Structures;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class AtImportParserTests : ParserTestBase
{
	const string importPath = "\"styles.css\"";

	[TestMethod]
	public void ShouldParseAtImportWithStringPath()
	{
		var path = ShouldParseAtImport(importPath);

		var stringPath = path.ShouldBeOfType<StringToken>();
		stringPath.Text.ShouldBe(importPath);
	}

	[TestMethod]
	public void ShouldParseAtImportWithFunctionPath()
	{
		const string functionString = $"url({importPath})";
		var path = ShouldParseAtImport(functionString);

		var functionPath = path.ShouldBeOfType<FunctionCall>();
		functionPath.Name.Text.ShouldBe("url");

		var arguments = functionPath.Arguments;
		arguments.ShouldNotBeNull();

		var stringPath = arguments.ShouldBeOfType<StringToken>();
		stringPath.Text.ShouldBe(importPath);
	}

	private static IValue ShouldParseAtImport(string importPath)
	{
		var source = $"@import {importPath};";
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<TokenReader>();
		var atRuleParser = provider.GetRequiredService<AtRuleParser>();

		var atRule = atRuleParser.Parse(tokenReader);
		atRule.ShouldNotBeNull();
		atRule.Issues.ShouldBeEmpty();
		tokenReader.End.ShouldBeTrue();

		var atImport = atRule.ShouldBeOfType<AtImport>();

		return atImport.Path;
	}
}
