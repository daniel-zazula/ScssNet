using Combinatorics.Collections;
using ScssNet.Structures;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class CompositeSelectorTests : SelectorParserTestsBase
{
	internal static IEnumerable<object[]> CompositeSelectorParams => BuildSelectorPermutations();

	[TestMethod]
	[DynamicData(nameof(CompositeSelectorParams))]
	public void ShouldParseCompositeSelectors(string[] selectors)
	{
		var compositeSelectorSource = string.Concat(selectors);

		if(selectors[0] == Selectors.UniversalSelector)
		{
			var universalSelector = ShouldParseSelector<UniversalSelector>(compositeSelectorSource);
			TestQualifier(selectors, universalSelector, 1);
		}
		else
		{
			var tagSelector = ShouldParseSelector<TagSelector>(compositeSelectorSource);
			TestQualifier(selectors, tagSelector, 1);
		}
	}

	private static IEnumerable<object[]> BuildSelectorPermutations()
	{
		var mainSelectors = new string[] { Selectors.UniversalSelector, Selectors.TagSelector };
		var qualifiers = new string[] { Selectors.IdSelector, Selectors.ClassSelector, Selectors.AttributeSelector };
		var permutations = new Permutations<string>(qualifiers);

		foreach(var mainSelector in mainSelectors)
		{
			foreach(var permutation in permutations)
			{
				var selectors = permutation.Prepend(mainSelector).ToArray();
				yield return [selectors];
			}
		}
	}

	private static void TestQualifier(string[] qualifiers, ICompositeSelector selector, int index)
	{
		var qualifierSource = qualifiers[index];
		var qualifier = selector.Qualifier;

		qualifier.ShouldNotBeNull();

		qualifier.Assert(qualifierSource);

		index++;
		if (index < qualifiers.Length)
		{
			TestQualifier(qualifiers, qualifier, index);
		}
	}
}
