using System;
using ScssNet.Structures;
using Shouldly;

namespace ScssNet.Test.Parsing;

public static class SelectorTestExtensions
{
	public static void Assert(this ISelector selector, string source)
	{
		switch(source)
		{
			case Selectors.UniversalSelector:
				selector.ShouldBeOfType<UniversalSelector>();
				break;

			case Selectors.TagSelector:
				var tagSelector = selector.ShouldBeOfType<TagSelector>();
				tagSelector.AssertTagText();
				break;

			case Selectors.IdSelector:
				var idSelector = selector.ShouldBeOfType<IdSelector>();
				idSelector.AssertIdText();
				break;

			case Selectors.ClassSelector:
				var classSelector = selector.ShouldBeOfType<ClassSelector>();
				classSelector.AssertClassText();
				break;

			case Selectors.AttributeSelector:
				var attributeSelector = selector.ShouldBeOfType<AttributeSelector>();
				attributeSelector.AssertAttributeName();
				break;

			default:
				throw new NotImplementedException($"Unknown selector source: {source}");
		}
	}

	public static void AssertTagText(this TagSelector selector)
	{
		selector.Identifier.Text.ShouldBe(Selectors.TagSelector);
	}

	public static void AssertIdText(this IdSelector selector)
	{
		selector.Identifier.Value.ShouldBe(Selectors.IdSelector);
	}

	public static void AssertClassText(this ClassSelector selector)
	{
		selector.Identifier.Text.ShouldBe("my-class");
	}

	public static void AssertAttributeName(this AttributeSelector selector)
	{
		selector.Attribute.Text.ShouldBe(Selectors.AttributeName);
	}

	public static void AssertAttributeValue(this AttributeSelector selector)
	{
		selector.Value.ShouldNotBeNull();
		selector.Value.Text.ShouldBe(Selectors.AttributeValue);
	}
}
