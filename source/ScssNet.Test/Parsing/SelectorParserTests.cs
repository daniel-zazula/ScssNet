using ScssNet.Structures;

namespace ScssNet.Test.Parsing;

[TestClass]
public class SelectorParserTests : SelectorParserTestsBase
{
	[TestMethod]
	public void ShouldParseTagSelector()
	{
		var tagSelector = ShouldParseSelector<TagSelector>(Selectors.TagSelector);
		tagSelector.AssertTagText();
	}

	[TestMethod]
	public void ShouldParseIdSelector()
	{
		var idSelector = ShouldParseSelector<IdSelector>(Selectors.IdSelector);
		idSelector.AssertIdentifierText();
	}

	[TestMethod]
	public void ShouldParseClassSelector()
	{
		var classSelector = ShouldParseSelector<ClassSelector>(Selectors.ClassSelector);
		classSelector.AssertClassText();
	}

	[TestMethod]
	public void ShouldParseAttributeSelector()
	{
		var attributeSelector = ShouldParseSelector<AttributeSelector>(Selectors.AttributeSelector);
		attributeSelector.AssertAttributeName();
	}
}
