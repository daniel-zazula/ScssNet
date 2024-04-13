using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class TagSelector(IdentifierToken identifier, ICompoundSelector? qualifier): ISelector
	{
		public IdentifierToken Identifier => identifier;
		public ICompoundSelector? Qualifier => qualifier;
	}

	internal class TagSelectorParser(Lazy<CompoundSelectorParser> compoundSelectorParser)
	{
		internal TagSelector? Parse(TokenReader tokenReader)
		{
			if(tokenReader.Peek() is not IdentifierToken identifier)
				return null;

			tokenReader.Read();
			return new TagSelector(identifier, compoundSelectorParser.Value.Parse(tokenReader));
		}
	}
}
