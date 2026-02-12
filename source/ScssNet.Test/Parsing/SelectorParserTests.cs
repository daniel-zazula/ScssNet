using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using ScssNet.SourceElements;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class SelectorParserTests : ParserTestBase
{
	[TestMethod]
	public void ShouldParseTagSelector()
	{
		var tagSelector = ShouldParseSelector<TagSelector>("div");
		tagSelector.Identifier.Text.ShouldBe("div");
	}

	[TestMethod]
	public void ShouldParseIdSelector()
	{
		var idSelector = ShouldParseSelector<IdSelector>("#theId");
		idSelector.Identifier.Text.ShouldBe("theId");
	}

	[TestMethod]
	public void ShouldParseClassSelector()
	{
		var classSelector = ShouldParseSelector<ClassSelector>(".my-class");
		classSelector.Identifier.Text.ShouldBe("my-class");
	}

	[TestMethod]
	public void ShouldParseAttributeSelector()
	{
		var attributeSelector = ShouldParseSelector<AttributeSelector>("[href]");
		attributeSelector.Attribute.Text.ShouldBe("href");
	}

	private static T ShouldParseSelector<T>(string source)
	{
		var provider = BuildServiceProvider(source);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var selectorParser = provider.GetRequiredService<SelectorParser>();

		var selector = selectorParser.Parse(tokenReader).ShouldNotBeNull();

		selector.Issues.ShouldBeEmpty();
		tokenReader.End.ShouldBeTrue();

		return selector.ShouldBeOfType<T>();
	}
}
