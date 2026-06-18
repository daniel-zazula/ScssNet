using ScssNet.Tokens;

namespace ScssNet.Lexing;

internal class TokenReader
(
	ISourceReader sourceReader, IdentifierParser identifierParser, SymbolParser symbolParser,
	UnitValueParser unitValueParser, HashValueParser hashValueParser, StringParser stringParser,
	CommentParser commentParser, WhiteSpaceParser whiteSpaceParser
) : ITokenReader
{
	public bool End => NextToken == null && SourceReader.End;

	private readonly ISourceReader SourceReader = sourceReader;
	private ISeparatedToken? NextToken;

	public SourceCoordinates GetCoordinates() => SourceReader.GetCoordinates();

	public SymbolToken? Match(Symbol symbol)
	{
		return Match([symbol]);
	}

	public SymbolToken? Match(ICollection<Symbol> symbols)
	{
		if(Peek() is SymbolToken symbolToken && symbols.Contains(symbolToken.Symbol))
		{
			ReadNextToken();
			return symbolToken;
		}

		return null;
	}

	public T? Match<T>() where T : IToken
	{
		var typeOfT = typeof(T);
		if(typeOfT == typeof(SymbolToken))
			throw new InvalidOperationException("Use Match(Symbol symbol) for matching symbols.");

		if(Peek() is T token)
		{
			ReadNextToken();
			return token;
		}

		return default;
	}

	public SymbolToken Require(Symbol symbol)
	{
		return Match(symbol) ?? SymbolToken.CreateMissing(symbol, GetCoordinates());
	}

	public IdentifierToken RequireIdentifier()
	{
		return Match<IdentifierToken>() ?? IdentifierToken.CreateMissing(GetCoordinates());
	}

	public StringToken RequireString()
	{
		return Match<StringToken>() ?? StringToken.CreateMissing(GetCoordinates());
	}

	private IToken? Peek()
	{
		if(!SourceReader.End && NextToken == null)
		{
			// Reads the first token
			ReadNextToken();
		}

		return NextToken;
	}

	private void ReadNextToken(bool acceptHexValue = false)
	{
		if(SourceReader.End)
		{
			NextToken = null;
			return;
		}

		var leadingSeparator = NextToken is null
			? ReadSeparator()
			: NextToken.TrailingSeparator;

		var separatedToken = ParseSymbol() ?? ParseIdentifier() ?? ParseUnitValue() ?? ParseString() ?? ParseHashValue()
			?? throw new Exception("Failed to parse any tokens");

		NextToken = separatedToken;

		// Local functions

		Separator GetTrailingSeparator() => ReadSeparator();

		ISeparatedToken? ParseSymbol()
		{
			return symbolParser.Parse(SourceReader, leadingSeparator, GetTrailingSeparator);
		}

		ISeparatedToken? ParseIdentifier()
		{
			return identifierParser.Parse(SourceReader, leadingSeparator, GetTrailingSeparator);
		}

		ISeparatedToken? ParseUnitValue()
		{
			return unitValueParser.Parse(SourceReader, leadingSeparator, GetTrailingSeparator);
		}

		ISeparatedToken? ParseString()
		{
			return stringParser.Parse(SourceReader, leadingSeparator, GetTrailingSeparator);
		}

		ISeparatedToken? ParseHashValue()
		{
			return hashValueParser.Parse(SourceReader, leadingSeparator, GetTrailingSeparator);
		}
	}

	private Separator ReadSeparator()
	{
		var tokens = new List<ISeparatorToken>();

		var token = ReadSeparatorToken();
		while(token != null)
		{
			tokens.Add(token);
			token = ReadSeparatorToken();
		}

		return tokens.Count > 0 ? new Separator(tokens) : Separator.Empty;

		ISeparatorToken? ReadSeparatorToken()
		{
			return (ISeparatorToken?)commentParser.Parse(SourceReader)
				?? whiteSpaceParser.Parse(SourceReader);
		}
	}
}
