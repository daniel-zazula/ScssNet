using System;
using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class Property(IdentifierToken Name, ValueToken Value)
	{
	}

	internal class PropertyParser
	{
		internal Property? Parse(TokenReader tokenReader)
		{
			if(tokenReader.Peek() is not IdentifierToken)
				return null;

			var identifier = (tokenReader.Read() as IdentifierToken)!;
			if(tokenReader.Peek() is not SymbolToken colon || colon != SymbolToken.Colon)
				throw new NotImplementedException();

			tokenReader.Read();

			if(tokenReader.Peek() is not ValueToken value)
				throw new NotImplementedException();

			return new Property(identifier, value);
		}
	}
}
