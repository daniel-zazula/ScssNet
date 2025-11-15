using ScssNet.Lexing;
using ScssNet.SourceElements;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class SelectorParser
(
	Lazy<TagSelectorParser> tagSelectorParser, Lazy<IdSelectorParser> idSelectorParser,
	Lazy<ClassSelectorParser> classSelectorParser, Lazy<AttributteSelectorParser> attributteSelectorParser,
	Lazy<SubSelectorParser> subSelectorParser
)
{
	internal ISelector? Parse(ITokenReader tokenReader)
	{
		var selector = (ISelector?)tagSelectorParser.Value.Parse(tokenReader)
			?? (ISelector?)idSelectorParser.Value.Parse(tokenReader)
			?? (ISelector?)classSelectorParser.Value.Parse(tokenReader)
			?? attributteSelectorParser.Value.Parse(tokenReader);

		if(selector == null)
			return null;

		var separator = (IToken?)tokenReader.Match<WhiteSpaceToken>() ?? tokenReader.Match<CommentToken>();
		if (separator != null)
		{
			var subSelector = (ISelector?)subSelectorParser.Value.Parse(tokenReader, selector);
			return subSelector ?? selector;
		}

		return selector;
	}
}
