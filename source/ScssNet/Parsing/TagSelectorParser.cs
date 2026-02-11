using ScssNet.Lexing;
using ScssNet.SourceElements;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class TagSelectorParser(Lazy<SelectorParser> selectorParser)
{
	internal TagSelector? Parse(ITokenReader tokenReader)
	{
		var identifier = tokenReader.Match<IdentifierToken>();
		if(identifier is null)
			return null;

		var selectorQualifier = identifier.TrailingSeparator == Separator.Empty
			? selectorParser.Value.ParseQualifier(tokenReader)
			: default;

		return new TagSelector(identifier, selectorQualifier);
	}
}
