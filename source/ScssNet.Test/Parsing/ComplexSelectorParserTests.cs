using ScssNet.Structures;

namespace ScssNet.Test.Parsing;

[TestClass]
public class ComplexSelectorParserTests : SelectorParserTestsBase
{
	private static readonly string[] SelectorList = [Selectors.TagSelector, Selectors.IdSelector, Selectors.ClassSelector, Selectors.AttributeSelector];
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
		selector.Assert(source);
	}

	private static IEnumerable<object[]> BuildSelectorPermutations()
	{
		foreach (var firstSelector in SelectorList)
		{
			foreach (var secondSelector in SelectorList)
			{
				yield return new object[] { firstSelector, secondSelector };
			}
		}
	}
}
