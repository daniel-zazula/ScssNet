using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class IdSelectorParser(Lazy<SelectorParser> selectorParser)
{
	internal IdSelector? Parse(ITokenReader tokenReader)
	{
		var hashValue = tokenReader.Match<HashValueToken>();
		if(hashValue is null)
			return null;

		var selectorQualifier = hashValue.TrailingSeparator == Separator.Empty
			? selectorParser.Value.ParseQualifier(tokenReader)
			: default;

		return new IdSelector(hashValue, selectorQualifier);
	}
}
