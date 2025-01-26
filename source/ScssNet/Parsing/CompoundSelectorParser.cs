using ScssNet.Lexing;
using ScssNet.SourceElements;

namespace ScssNet.Parsing
{
	internal class CompoundSelectorParser
	(
		Lazy<IdSelectorParser> idSelectorParser, Lazy<ClassSelectorParser> classSelectorParser, Lazy<AttributteSelectorParser> attributteSelectorParser
	)
	{
		internal ICompoundSelector? Parse(TokenReader tokenReader)
		{
			return (ICompoundSelector?)idSelectorParser.Value.Parse(tokenReader, false)
				?? (ICompoundSelector?)classSelectorParser.Value.Parse(tokenReader, false)
				?? attributteSelectorParser.Value.Parse(tokenReader, false);
		}
	}
}
