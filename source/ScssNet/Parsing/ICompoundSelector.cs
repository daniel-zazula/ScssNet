using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public interface ICompoundSelector: ISourceElement
	{
		ICompoundSelector? Qualifier { get; }
	}

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
