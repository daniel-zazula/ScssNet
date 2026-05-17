using System;
using Combinatorics.Collections;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using ScssNet.Structures;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class CompositeSelectorTests : ParserTestBase
{
	const string tagText = "div";
	const string idText = "#my-id";
	const string classText = ".my-class";
	const string attributeText = "[my-attr]";

	private static readonly string[] CompositeSelectorQualifiers = [idText, classText, attributeText];
	internal static IEnumerable<object[]> CompositeSelectorQualifierParams => BuildSelectorPermutations();

	[TestMethod]
	[DynamicData(nameof(CompositeSelectorQualifierParams))]
	public void ShouldParseCompositeSelectors(string[] qualifiers)
	{
		qualifiers.Length.ShouldBe(CompositeSelectorQualifiers.Length);

		var compositeSelector = tagText + string.Concat(qualifiers);

		var provider = BuildServiceProvider(compositeSelector);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var selectorParser = provider.GetRequiredService<SelectorParser>();

		var selector = selectorParser.Parse(tokenReader).ShouldNotBeNull();
		selector.Issues.ShouldBeEmpty();

		tokenReader.End.ShouldBeTrue();

		var tagSelector = selector.ShouldBeOfType<TagSelector>();
		tagSelector.ShouldNotBeNull();
		tagSelector.Identifier.Text.ShouldBe(tagText);

		//TODO: Recursively get the qualifiers
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

		switch(qualifierSource)
		{
			case idText:
				var idSelector = qualifier.ShouldBeOfType<IdSelector>();
				idSelector.Identifier.Text.ShouldBe("my-id");
				break;

			case classText:
				var classSelector = qualifier.ShouldBeOfType<ClassSelector>();
				classSelector.Identifier.Text.ShouldBe("my-class");
				break;

			case attributeText:
				var attributeSelector = qualifier.ShouldBeOfType<AttributeSelector>();
				attributeSelector.Attribute.Text.ShouldBe("my-attr");
				break;

			default:
				throw new InvalidOperationException($"Unexpected qualifier source: {qualifierSource}");
		}

		index++;
		if (index < CompositeSelectorQualifiers.Length)
		{
			TestQualifier(qualifiers, qualifier, index);
		}
	}
}
