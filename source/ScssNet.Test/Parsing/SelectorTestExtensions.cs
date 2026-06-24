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
			case Selectors.UniversalSelector:
				selector.ShouldBeOfType<UniversalSelector>();
				break;

			case Selectors.TagSelector:
				var tagSelector = selector.ShouldBeOfType<TagSelector>();
				tagSelector.AssertIdentifierText();
				break;

			case Selectors.IdSelector:
				var idSelector = selector.ShouldBeOfType<IdSelector>();
				idSelector.AssertIdentifierValue();
				break;

			case Selectors.ClassSelector:
				var classSelector = selector.ShouldBeOfType<ClassSelector>();
				classSelector.AssertIdentifierText();
				break;

			case Selectors.PseudoClassSelector:
				var pseudoClassSelector = selector.ShouldBeOfType<PseudoClassSelector>();
				pseudoClassSelector.AssertIdentifierText();
				break;

			case Selectors.PseudoElementSelector:
				var pseudoElementSelector = selector.ShouldBeOfType<PseudoElementSelector>();
				pseudoElementSelector.AssertIdentifierText();
				break;

			case Selectors.AttributeSelector:
				var attributeSelector = selector.ShouldBeOfType<AttributeSelector>();
				attributeSelector.AssertAttributeText();
				break;

			default:
				throw new NotImplementedException($"Unknown selector source: {source}");
		}
	}

	public static void AssertIdentifierText(this TagSelector selector, string? text = null)
	{
		text = text ?? Selectors.TagSelector;
		selector.Identifier.Text.ShouldBe(text);
	}

	public static void AssertIdentifierValue(this IdSelector selector, string text = Selectors.IdSelector)
	{
		selector.Identifier.Value.ShouldBe(text);
	}

	public static void AssertIdentifierText(this ClassSelector selector, string? text = null)
	{
		text = text ?? Selectors.ClassSelector.Substring(1);
		selector.Identifier.Text.ShouldBe(text);
	}

	public static void AssertIdentifierText(this PseudoClassSelector selector, string? text = null)
	{
		text = text ?? Selectors.PseudoClassSelector.Substring(1);
		selector.Identifier.Text.ShouldBe(text);
	}

	public static void AssertIdentifierText(this PseudoElementSelector selector, string? text = null)
	{
		text = text ?? Selectors.PseudoElementSelector.Substring(2);
		selector.Identifier.Text.ShouldBe(text);
	}

	public static void AssertAttributeText(this AttributeSelector selector)
	{
		selector.Attribute.Text.ShouldBe(Selectors.AttributeName);
	}

	public static void AssertValueText(this AttributeSelector selector)
	{
		selector.Value.ShouldNotBeNull();
		selector.Value.Text.ShouldBe(Selectors.AttributeValue);
	}
}
