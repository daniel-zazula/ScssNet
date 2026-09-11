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
		var important = CreateValueKeywordToken(ValueKeyword.Important, predecessor: exclamation);
		var semiColon = SymbolToken.Create(Symbol.SemiColon, predecessor: important);

		var rule = new Rule(property, colon, value, new ImportantValue(exclamation, important), semiColon);

		ShouldWriteRule(rule, "prop:val!important;");
	}

	private static ValueKeywordToken CreateValueKeywordToken
	(
		ValueKeyword valueKeyword, string? value = null, ISourceElement? predecessor = null
	)
	{
		value = ValueKeywordToken.CheckOrGetValue(valueKeyword, value);
		var length = value.Length;
		var span = SourceSpan.Create(predecessor, length);

		return new ValueKeywordToken(valueKeyword, value, span, Separator.Empty, Separator.Empty);
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
