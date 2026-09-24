using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using ScssNet.Test.ElementCreation;
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
		var rule = Rule.Create();

		ShouldWriteRule(rule, RegularRuleExpected);
	}

	[TestMethod]
	public void ShouldGenerateImportantRule()
	{
		var property = IdentifierToken.Create("prop");
		var colon = SymbolToken.Create(Symbol.Colon, predecessor: property);
		var value = IdentifierToken.Create("val", predecessor: colon);
		var exclamation = SymbolToken.Create(Symbol.Exclamation, predecessor: value);
		var important = ValueKeywordToken.Create(ValueKeyword.Important, predecessor: exclamation);
		var semiColon = SymbolToken.Create(Symbol.SemiColon, predecessor: important);

		var rule = new Rule(property, colon, value, new ImportantValue(exclamation, important), semiColon);

		ShouldWriteRule(rule, "prop:val!important;");
	}

	private static void ShouldWriteRule(Rule rule, string expected)
	{
		var provider = BuildServiceProvider();
		var ruleGenerator = provider.GetRequiredService<RuleGenerator>();
		var writer = provider.GetRequiredService<CssWriter>();
		ruleGenerator.Generate(rule, writer);

		provider.GetStringWriter().ShouldContain(expected, StringCompareShould.IgnoreCase);
	}
}
