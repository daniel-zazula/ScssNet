using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using ScssNet.Test.ElementCreation;
using Shouldly;

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

		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe(ExpectedBlock);
	}
}
