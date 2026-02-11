using ScssNet.Lexing;
using ScssNet.SourceElements;

namespace ScssNet.Parsing;

internal class SelectorParser
(
	Lazy<TagSelectorParser> tagSelectorParser, Lazy<IdSelectorParser> idSelectorParser,
	Lazy<ClassSelectorParser> classSelectorParser, Lazy<AttributteSelectorParser> attributteSelectorParser
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

		return selector;
	}

	internal ISelectorQualifier? ParseQualifier(ITokenReader tokenReader)
	{
		// A qualifier is an selector that comes after the first selector of a compound selector.
		// For example, in the compound selector "div#my-id.my-class", "#my-id" and ".my-class" are qualifiers.

		return (ISelectorQualifier?)idSelectorParser.Value.Parse(tokenReader)
			?? (ISelectorQualifier?)classSelectorParser.Value.Parse(tokenReader)
			?? attributteSelectorParser.Value.Parse(tokenReader);
	}
}
