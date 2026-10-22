using ScssNet.Structures;
using ScssNet.Lexing;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal interface IPseudoElementSelectorParser: IParser<PseudoElementSelector> { }

internal class PseudoElementSelectorParser(ParserContext parserContext): IPseudoElementSelectorParser
{
	private ISelectorQualifierParser SelectorQualifierParser => parserContext.GetParser<ISelectorQualifierParser>();

	public PseudoElementSelector? Parse(TokenReader tokenReader)
	{
		var doubleColon = tokenReader.MatchSymbol(Symbol.DoubleColon);
		if(doubleColon is null)
			return null;

		var identifier = tokenReader.RequireIdentifier();

		var selectorQualifier = !identifier.HasTrailingSeparator()
			? SelectorQualifierParser.Parse(tokenReader)
			: default;

		return new PseudoElementSelector(doubleColon, identifier, selectorQualifier);
	}
}
