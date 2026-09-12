using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using ScssNet.Test.ElementCreation;

namespace ScssNet.Test.Generation;

[TestClass]
public class BlockGeneratorTests: GeneratorTestBase
{
	internal const string ExpectedBlock = "{" + RuleGeneratorTests.RegularRuleExpected + "}";

	[TestMethod]
	public void ShouldWriteBraces()
	{
		var block = Block.Create();

		var provider = BuildServiceProvider();
		var blockGenerator = provider.GetRequiredService<BlockGenerator>();
		var writer = provider.GetRequiredService<CssWriter>();
		blockGenerator.Generate(block, writer);

		provider.GetStringWriter().ShouldContain(ExpectedBlock);
	}
}
