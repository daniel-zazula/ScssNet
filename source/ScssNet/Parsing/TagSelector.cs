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
			if(tokenReader.Peek() is not IdentifierToken)
				return null;

			var identifier = (tokenReader.Read() as IdentifierToken)!;
			return new TagSelector(identifier);
		}
	}
}
