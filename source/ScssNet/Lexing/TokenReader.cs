using ScssNet.Tokens;

namespace ScssNet.Lexing;

internal class TokenReader
(
	ISourceReader sourceReader, IdentifierParser identifierParser, SymbolParser symbolParser,
	UnitValueParser unitValueParser, HexValueParser hexValueParser, StringParser stringParser,
	CommentParser commentParser, WhiteSpaceParser whiteSpaceParser
) : ITokenReader
{
	public bool End => NextToken == null && SourceReader.End;

	private readonly ISourceReader SourceReader = sourceReader;
	private ISeparatedToken? NextToken;

	private readonly IdentifierParser IdentifierParser = identifierParser;
	private readonly SymbolParser SymbolParser = symbolParser;
	private readonly UnitValueParser UnitValueParser = unitValueParser;
	private readonly HexValueParser HexValueParser = hexValueParser;
	private readonly StringParser StringParser = stringParser;
	private readonly CommentParser CommentParser = commentParser;
	private readonly WhiteSpaceParser WhiteSpaceParser = whiteSpaceParser;

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
			throw new InvalidOperationException("Use Match(Symbol symbol, ...) for matching symbols.");

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

	private void ReadNextToken()
	{
		if(SourceReader.End)
		{
			NextToken = null;
			return;
		}

		var leadingSeparator = NextToken is null
			? ReadSeparator()
			: NextToken.TrailingSeparator;

		var separatedToken = (ISeparatedToken?)IdentifierParser.Parse(SourceReader, leadingSeparator, ReadSeparator)
			?? (ISeparatedToken?)SymbolParser.Parse(SourceReader, leadingSeparator, ReadSeparator)
			?? (ISeparatedToken?)UnitValueParser.Parse(SourceReader, leadingSeparator, ReadSeparator)
			?? (ISeparatedToken?)HexValueParser.Parse(SourceReader, leadingSeparator, ReadSeparator)
			?? (ISeparatedToken?)StringParser.Parse(SourceReader, leadingSeparator, ReadSeparator)
			?? throw new Exception("Failed to parse any tokens");

		NextToken = separatedToken;
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
			return (ISeparatorToken?)CommentParser.Parse(SourceReader)
				?? WhiteSpaceParser.Parse(SourceReader);
		}
	}
}
