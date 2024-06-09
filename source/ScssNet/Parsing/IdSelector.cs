using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class IdSelector(SymbolToken hash, IdentifierToken identifier, ICompoundSelector? qualifier) : ISourceElement, ISelector, ICompoundSelector
	{
		public SymbolToken Hash => hash;
		public IdentifierToken Identifier => identifier;
		public ICompoundSelector? Qualifier => qualifier;

		public IEnumerable<Issue> Issues => SourceElement.List(Hash, Identifier, Qualifier).ConcatIssues();

		public SourceCoordinates Start => Hash.Start;

		public SourceCoordinates End => SourceElement.List(identifier, qualifier).LastEnd();
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
