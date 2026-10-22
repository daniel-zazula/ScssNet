using ScssNet.Structures;
using ScssNet.Lexing;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal interface IUniversalSelectorParser: IParser<UniversalSelector> { }

internal class UniversalSelectorParser(ParserContext parserContext): IUniversalSelectorParser
{
	private ISelectorQualifierParser SelectorQualifierParser => parserContext.GetParser<ISelectorQualifierParser>();

	public UniversalSelector? Parse(TokenReader tokenReader)
	{
		var asterisk = tokenReader.MatchSymbol(Symbol.Asterisk);
		if(asterisk is null)
			return null;

		var selectorQualifier = !asterisk.HasTrailingSeparator()
			? SelectorQualifierParser.Parse(tokenReader)
			: default;

		return new UniversalSelector(asterisk, selectorQualifier);
	}
}
