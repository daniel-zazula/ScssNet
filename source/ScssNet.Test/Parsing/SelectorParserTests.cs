using ScssNet.Structures;

namespace ScssNet.Test.Parsing;

[TestClass]
public class SelectorParserTests : SelectorParserTestsBase
{
	[TestMethod]
	public void ShouldParseTagSelector()
	{
		var tagSelector = ShouldParseSelector<TagSelector>(Selectors.TagSelector);
		tagSelector.AssertIdentifierText();
	}

	[TestMethod]
	public void ShouldParseIdSelector()
	{
		var idSelector = ShouldParseSelector<IdSelector>(Selectors.IdSelector);
		idSelector.AssertIdentifierValue();
	}

	[TestMethod]
	public void ShouldParseClassSelector()
	{
		var classSelector = ShouldParseSelector<ClassSelector>(Selectors.ClassSelector);
		classSelector.AssertIdentifierText();
	}

	[TestMethod]
	public void ShouldParseAttributeSelector()
	{
		var attributeSelector = ShouldParseSelector<AttributeSelector>(Selectors.AttributeSelector);
		attributeSelector.AssertAttributeText();
	}
}
