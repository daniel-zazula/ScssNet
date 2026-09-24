using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class SelectorParser
(
	Lazy<TagSelectorParser> tagSelectorParser, Lazy<IdSelectorParser> idSelectorParser,
	Lazy<ClassSelectorParser> classSelectorParser, Lazy<AttributeSelectorParser> attributeSelectorParser,
	Lazy<UniversalSelectorParser> universalSelectorParser, Lazy<PseudoClassSelectorParser> pseudoClassSelectorParser,
	Lazy<PseudoElementSelectorParser> pseudoElementSelectorParser

)
{
	internal ISelector? Parse(TokenReader tokenReader)
	{
		var selector = ParseTagSelector() ?? ParseUniversalSelector()
			?? ParseQualifier(tokenReader);

		if(selector == null)
			return null;

		var complexSelector = ParseComplex(tokenReader, selector);
		while(complexSelector != null)
		{
			selector = complexSelector;
			complexSelector = ParseComplex(tokenReader, selector);
		}

		return selector;

		ISelector? ParseTagSelector() => tagSelectorParser.Value.Parse(tokenReader);
		ISelector? ParseUniversalSelector() => universalSelectorParser.Value.Parse(tokenReader);
	}

	internal ISelectorQualifier? ParseQualifier(TokenReader tokenReader)
	{
		return ParseIdSelector() ?? ParseClassSelector() ?? ParseAttributeSelector()
			?? ParsePseudoClassSelector() ?? ParsePseudoElementSelector();

		ISelectorQualifier? ParseIdSelector() => idSelectorParser.Value.Parse(tokenReader);
		ISelectorQualifier? ParseClassSelector() => classSelectorParser.Value.Parse(tokenReader);
		ISelectorQualifier? ParseAttributeSelector() => attributeSelectorParser.Value.Parse(tokenReader);
		ISelectorQualifier? ParsePseudoClassSelector() => pseudoClassSelectorParser.Value.Parse(tokenReader);
		ISelectorQualifier? ParsePseudoElementSelector() => pseudoElementSelectorParser.Value.Parse(tokenReader);
	}

	private IComplexSelector? ParseComplex(TokenReader tokenReader, ISelector previousSelector)
	{
		var combinator = tokenReader.MatchSymbol([Symbol.GreaterThan, Symbol.Tilde, Symbol.Plus]);
		if(combinator is null)
		{
			return previousSelector.HasTrailingSeparator()
				? ParseDescendant(tokenReader, previousSelector)
				: null;
		}

		var selector = Parse(tokenReader) ?? throw new NotImplementedException("Handle missing selector");

		return combinator.Symbol switch
		{
			Symbol.GreaterThan => new ChildSelector(previousSelector, combinator, selector),
			Symbol.Tilde => new SubsequentSiblingSelector(previousSelector, combinator, selector),
			Symbol.Plus => new NextSiblingSelector(previousSelector, combinator, selector),
			_ => throw new NotImplementedException("Invalid combinator symbol"),
		};
	}

	private IComplexSelector? ParseDescendant(TokenReader tokenReader, ISelector previousSelector)
	{
		var childSelector = Parse(tokenReader);
		if(childSelector is null)
			return null;

		return new DescendantSelector(previousSelector, childSelector);
	}
}
