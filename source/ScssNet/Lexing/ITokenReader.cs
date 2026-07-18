using ScssNet.Tokens;

namespace ScssNet.Lexing;

internal interface ITokenReader
{
	bool End { get; }

	SourceCoordinates GetCoordinates();
	SymbolToken? Match(ICollection<Symbol> symbols);
	SymbolToken? Match(Symbol symbol);
	KeywordToken? Match(Keyword keyword);
	T? Match<T>() where T : IToken;
	SymbolToken Require(Symbol symbol);
	IdentifierToken RequireIdentifier();
	StringToken RequireString();
}
