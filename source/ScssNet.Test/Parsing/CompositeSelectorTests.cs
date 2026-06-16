using Combinatorics.Collections;
using ScssNet.Structures;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class CompositeSelectorTests : SelectorParserTestsBase
{
	private static readonly string[] CompositeSelectorQualifiers = [Selectors.IdSelector, Selectors.ClassSelector, Selectors.AttributeSelector];
	internal static IEnumerable<object[]> CompositeSelectorQualifierParams => BuildSelectorPermutations();

	[TestMethod]
	[DynamicData(nameof(CompositeSelectorQualifierParams))]
	public void ShouldParseCompositeSelectors(string[] qualifiers)
	{
		qualifiers.Length.ShouldBe(CompositeSelectorQualifiers.Length);

		var compositeSelectorSource = Selectors.TagSelector + string.Concat(qualifiers);

		var tagSelector = ShouldParseSelector<TagSelector>(compositeSelectorSource);

		TestQualifier(qualifiers, tagSelector, 0);
	}

	private static IEnumerable<object[]> BuildSelectorPermutations()
	{
		return new Permutations<string>(CompositeSelectorQualifiers)
			.Select(p => new object[] { p.ToArray() });
	}

	private static void TestQualifier(string[] qualifiers, ICompositeSelector selector, int index)
	{
		var qualifierSource = qualifiers[index];
		var qualifier = selector.Qualifier;

		qualifier.ShouldNotBeNull();

		qualifier.Assert(qualifierSource);

		index++;
		if (index < CompositeSelectorQualifiers.Length)
		{
			TestQualifier(qualifiers, qualifier, index);
		}
	}
}
