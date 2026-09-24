using ScssNet.Structures;
using ScssNet.Lexing;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class ClassSelectorParser(Lazy<SelectorParser> selectorParser)
{
	internal ClassSelector? Parse(TokenReader tokenReader)
	{
		var dot = tokenReader.MatchSymbol(Symbol.Dot);
		if(dot is null)
			return null;

		var identifier = tokenReader.RequireIdentifier();

		var selectorQualifier = !identifier.HasTrailingSeparator()
			? selectorParser.Value.ParseQualifier(tokenReader)
			: default;

		return new ClassSelector(dot, identifier, selectorQualifier);
	}
}
