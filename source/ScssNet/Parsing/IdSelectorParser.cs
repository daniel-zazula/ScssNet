using ScssNet.Lexing;
using ScssNet.SourceElements;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class IdSelectorParser(Lazy<CompoundSelectorParser> compoundSelectorParser)
{
	internal IdSelector? Parse(ITokenReader tokenReader)
	{
		var hash = tokenReader.Match(Symbol.Hash);
		if(hash is null)
			return null;

		var identifier = tokenReader.RequireIdentifier();
		var compoundSelector = compoundSelectorParser.Value.Parse(tokenReader);

		return new IdSelector(hash, identifier, compoundSelector);
	}
}
