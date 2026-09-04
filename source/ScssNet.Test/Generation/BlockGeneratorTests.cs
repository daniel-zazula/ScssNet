using System.IO;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Generation;
using ScssNet.Structures;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Generation;

[TestClass]
public class BlockGeneratorTests: GeneratorTestBase
{
	internal const string ExpectedBlock = "{" + RuleGeneratorTests.RegularRuleExpected + "}";

	[TestMethod]
	public void ShouldWriteBraces()
	{
		var block = CreateBlock();

		var provider = BuildServiceProvider();
		var blockGenerator = provider.GetRequiredService<BlockGenerator>();
		var writer = provider.GetRequiredService<CssWriter>();
		blockGenerator.Generate(block, writer);

		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe(ExpectedBlock);
	}

	internal static Block CreateBlock(ISourceElement? predecessor = null)
	{
		var openBrace = CreateSymbolToken(Symbol.OpenBrace, predecessor: predecessor);
		var rule = RuleGeneratorTests.CreateRegularRule(predecessor: openBrace);
		var closeBrace = CreateSymbolToken(Symbol.CloseBrace, predecessor: rule);
		return new Block(openBrace, [rule], closeBrace);
	}
}
