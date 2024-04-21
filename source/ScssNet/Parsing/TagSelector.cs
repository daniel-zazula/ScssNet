using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class TagSelector(IdentifierToken identifier, ICompoundSelector? qualifier): ISelector
	{
		public IdentifierToken Identifier => identifier;
		public ICompoundSelector? Qualifier => qualifier;
	}

	internal class TagSelectorParser(Lazy<CompoundSelectorParser> compoundSelectorParser): ParserBase
	{
		internal TagSelector? Parse(TokenReader tokenReader)
		{
			var identifier = MatchIdentifier(tokenReader);
			if(identifier is null)
				return null;

			var compoundSelector = compoundSelectorParser.Value.Parse(tokenReader);

			return new TagSelector(identifier, compoundSelector);
		}
	}
}
