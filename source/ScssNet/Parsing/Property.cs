using System;
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
			if(tokenReader.Peek() is not IdentifierToken)
				return null;

			var identifier = (tokenReader.Read() as IdentifierToken)!;
			var colon = Require(tokenReader, SymbolToken.Colon);

			if(tokenReader.Peek() is not ValueToken value)
				throw new NotImplementedException();

			return new Property(identifier, colon, value);
		}
	}
}
