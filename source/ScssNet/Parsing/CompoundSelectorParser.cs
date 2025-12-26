using ScssNet.Lexing;
using ScssNet.SourceElements;

namespace ScssNet.Parsing;

internal class CompoundSelectorParser
(
	Lazy<IdSelectorParser> idSelectorParser, Lazy<ClassSelectorParser> classSelectorParser,
	Lazy<AttributteSelectorParser> attributteSelectorParser
)
{
	internal ICompoundSelector? Parse(ITokenReader tokenReader)
	{
		if (!tokenReader.LastSeparatorWasEmpty())
			return null;

		return (ICompoundSelector?)idSelectorParser.Value.Parse(tokenReader)
			?? (ICompoundSelector?)classSelectorParser.Value.Parse(tokenReader)
			?? attributteSelectorParser.Value.Parse(tokenReader);
	}
}
