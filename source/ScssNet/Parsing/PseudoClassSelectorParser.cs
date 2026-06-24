using ScssNet.Structures;
using ScssNet.Lexing;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class PseudoClassSelectorParser(Lazy<SelectorParser> selectorParser)
{
	internal PseudoClassSelector? Parse(ITokenReader tokenReader)
	{
		var colon = tokenReader.Match(Symbol.Colon);
		if(colon is null)
			return null;

		var identifier = tokenReader.RequireIdentifier();

		var selectorQualifier = !identifier.HasTrailingSeparator()
			? selectorParser.Value.ParseQualifier(tokenReader)
			: default;

		return new PseudoClassSelector(colon, identifier, selectorQualifier);
	}
}
