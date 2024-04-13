using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public interface ISelector
	{

	}

	internal class SelectorParser(Lazy<TagSelectorParser> tagSelectorParser, Lazy<IdSelectorParser> idSelectorParser, Lazy<ClassSelectorParser> classSelectorParser)
	{
		internal ISelector? Parse(TokenReader tokenReader)
		{
			return (ISelector?)tagSelectorParser.Value.Parse(tokenReader)
				?? (ISelector?)idSelectorParser.Value.Parse(tokenReader)
				?? classSelectorParser.Value.Parse(tokenReader);
		}
	}
}
