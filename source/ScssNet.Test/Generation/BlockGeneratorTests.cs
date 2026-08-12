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

	internal static Block CreateBlock(int columnNumber = 1)
	{
		var openBrace = CreateSymbolToken(Symbol.OpenBrace, columnNumber: columnNumber);
		var rule = RuleGeneratorTests.CreateRegularRule(openBrace.End.ColumnNumber + 1);
		var closeBrace = CreateSymbolToken(Symbol.CloseBrace, columnNumber: rule.End.ColumnNumber + 1);
		return new Block(openBrace, [rule], closeBrace);
	}
}
