using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public interface ICompoundSelector
	{
		ICompoundSelector? Qualifier { get; }
	}

	internal class CompoundSelectorParser(Lazy<IdSelectorParser> idSelectorParser, Lazy<ClassSelectorParser> classSelectorParser)
	{
		internal ICompoundSelector? Parse(TokenReader tokenReader)
		{
			return (ICompoundSelector?)idSelectorParser.Value.Parse(tokenReader, false) ?? classSelectorParser.Value.Parse(tokenReader, false);
		}
	}
}
