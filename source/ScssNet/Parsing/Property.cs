using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class Property(IdentifierToken name, SymbolToken colon, ValueToken value)
	{
		public IdentifierToken Name { get; } = name;
		public SymbolToken Colon { get; } = colon;
		public ValueToken Value { get; } = value;
	}

	internal class PropertyParser: ParserBase
	{
		internal Property? Parse(TokenReader tokenReader)
		{
			if(tokenReader.Peek() is not IdentifierToken identifier)
				return null;

			tokenReader.Read();
			var colon = Require(tokenReader, Symbol.Colon);

			if(tokenReader.Peek() is not ValueToken value)
				value = new MissingValueToken(tokenReader.LineNumber, tokenReader.ColumnNumber);
			else
				tokenReader.Read();

			return new Property(identifier, colon, value);
		}
	}
}
