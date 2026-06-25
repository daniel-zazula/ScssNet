using System;
using ScssNet.Structures;
using Shouldly;

namespace ScssNet.Test.Parsing;

internal static class SelectorTestExtensions
{
	public static void AssertText(this ISelector selector, string source)
	{
		switch(source)
		{
			case TestSelectors.UniversalSelector:
				selector.ShouldBeOfType<UniversalSelector>();
				break;

			case TestSelectors.TagSelector:
				var tagSelector = selector.ShouldBeOfType<TagSelector>();
				tagSelector.AssertIdentifierText();
				break;

			case TestSelectors.IdSelector:
				var idSelector = selector.ShouldBeOfType<IdSelector>();
				idSelector.AssertIdentifierValue();
				break;

			case TestSelectors.ClassSelector:
				var classSelector = selector.ShouldBeOfType<ClassSelector>();
				classSelector.AssertIdentifierText();
				break;

			case TestSelectors.PseudoClassSelector:
				var pseudoClassSelector = selector.ShouldBeOfType<PseudoClassSelector>();
				pseudoClassSelector.AssertIdentifierText();
				break;

			case TestSelectors.PseudoElementSelector:
				var pseudoElementSelector = selector.ShouldBeOfType<PseudoElementSelector>();
				pseudoElementSelector.AssertIdentifierText();
				break;

			case TestSelectors.AttributeSelector:
				var attributeSelector = selector.ShouldBeOfType<AttributeSelector>();
				attributeSelector.AssertAttributeText();
				break;

			default:
				throw new NotImplementedException($"Unknown selector source: {source}");
		}
	}

	public static void AssertIdentifierText(this TagSelector selector, string? text = null)
	{
		text = text ?? TestSelectors.TagSelector;
		selector.Identifier.Text.ShouldBe(text);
	}

	public static void AssertIdentifierValue(this IdSelector selector, string text = TestSelectors.IdSelector)
	{
		selector.Identifier.Value.ShouldBe(text);
	}

	public static void AssertIdentifierText(this ClassSelector selector, string? text = null)
	{
		text = text ?? TestSelectors.ClassSelector.Substring(1);
		selector.Identifier.Text.ShouldBe(text);
	}

	public static void AssertIdentifierText(this PseudoClassSelector selector, string? text = null)
	{
		text = text ?? TestSelectors.PseudoClassSelector.Substring(1);
		selector.Identifier.Text.ShouldBe(text);
	}

	public static void AssertIdentifierText(this PseudoElementSelector selector, string? text = null)
	{
		text = text ?? TestSelectors.PseudoElementSelector.Substring(2);
		selector.Identifier.Text.ShouldBe(text);
	}

	public static void AssertAttributeText(this AttributeSelector selector)
	{
		selector.Attribute.Text.ShouldBe(TestSelectors.AttributeName);
	}

	public static void AssertValueText(this AttributeSelector selector)
	{
		selector.Value.ShouldNotBeNull();
		selector.Value.Text.ShouldBe(TestSelectors.AttributeValue);
	}
}
