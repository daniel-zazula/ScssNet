using ScssNet.Lexing;
using ScssNet.SourceElements;
using ScssNet.Tokens;

namespace ScssNet.Parsing
{
	internal class IdSelectorParser(Lazy<CompoundSelectorParser> compoundSelectorParser)
	{
		internal IdSelector? Parse(TokenReader tokenReader, bool skipWhitespace = true)
		{
			var hash = tokenReader.Match(Symbol.Hash, skipWhitespace);
			if(hash is null)
				return null;

			var identifier = tokenReader.RequireIdentifier(false);
			var compoundSelector = compoundSelectorParser.Value.Parse(tokenReader);

			return new IdSelector(hash, identifier, compoundSelector);
		}
	}
}
