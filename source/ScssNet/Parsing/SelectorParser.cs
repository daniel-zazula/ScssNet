using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal interface ISelectorParser: IParser<ISelector> { }

internal class SelectorParser(ParserContext parserContext): ISelectorParser
{
	private ITagSelectorParser TagSelectorParser => parserContext.GetParser<ITagSelectorParser>();
	private IUniversalSelectorParser UniversalSelectorParser => parserContext.GetParser<IUniversalSelectorParser>();
	private ISelectorQualifierParser SelectorQualifierParser => parserContext.GetParser<ISelectorQualifierParser>();

	public ISelector? Parse(TokenReader tokenReader)
	{
		var selector = (ISelector?)TagSelectorParser.Parse(tokenReader)
			?? UniversalSelectorParser.Parse(tokenReader)
			?? SelectorQualifierParser.Parse(tokenReader);

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
