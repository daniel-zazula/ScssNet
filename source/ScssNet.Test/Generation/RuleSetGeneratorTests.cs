using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Generation;

[TestClass]
public class RuleSetGeneratorTests: GeneratorTestBase
{
	[TestMethod]
	public void ShouldGenerateRuleSet()
	{
		var block = CreateRuleSet();

		var provider = BuildServiceProvider();
		var ruleSetGenerator = provider.GetRequiredService<RuleSetGenerator>();
		var writer = provider.GetRequiredService<CssWriter>();
		ruleSetGenerator.Generate(block, writer);

		AssertRuleSet(provider);
	}

	internal static RuleSet CreateRuleSet()
	{
		var selector = new TagSelector(CreateIdentifierToken("p"), null);
		var selectors = new SelectorList([new SelectorListItem(selector, null)]);
		var openBrace = CreateSymbolToken(Symbol.OpenBrace, columnNumber: selector.End.ColumnNumber + 1);
		var rule = RuleGeneratorTests.CreateRegularRule(openBrace.End.ColumnNumber + 1);
		var closeBrace = CreateSymbolToken(Symbol.CloseBrace, columnNumber: rule.End.ColumnNumber + 1);
		return new RuleSet(selectors, openBrace, [rule], closeBrace);
	}

	internal static void AssertRuleSet(ServiceProvider provider)
	{
		var expectedBlock = "p{" + RuleGeneratorTests.RegularRuleExpected + "}";
		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe(expectedBlock);
	}
}
