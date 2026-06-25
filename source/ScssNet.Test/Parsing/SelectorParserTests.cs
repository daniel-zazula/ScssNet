using ScssNet.Structures;

namespace ScssNet.Test.Parsing;

[TestClass]
public class SelectorParserTests : SelectorParserTestsBase
{
	[TestMethod]
	public void ShouldParseTagSelector()
	{
		var tagSelector = ShouldParseSelector<TagSelector>(TestSelectors.TagSelector);
		tagSelector.AssertIdentifierText();
	}

	[TestMethod]
	public void ShouldParseIdSelector()
	{
		var idSelector = ShouldParseSelector<IdSelector>(TestSelectors.IdSelector);
		idSelector.AssertIdentifierValue();
	}

	[TestMethod]
	public void ShouldParseClassSelector()
	{
		var classSelector = ShouldParseSelector<ClassSelector>(TestSelectors.ClassSelector);
		classSelector.AssertIdentifierText();
	}

	[TestMethod]
	public void ShouldParseAttributeSelector()
	{
		var attributeSelector = ShouldParseSelector<AttributeSelector>(TestSelectors.AttributeSelector);
		attributeSelector.AssertAttributeText();
	}
}
