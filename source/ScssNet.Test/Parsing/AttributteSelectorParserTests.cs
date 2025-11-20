using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class AttributteSelectorParserTests: ParserTestBase
{
	[TestMethod]
	public void ShouldParseAttributeSelector()
	{
		var provider = BuildServiceProvider("[type=\"text\"]");

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var attributeSelectorParser = provider.GetRequiredService<AttributteSelectorParser>();

		var attributeSelector = attributeSelectorParser.Parse(tokenReader);
		attributeSelector.ShouldNotBeNull();
		attributeSelector!.Attribute.Text.ShouldBe("type");
		attributeSelector.Operator!.Symbol.ShouldBe(Symbol.Equals);
		attributeSelector.Value!.Text.ShouldBe("\"text\"");
		attributeSelector.Issues.ShouldBeEmpty();

		tokenReader.End.ShouldBeTrue();
	}
}
