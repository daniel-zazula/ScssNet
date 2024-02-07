using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class TagSelector(IdentifierToken identifier)
	{
		public IdentifierToken Identifier { get; } = identifier;
	}

	internal class TagSelectorParser
	{
		internal TagSelector? Parse(TokenReader tokenReader)
		{
			if(tokenReader.Peek() is not IdentifierToken identifier)
				return null;

			tokenReader.Read();
			return new TagSelector(identifier);
		}
	}
}
