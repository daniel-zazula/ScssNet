using ScssNet.Lexing;
using ScssNet.SourceElements;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class TagSelectorParser(Lazy<CompoundSelectorParser> compoundSelectorParser)
{
	internal TagSelector? Parse(TokenReader tokenReader)
	{
		var identifier = tokenReader.Match<IdentifierToken>();
		if(identifier is null)
			return null;

		var compoundSelector = compoundSelectorParser.Value.Parse(tokenReader);

		return new TagSelector(identifier, compoundSelector);
	}
}
