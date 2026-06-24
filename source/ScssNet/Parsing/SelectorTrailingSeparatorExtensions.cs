using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal static class SelectorTrailingSeparatorExtensions
{
	public static bool HasTrailingSeparator(this ISelector selector)
	{
		return selector switch
		{
			UniversalSelector universalSelector
				=> HasTrailingSeparator(universalSelector.Qualifier, universalSelector.Asterisk),
			TagSelector tagSelector
				=> HasTrailingSeparator(tagSelector.Qualifier, tagSelector.Identifier),
			IdSelector idSelector
				=> HasTrailingSeparator(idSelector.Qualifier, idSelector.Identifier),
			ClassSelector classSelector
				=> HasTrailingSeparator(classSelector.Qualifier, classSelector.Identifier),
			PseudoClassSelector pseudoClassSelector
				=> HasTrailingSeparator(pseudoClassSelector.Qualifier, pseudoClassSelector.Identifier),
			PseudoElementSelector pseudoElementSelector
				=> HasTrailingSeparator(pseudoElementSelector.Qualifier, pseudoElementSelector.Identifier),
			AttributeSelector attributeSelector
				=> HasTrailingSeparator(attributeSelector.Qualifier, attributeSelector.CloseBracket),
			IComplexSelector complexSelector => InnerHasTrailingSeparator(complexSelector),
			_ => throw new NotImplementedException("Unknow Selector type")
		};
	}

	private static bool HasTrailingSeparator(ISelectorQualifier? qualifier, ISeparatedToken token)
	{
		return qualifier is not null
			? HasTrailingSeparator(qualifier)
			: token.HasTrailingSeparator();
	}

	private static bool InnerHasTrailingSeparator(IComplexSelector complexSelector)
	{
		return HasTrailingSeparator(complexSelector.Selector);
	}
}
