using ScssNet.Lexing;
using ScssNet.SourceElements;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class SelectorParser
(
	Lazy<TagSelectorParser> tagSelectorParser, Lazy<IdSelectorParser> idSelectorParser,
	Lazy<ClassSelectorParser> classSelectorParser, Lazy<AttributeSelectorParser> attributeSelectorParser
)
{
	internal ISelector? Parse(ITokenReader tokenReader)
	{
		var selector = (ISelector?)tagSelectorParser.Value.Parse(tokenReader)
			?? (ISelector?)idSelectorParser.Value.Parse(tokenReader)
			?? (ISelector?)classSelectorParser.Value.Parse(tokenReader)
			?? attributeSelectorParser.Value.Parse(tokenReader);

		if(selector == null)
			return null;

		var complexSelector = ParseComplex(tokenReader, selector);
		while(complexSelector != null)
		{
			selector = complexSelector;
			complexSelector = ParseComplex(tokenReader, selector);
		}

		return selector;
	}

	internal ISelectorQualifier? ParseQualifier(ITokenReader tokenReader)
	{
		// A qualifier is an selector that comes after the first selector of a compound selector.
		// For example, in the compound selector "div#my-id.my-class", "#my-id" and ".my-class" are qualifiers.

		return (ISelectorQualifier?)idSelectorParser.Value.Parse(tokenReader)
			?? (ISelectorQualifier?)classSelectorParser.Value.Parse(tokenReader)
			?? attributeSelectorParser.Value.Parse(tokenReader);
	}

	private IComplexSelector? ParseComplex(ITokenReader tokenReader, ISelector previousSelector)
	{
		var combinator = tokenReader.Match([Symbol.GreaterThan, Symbol.Tilde, Symbol.Plus]);
		if(combinator is null)
		{
			return previousSelector.HasSeparatorAfter()
				? ParseDescendant(tokenReader, previousSelector)
				: null;
		}

		var selector = Parse(tokenReader) ?? throw new NotImplementedException("Handle missing selector");

		switch(combinator.Symbol)
		{
			case Symbol.GreaterThan:
				return new ChildSelector(previousSelector, combinator, selector);
			case Symbol.Tilde:
				return new SubsequentSiblingSelector(previousSelector, combinator, selector);
			case Symbol.Plus:
				return new NextSiblingSelector(previousSelector, combinator, selector);
			default:
				throw new NotImplementedException("Invalid combinator symbol");
		}
	}

	private IComplexSelector? ParseDescendant(ITokenReader tokenReader, ISelector previousSelector)
	{
		var childSelector = Parse(tokenReader);
		if(childSelector is null)
			return null;

		return new DescendantSelector(previousSelector, childSelector);
	}
}
