using ScssNet.Lexing;
using ScssNet.SourceElements;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class IdSelectorParser(Lazy<SelectorParser> selectorParser)
{
	internal IdSelector? Parse(ITokenReader tokenReader)
	{
		var hash = tokenReader.Match(Symbol.Hash);
		if(hash is null)
			return null;

		var identifier = tokenReader.RequireIdentifier();

		var selectorQualifier = identifier.TrailingSeparator == Separator.Empty
			? selectorParser.Value.ParseQualifier(tokenReader)
			: default;

		return new IdSelector(hash, identifier, selectorQualifier);
	}
}
