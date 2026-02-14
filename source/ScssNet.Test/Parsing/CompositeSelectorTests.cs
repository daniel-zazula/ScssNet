using System;
using Combinatorics.Collections;
using Microsoft.Extensions.DependencyInjection;
using ScssNet.Lexing;
using ScssNet.Parsing;
using ScssNet.SourceElements;
using Shouldly;

namespace ScssNet.Test.Parsing;

[TestClass]
public class CompositeSelectorTests : ParserTestBase
{
	private static readonly string[] CompositeSelectorQualifiers = ["#my-id", ".my-class", "[my-attr]"];
	internal static IEnumerable<object[]> CompositeSelectorQualifierParams => new Permutations<string>(CompositeSelectorQualifiers)
		.Select(p => new object[] { p.ToArray() });

	[DataTestMethod]
	[DynamicData(nameof(CompositeSelectorQualifierParams), DynamicDataSourceType.Property)]
	public void ShouldParseCompositeSelectors(string[] qualifiers)
	{
		qualifiers.Length.ShouldBe(CompositeSelectorQualifiers.Length);

		var compositeSelector = "div" + string.Concat(qualifiers);

		var provider = BuildServiceProvider(compositeSelector);

		var tokenReader = provider.GetRequiredService<ITokenReader>();
		var selectorParser = provider.GetRequiredService<SelectorParser>();

		var selector = selectorParser.Parse(tokenReader).ShouldNotBeNull();
		selector.Issues.ShouldBeEmpty();

		tokenReader.End.ShouldBeTrue();

		var tagSelector = selector.ShouldBeOfType<TagSelector>();
		tagSelector.ShouldNotBeNull();
		tagSelector.Identifier.Text.ShouldBe("div");

		//TODO: Recursively get the qualifiers
		TestQualifier(qualifiers, tagSelector, 0);
	}

	private static void TestQualifier(string[] qualifiers, ICompositeSelector selector, int index)
	{
		var qualifierSource = qualifiers[index];
		var qualifier = selector.Qualifier;

		qualifier.ShouldNotBeNull();

		switch(qualifierSource)
		{
			case "#my-id":
				var idSelector = qualifier.ShouldBeOfType<IdSelector>();
				idSelector.Identifier.Text.ShouldBe("my-id");
				break;

			case ".my-class":
				var classSelector = qualifier.ShouldBeOfType<ClassSelector>();
				classSelector.Identifier.Text.ShouldBe("my-class");
				break;

			case "[my-attr]":
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
