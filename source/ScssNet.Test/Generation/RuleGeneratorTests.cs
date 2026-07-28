using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Generation;

[TestClass]
public class RuleGeneratorTests: GeneratorTestBase
{
	[TestMethod]
	public void ShouldWriteRegularRule()
	{
		var prop = CreateIdentifierToken("prop");
		var colon = CreateSymbolToken(Symbol.Colon, columnNumber: prop.End.ColumnNumber + 1);
		var val = CreateIdentifierToken("val", columnNumber: colon.End.ColumnNumber + 1);
		var semiColon = CreateSymbolToken(Symbol.SemiColon, columnNumber: val.End.ColumnNumber + 1);

		var rule = new Rule(prop, colon, val, null, semiColon);

		ShouldWriteRule(rule, "prop:val;");
	}

	[TestMethod]
	public void ShouldWriteImportantRule()
	{
		var prop = CreateIdentifierToken("prop");
		var colon = CreateSymbolToken(Symbol.Colon, columnNumber: prop.End.ColumnNumber + 1);
		var val = CreateIdentifierToken("val", columnNumber: colon.End.ColumnNumber + 1);
		var exclamation = CreateSymbolToken(Symbol.Exclamation, columnNumber: val.End.ColumnNumber + 1);
		var important = CreateKeywordToken(Keyword.Important, columnNumber: exclamation.End.ColumnNumber + 1);
		var semiColon = CreateSymbolToken(Symbol.SemiColon, columnNumber: important.End.ColumnNumber + 1);

		var rule = new Rule(prop, colon, val, new ImportantValue(exclamation, important), semiColon);

		ShouldWriteRule(rule, "prop:val!important;");
	}

	private static void ShouldWriteRule(Rule rule, string expected)
	{
		var provider = BuildServiceProvider();
		var ruleGenerator = provider.GetRequiredService<RuleGenerator>();
		var writer = provider.GetRequiredService<CssWriter>();
		ruleGenerator.Generate(rule, writer);

		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe(expected, StringCompareShould.IgnoreCase);
	}
}
