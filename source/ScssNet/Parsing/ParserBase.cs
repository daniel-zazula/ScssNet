using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	internal abstract class ParserBase
	{
		protected SymbolToken? Match(TokenReader tokenReader, Symbol symbol, bool skipWhitespace = true)
		{
			return Match(tokenReader, [symbol], skipWhitespace);
		}

		protected SymbolToken? Match(TokenReader tokenReader, ICollection<Symbol> symbols, bool skipWhitespace = true)
		{
			if(tokenReader.Peek(skipWhitespace) is SymbolToken symbolToken && symbols.Contains(symbolToken.Symbol))
			{
				tokenReader.Read();
				return symbolToken;
			}

			return null;
		}

		protected IdentifierToken? MatchIdentifier(TokenReader tokenReader, bool skipWhitespace = true)
		{
			if(tokenReader.Peek(skipWhitespace) is IdentifierToken identifierToken)
			{
				tokenReader.Read();
				return identifierToken;
			}

			return null;
		}

		protected StringToken? MatchString(TokenReader tokenReader)
		{
			if(tokenReader.Peek() is StringToken stringToken)
			{
				tokenReader.Read();
				return stringToken;
			}

			return null;
		}

		protected SymbolToken Require(TokenReader tokenReader, Symbol symbol, bool skipWhitespace = true)
		{
			var symbolToken = Match(tokenReader, symbol, skipWhitespace);
			if (symbolToken is null)
				return new MissingSymbolToken(symbol, tokenReader.GetCoordinates());

			return symbolToken;
		}

		protected IdentifierToken RequireIdentifier(TokenReader tokenReader, bool skipWhitespace = true)
		{
			var identifierToken = MatchIdentifier(tokenReader, skipWhitespace);
			if(identifierToken is null)
				return new MissingIdentifierToken(tokenReader.GetCoordinates());

			return identifierToken;
		}

		protected StringToken RequireString(TokenReader tokenReader)
		{
			var stringToken = MatchString(tokenReader);
			if(stringToken is null)
				return new MissingStringToken(tokenReader.GetCoordinates());

			return stringToken;
		}
	}
}
