using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class BlockParserTests: ParserTestBase
{
	[TestMethod]
	public void ShouldParseEmptyBlock()
	{
		var source = "{}";
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var blockParser = provider.GetRequiredService<BlockParser>();

		var block = blockParser.Parse(tokenReader);
		block.ShouldNotBeNull();
		block!.Rules.ShouldBeEmpty();
		block.Issues.ShouldBeEmpty();
		tokenReader.End.ShouldBeTrue();
	}

	[DataTestMethod]
	[DataRow(";")]
	[DataRow("")]
	public void ShouldParseBlockWithSingleRule(string semicolon)
	{
		var source = $"{{ color: red{semicolon} }}";
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var blockParser = provider.GetRequiredService<BlockParser>();

		var block = blockParser.Parse(tokenReader);
		block.ShouldNotBeNull();
		block!.Rules.ShouldHaveSingleItem();
		block.Issues.ShouldBeEmpty();
		tokenReader.End.ShouldBeTrue();
	}

	[DataTestMethod]
	[DataRow(";")]
	[DataRow("")]
	public void ShouldParseBlockWithMultipleRules(string semicolon)
	{
		var source = $@"
		{{
			text-align: center;
			font-size: 10px{semicolon}
		}}";
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var blockParser = provider.GetRequiredService<BlockParser>();

		var block = blockParser.Parse(tokenReader);
		block.ShouldNotBeNull();
		block.Rules.Count. ShouldBe(2);
		block.Issues.ShouldBeEmpty();
		tokenReader.End.ShouldBeTrue();
	}
}
