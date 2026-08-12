using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using Shouldly;

namespace ScssNet.Test.Generation;

[TestClass]
public class RuleSetGeneratorTests: GeneratorTestBase
{
	[TestMethod]
	public void ShouldGenerateRuleSet()
	{
		var ruleSet = CreateRuleSet();

		var provider = BuildServiceProvider();
		var ruleSetGenerator = provider.GetRequiredService<RuleSetGenerator>();
		var writer = provider.GetRequiredService<CssWriter>();
		ruleSetGenerator.Generate(ruleSet, writer);

		AssertRuleSet(provider);
	}

	internal static RuleSet CreateRuleSet()
	{
		var selector = new TagSelector(CreateIdentifierToken("p"), null);
		var selectors = new SelectorList([new SelectorListItem(selector, null)]);
		var block = BlockGeneratorTests.CreateBlock(selector.End.ColumnNumber + 1);
		return new RuleSet(selectors, block);
	}

	internal static void AssertRuleSet(ServiceProvider provider)
	{
		var expectedBlock = "p" + BlockGeneratorTests.ExpectedBlock;
		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe(expectedBlock);
	}
}
