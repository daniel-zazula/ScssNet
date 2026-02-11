using ScssNet.SourceElements;
using ScssNet.Lexing;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class ClassSelectorParser(Lazy<SelectorParser> selectorParser)
{
	internal ClassSelector? Parse(ITokenReader tokenReader)
	{
		var dot = tokenReader.Match(Symbol.Dot);
		if(dot is null)
			return null;

		var identifier = tokenReader.RequireIdentifier();

		var selectorQualifier = identifier.TrailingSeparator == Separator.Empty
			? selectorParser.Value.ParseQualifier(tokenReader)
			: default;

		return new ClassSelector(dot, identifier, selectorQualifier);
	}
}
