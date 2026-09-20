using ScssNet.Tokens;

namespace ScssNet.Lexing;

internal static class SymbolTokenMatchingExtensions
{
	extension(TokenReader tokenReader)
	{
		public SymbolToken? Match(Symbol symbol)
		{
			return tokenReader.Match([symbol]);
		}

		public SymbolToken Require(Symbol symbol)
		{
			return tokenReader.Match(symbol)
				?? new SymbolToken(symbol, tokenReader.GetCoordinates(), Issue.CreateExpected(symbol.ToChars()));
		}
	}
}
