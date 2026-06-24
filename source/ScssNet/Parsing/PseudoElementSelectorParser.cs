using ScssNet.Structures;
using ScssNet.Lexing;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class PseudoElementSelectorParser(Lazy<SelectorParser> selectorParser)
{
	internal PseudoElementSelector? Parse(ITokenReader tokenReader)
	{
		var doubleColon = tokenReader.Match(Symbol.DoubleColon);
		if(doubleColon is null)
			return null;

		var identifier = tokenReader.RequireIdentifier();

		var selectorQualifier = !identifier.HasTrailingSeparator()
			? selectorParser.Value.ParseQualifier(tokenReader)
			: default;

		return new PseudoElementSelector(doubleColon, identifier, selectorQualifier);
	}
}
