using ScssNet.Structures;

namespace ScssNet.Parsing;

internal static class SelectorTrailingSeparatorExtensions
{
	public static bool HasTrailingSeparator(this ISelector selector)
	{
		return selector switch
		{
			AttributeSelector attributeSelector => attributeSelector.HasTrailingSeparator(),
			ClassSelector classSelector => classSelector.HasTrailingSeparator(),
			IdSelector idSelector => idSelector.HasTrailingSeparator(),
			TagSelector tagSelector => tagSelector.HasTrailingSeparator(),
			IComplexSelector complexSelector => complexSelector.HasTrailingSeparator(),
			_ => throw new NotImplementedException("Unknow Selector type")
		};
	}

	private static bool HasTrailingSeparator(this AttributeSelector attributeSelector)
	{
		return attributeSelector.Qualifier?.HasTrailingSeparator()
			?? attributeSelector.CloseBracket.HasTrailingSeparator();
	}

	private static bool HasTrailingSeparator(this ClassSelector classSelector)
	{
		return classSelector.Qualifier?.HasTrailingSeparator()
			?? classSelector.Identifier.HasTrailingSeparator();
	}

	private static bool HasTrailingSeparator(this IdSelector idSelector)
	{
		return idSelector.Qualifier?.HasTrailingSeparator()
			?? idSelector.Id.HasTrailingSeparator();
	}

	private static bool HasTrailingSeparator(this TagSelector tagSelector)
	{
		return tagSelector.Qualifier?.HasTrailingSeparator()
			?? tagSelector.Identifier.HasTrailingSeparator();
	}

	private static bool HasTrailingSeparator(this IComplexSelector complexSelector)
	{
		return complexSelector.Selector.HasTrailingSeparator();
	}
}
