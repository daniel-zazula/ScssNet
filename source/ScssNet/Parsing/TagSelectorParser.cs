using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal interface ITagSelectorParser: IParser<TagSelector> { }

internal class TagSelectorParser(ParserContext parserContext): ITagSelectorParser
{
	private ISelectorQualifierParser SelectorQualifierParser => parserContext.GetParser<ISelectorQualifierParser>();

	public TagSelector? Parse(TokenReader tokenReader)
	{
		var identifier = tokenReader.MatchIdentifier();
		if(identifier is null)
			return null;

		var selectorQualifier = identifier.TrailingSeparator == Separator.Empty
			? SelectorQualifierParser.Parse(tokenReader)
			: default;

		return new TagSelector(identifier, selectorQualifier);
	}
}
