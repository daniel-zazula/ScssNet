using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	internal abstract class ParserBase
	{
		protected bool Match(TokenReader tokenReader, Symbol symbol, out SymbolToken? token, bool skipWhitespace = true)
		{
			if (tokenReader.Peek(skipWhitespace) is SymbolToken symbolToken && symbolToken.Symbol == symbol)
			{
				token = symbolToken;
				return true;
			}

			token = null;
			return false;
		}

		protected SymbolToken Require(TokenReader tokenReader, Symbol symbol)
		{
			if (Match(tokenReader, symbol, out var symbolToken))
				return new MissingSymbolToken(symbol, tokenReader.LineNumber, tokenReader.ColumnNumber);

			tokenReader.Read();
			return symbolToken!;
		}
	}
}
