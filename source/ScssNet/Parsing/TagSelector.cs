using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class TagSelector(IdentifierToken identifier, ICompoundSelector? qualifier)
	{
		public IdentifierToken Identifier { get; } = identifier;
		public ICompoundSelector? Qualifier { get; } = qualifier;
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
