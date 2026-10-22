using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal interface IIdSelectorParser : IParser<IdSelector> { }

internal class IdSelectorParser(ParserContext parserContext): IIdSelectorParser
{
	private ISelectorQualifierParser SelectorQualifierParser => parserContext.GetParser<ISelectorQualifierParser>();

	public IdSelector? Parse(TokenReader tokenReader)
	{
		var hashValue = tokenReader.MatchHashValue();
		if(hashValue is null)
			return null;

		var selectorQualifier = hashValue.TrailingSeparator == Separator.Empty
			? SelectorQualifierParser.Parse(tokenReader)
			: default;

		return new IdSelector(hashValue, selectorQualifier);
	}
}
