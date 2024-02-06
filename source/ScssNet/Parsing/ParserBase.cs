using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	internal abstract class ParserBase
	{
		protected SymbolToken? Match(TokenReader tokenReader, Symbol symbol)
		{
			return tokenReader.Peek() is SymbolToken symbolToken && symbolToken.Symbol == symbol ? symbolToken : null;
		}

		protected SymbolToken Require(TokenReader tokenReader, Symbol symbol)
		{
			var symbolToken = Match(tokenReader, symbol);
			if (symbolToken == null)
				return new MissingSymbolToken(symbol, tokenReader.LineNumber, tokenReader.ColumnNumber);

			tokenReader.Read();
			return symbolToken;
		}
	}
}
