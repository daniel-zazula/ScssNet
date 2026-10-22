using ScssNet.Structures;
using ScssNet.Lexing;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal interface IPseudoClassSelectorParser : IParser<PseudoClassSelector> { }

internal class PseudoClassSelectorParser(ParserContext parserContext): IPseudoClassSelectorParser
{
	private ISelectorQualifierParser SelectorQualifierParser => parserContext.GetParser<ISelectorQualifierParser>();

	public PseudoClassSelector? Parse(TokenReader tokenReader)
	{
		var colon = tokenReader.MatchSymbol(Symbol.Colon);
		if(colon is null)
			return null;

		var identifier = tokenReader.RequireIdentifier();

		var selectorQualifier = !identifier.HasTrailingSeparator()
			? SelectorQualifierParser.Parse(tokenReader)
			: default;

		return new PseudoClassSelector(colon, identifier, selectorQualifier);
	}
}
