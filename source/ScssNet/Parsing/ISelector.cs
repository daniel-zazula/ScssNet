using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public interface ISelector: ISourceElement
	{

	}

	internal class SelectorParser
	(
		Lazy<TagSelectorParser> tagSelectorParser, Lazy<IdSelectorParser> idSelectorParser, Lazy<ClassSelectorParser> classSelectorParser,
		Lazy<AttributteSelectorParser> attributteSelectorParser
	)
	{
		internal ISelector? Parse(TokenReader tokenReader)
		{
			return (ISelector?)tagSelectorParser.Value.Parse(tokenReader)
				?? (ISelector?)idSelectorParser.Value.Parse(tokenReader)
				?? (ISelector?)classSelectorParser.Value.Parse(tokenReader)
				?? attributteSelectorParser.Value.Parse(tokenReader);
		}
	}
}
