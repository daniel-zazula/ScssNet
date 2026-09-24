using ScssNet.Tokens;

namespace ScssNet.Lexing;

internal static class SymbolTokenMatchingExtensions
{
	public static SymbolToken? MatchSymbol(this TokenReader tokenReader, Symbol symbol)
	{
		return tokenReader.Match(t => t is SymbolToken st && st.Symbol == symbol ? st : default);
	}

	public static SymbolToken? MatchSymbol(this TokenReader tokenReader, ICollection<Symbol> symbols)
	{
		return tokenReader.Match(t => t is SymbolToken st && symbols.Contains(st.Symbol) ? st : default);
	}

	public static SymbolToken RequireSymbol(this TokenReader tokenReader, Symbol symbol)
	{
		return tokenReader.MatchSymbol(symbol)
			?? new SymbolToken(symbol, tokenReader.GetCoordinates(), Issue.CreateExpected(symbol.ToChars()));
	}
}
