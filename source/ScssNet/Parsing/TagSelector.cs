using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class TagSelector(IdentifierToken identifier, ICompoundSelector? qualifier): ISourceElement, ISelector
	{
		public IdentifierToken Identifier => identifier;
		public ICompoundSelector? Qualifier => qualifier;

		public IEnumerable<Issue> Issues => SourceElement.List(identifier, qualifier).ConcatIssues();

		public SourceCoordinates Start => identifier.Start;

		public SourceCoordinates End => SourceElement.List(identifier, qualifier).LastEnd();
	}

	internal class TagSelectorParser(Lazy<CompoundSelectorParser> compoundSelectorParser)
	{
		internal TagSelector? Parse(TokenReader tokenReader)
		{
			var identifier = tokenReader.Match<IdentifierToken>();
			if(identifier is null)
				return null;

			var compoundSelector = compoundSelectorParser.Value.Parse(tokenReader);

			return new TagSelector(identifier, compoundSelector);
		}
	}
}
