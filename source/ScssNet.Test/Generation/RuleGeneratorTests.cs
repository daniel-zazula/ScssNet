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
	internal const string RegularRuleExpected = "prop:val;";

	[TestMethod]
	public void ShouldGenerateRegularRule()
	{
		var rule = CreateRegularRule();

		ShouldWriteRule(rule, RegularRuleExpected);
	}

	[TestMethod]
	public void ShouldGenerateImportantRule()
	{
		var property = CreateIdentifierToken("prop");
		var colon = CreateSymbolToken(Symbol.Colon, predecessor: property);
		var value = CreateIdentifierToken("val", predecessor: colon);
		var exclamation = CreateSymbolToken(Symbol.Exclamation, predecessor: value);
		var important = CreateValueKeywordToken(ValueKeyword.Important, predecessor: exclamation);
		var semiColon = CreateSymbolToken(Symbol.SemiColon, predecessor: important);

		var rule = new Rule(property, colon, value, new ImportantValue(exclamation, important), semiColon);

		ShouldWriteRule(rule, "prop:val!important;");
	}

	internal static Rule CreateRegularRule(ISourceElement? predecessor = null)
	{
		var property = CreateIdentifierToken("prop", predecessor: predecessor);
		var colon = CreateSymbolToken(Symbol.Colon, predecessor: property);
		var value = CreateIdentifierToken("val", predecessor: colon);
		var semiColon = CreateSymbolToken(Symbol.SemiColon, predecessor: value);

		return new Rule(property, colon, value, null, semiColon);
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
