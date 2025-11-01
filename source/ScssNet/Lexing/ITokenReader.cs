using ScssNet.Tokens;

namespace ScssNet.Lexing
{
	internal interface ITokenReader
	{
		bool End { get; }

		SourceCoordinates GetCoordinates();
		SymbolToken? Match(ICollection<Symbol> symbols, bool skipWhitespaceOrComment = true);
		SymbolToken? Match(Symbol symbol, bool skipWhitespaceOrComment = true);
		T? Match<T>(bool skipWhitespaceOrComment = true) where T : IToken;
		SymbolToken Require(Symbol symbol, bool skipWhitespaceOrComment = true);
		IdentifierToken RequireIdentifier(bool skipWhitespaceOrComment = true);
		StringToken RequireString();
	}
}