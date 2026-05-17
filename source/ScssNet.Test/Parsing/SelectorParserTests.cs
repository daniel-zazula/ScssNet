using ScssNet.Structures;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class SelectorParserTests : SelectorParserTestsBase
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
}
