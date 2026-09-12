using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using ScssNet.Test.ElementCreation;

namespace ScssNet.Test.Generation;

[TestClass]
public class RuleSetGeneratorTests: GeneratorTestBase
{
	[TestMethod]
	public void ShouldGenerateRuleSet()
	{
		var ruleSet = RuleSet.Create();

		var provider = BuildServiceProvider();
		var ruleSetGenerator = provider.GetRequiredService<RuleSetGenerator>();
		var writer = provider.GetRequiredService<CssWriter>();
		ruleSetGenerator.Generate(ruleSet, writer);

		AssertRuleSet(provider);
	}

	internal static void AssertRuleSet(ServiceProvider provider)
	{
		var expectedBlock = "p" + BlockGeneratorTests.ExpectedBlock;
		provider.GetStringWriter().ShouldContain(expectedBlock);
	}
}
