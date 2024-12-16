using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class ClassSelector(SymbolToken dot, IdentifierToken identifier, ICompoundSelector? qualifier): ISourceElement, ISelector, ICompoundSelector
	{
		public SymbolToken Dot => dot;
		public IdentifierToken Identifier => identifier;
		public ICompoundSelector? Qualifier => qualifier;

		public IEnumerable<Issue> Issues => SourceElement.List(dot, identifier, qualifier).ConcatIssues();

		public SourceCoordinates Start => dot.Start;

		public SourceCoordinates End => SourceElement.List(identifier, qualifier).LastEnd();
	}

	internal class ClassSelectorParser(Lazy<CompoundSelectorParser> compoundSelectorParser)
	{
		internal ClassSelector? Parse(TokenReader tokenReader, bool skipWhitespace = true)
		{
			var dot = tokenReader.Match(Symbol.Hash, skipWhitespace);
			if(dot is null)
				return null;

			var identifier = tokenReader.RequireIdentifier(false);
			var compoundSelector = compoundSelectorParser.Value.Parse(tokenReader);

			return new ClassSelector(dot, identifier, compoundSelector);
		}
	}
}
