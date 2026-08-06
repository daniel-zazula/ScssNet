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
	[TestMethod]
	public void ShouldWriteBraces()
	{
		var openBrace = CreateSymbolToken(Symbol.OpenBrace, columnNumber: 1);
		var rule = RuleGeneratorTests.CreateRegularRule(openBrace.End.ColumnNumber + 1);
		var closeBrace = CreateSymbolToken(Symbol.CloseBrace, columnNumber: rule.End.ColumnNumber + 1);
		var block = new Block(openBrace, [rule], closeBrace);

		var provider = BuildServiceProvider();
		var blockGenerator = provider.GetRequiredService<BlockGenerator>();
		var writer = provider.GetRequiredService<CssWriter>();
		blockGenerator.Generate(block, writer);

		var expectedBlock = "{" + RuleGeneratorTests.RegularRuleExpected + "}";
		var stringWriter = provider.GetRequiredService<StringWriter>();
		stringWriter.ToString().ShouldBe(expectedBlock);
	}
}
