using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal static class SelectorTrailingSeparatorExtensions
{
	public static bool HasTrailingSeparator(this ISelector selector)
	{
		return selector switch
		{
			AttributeSelector attributeSelector => InnerHasTrailingSeparator(attributeSelector),
			ClassSelector classSelector => InnerHasTrailingSeparator(classSelector),
			IdSelector idSelector => InnerHasTrailingSeparator(idSelector),
			TagSelector tagSelector => InnerHasTrailingSeparator(tagSelector),
			UniversalSelector universalSelector => InnerHasTrailingSeparator(universalSelector),
			IComplexSelector complexSelector => InnerHasTrailingSeparator(complexSelector),
			_ => throw new NotImplementedException("Unknow Selector type")
		};
	}

	private static bool InnerHasTrailingSeparator(AttributeSelector attributeSelector)
	{
		return InnerHasTrailingSeparator(attributeSelector.Qualifier, attributeSelector.CloseBracket);
	}

	private static bool InnerHasTrailingSeparator(ClassSelector classSelector)
	{
		return InnerHasTrailingSeparator(classSelector.Qualifier, classSelector.Identifier);
	}

	private static bool InnerHasTrailingSeparator(IdSelector idSelector)
	{
		return InnerHasTrailingSeparator(idSelector.Qualifier, idSelector.Id);
	}

	private static bool InnerHasTrailingSeparator(TagSelector tagSelector)
	{
		return InnerHasTrailingSeparator(tagSelector.Qualifier, tagSelector.Identifier);
	}

	private static bool InnerHasTrailingSeparator(UniversalSelector universalSelector)
	{
		return InnerHasTrailingSeparator(universalSelector.Qualifier, universalSelector.Asterisk);
	}

	private static bool InnerHasTrailingSeparator(ISelectorQualifier? qualifier, ISeparatedToken token)
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
