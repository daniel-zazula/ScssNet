using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class IdSelector(SymbolToken hash, IdentifierToken identifier, ICompoundSelector? qualifier) : ISelector, ICompoundSelector
	{
		public SymbolToken Hash => hash;
		public IdentifierToken Identifier => identifier;
		public ICompoundSelector? Qualifier => qualifier;
	}

	internal class IdSelectorParser(Lazy<CompoundSelectorParser> compoundSelectorParser): ParserBase
	{
		internal IdSelector? Parse(TokenReader tokenReader, bool skipWhitespace = true)
		{
			var hash = Match(tokenReader, Symbol.Hash, skipWhitespace);
			if(hash is null)
				return null;

			var identifier = RequireIdentifier(tokenReader, false);
			var compoundSelector = compoundSelectorParser.Value.Parse(tokenReader);

			return new IdSelector(hash, identifier, compoundSelector);
		}
	}
}
