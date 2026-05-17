using System;
using Combinatorics.Collections;
using ScssNet.Structures;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class ComplexSelectorParserTests : SelectorParserTestsBase
{
	const string tagText = "div";
	const string idText = "#my-id";
	const string classText = ".my-class";
	const string attributeText = "[attr=\"value\"]";

	private static readonly string[] Selectors = [tagText, idText, classText, attributeText];
	internal static IEnumerable<object[]> SelectorParams => BuildSelectorPermutations();

	[TestMethod]
	[DynamicData(nameof(SelectorParams))]
	public void ShouldParseDescendantSelector(string firstSelector, string secondSelector)
	{
		var descendant = ShouldParseSelector<DescendantSelector>(firstSelector + " " + secondSelector);
		AssertSelector(firstSelector, descendant.AscendantSelector);
		AssertSelector(secondSelector, descendant.Selector);
	}

	[TestMethod]
	[DynamicData(nameof(SelectorParams))]
	public void ShouldParseChildSelector(string firstSelector, string secondSelector)
	{
		var childSelector = ShouldParseSelector<ChildSelector>(firstSelector + " > " + secondSelector);
		AssertSelector(firstSelector, childSelector.ParentSelector);
		AssertSelector(secondSelector, childSelector.Selector);
	}

	[TestMethod]
	[DynamicData(nameof(SelectorParams))]
	public void ShouldParseSubsequentSiblingSelector(string firstSelector, string secondSelector)
	{
		var siblingSelector = ShouldParseSelector<SubsequentSiblingSelector>(firstSelector + " ~ " + secondSelector);
		AssertSelector(firstSelector, siblingSelector.PrecedingSiblingSelector);
		AssertSelector(secondSelector, siblingSelector.Selector);
	}

	[TestMethod]
	[DynamicData(nameof(SelectorParams))]
	public void ShouldParseNextSiblingSelector(string firstSelector, string secondSelector)
	{
		var siblingSelector = ShouldParseSelector<NextSiblingSelector>(firstSelector + " + " + secondSelector);
		AssertSelector(firstSelector, siblingSelector.PreviousSiblingSelector);
		AssertSelector(secondSelector, siblingSelector.Selector);
	}

	private static void AssertSelector(string source, ISelector selector)
	{
		switch (source)
		{
			case tagText:
				var tagSelector = selector.ShouldBeOfType<TagSelector>();
				tagSelector.Identifier.Text.ShouldBe(tagText);
				break;
			case idText:
				var idSelector = selector.ShouldBeOfType<IdSelector>();
				idSelector.Identifier.Text.ShouldBe("my-id");
				break;
			case classText:
				var classSelector = selector.ShouldBeOfType<ClassSelector>();
				classSelector.Identifier.Text.ShouldBe("my-class");
				break;
			case attributeText:
				var attributeSelector = selector.ShouldBeOfType<AttributeSelector>();
				attributeSelector.Attribute.Text.ShouldBe("attr");
				attributeSelector.Value?.Text.ShouldBe(@"""value""");
				break;
			default:
				throw new InvalidOperationException($"Unexpected selector: {source}");
		}
	}

	private static IEnumerable<object[]> BuildSelectorPermutations()
	{
		foreach (var firstSelector in Selectors)
		{
			foreach (var secondSelector in Selectors)
			{
				yield return new object[] { firstSelector, secondSelector };
			}
		}
	}
}
