using ScssNet.Structures;
using ScssNet.Lexing;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class UniversalSelectorParser(Lazy<SelectorParser> selectorParser)
{
	internal UniversalSelector? Parse(ITokenReader tokenReader)
	{
		var asterisk = tokenReader.Match(Symbol.Asterisk);
		if(asterisk is null)
			return null;

		var selectorQualifier = !asterisk.HasTrailingSeparator()
			? selectorParser.Value.ParseQualifier(tokenReader)
			: default;

		return new UniversalSelector(asterisk, selectorQualifier);
	}
}
