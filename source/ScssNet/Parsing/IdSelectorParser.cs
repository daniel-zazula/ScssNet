using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class IdSelectorParser(Lazy<SelectorParser> selectorParser)
{
	internal IdSelector? Parse(TokenReader tokenReader)
	{
		var hashValue = tokenReader.MatchHashValue();
		if(hashValue is null)
			return null;

		var selectorQualifier = hashValue.TrailingSeparator == Separator.Empty
			? selectorParser.Value.ParseQualifier(tokenReader)
			: default;

		return new IdSelector(hashValue, selectorQualifier);
	}
}
