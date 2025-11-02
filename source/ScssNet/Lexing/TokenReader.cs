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
	private IToken? NextToken;
	private Separator? LastSeparator;

	private readonly IdentifierParser IdentifierParser = identifierParser;
	private readonly SymbolParser SymbolParser = symbolParser;
	private readonly UnitValueParser UnitValueParser = unitValueParser;
	private readonly HexValueParser HexValueParser = hexValueParser;
	private readonly StringParser StringParser = stringParser;
	private readonly CommentParser CommentParser = commentParser;
	private readonly WhiteSpaceParser WhiteSpaceParser = whiteSpaceParser;

	public SourceCoordinates GetCoordinates() => SourceReader.GetCoordinates();

	public SymbolToken? Match(Symbol symbol, bool skipWhitespaceOrComment = true)
	{
		return Match([symbol], skipWhitespaceOrComment);
	}

	public SymbolToken? Match(ICollection<Symbol> symbols, bool skipWhitespaceOrComment = true)
	{
		if(Peek(skipWhitespaceOrComment) is SymbolToken symbolToken && symbols.Contains(symbolToken.Symbol))
		{
			ReadNextToken();
			return symbolToken;
		}

		return null;
	}

	public T? Match<T>(bool skipWhitespaceOrComment = true)
		where T : IToken
	{
		var typeOfT = typeof(T);
		if(typeOfT == typeof(SymbolToken))
			throw new InvalidOperationException("Use Match(Symbol symbol, ...) for matching symbols.");

		if(typeOfT == typeof(WhiteSpaceToken) || typeOfT == typeof(CommentToken))
			skipWhitespaceOrComment = false;

		if(Peek(skipWhitespaceOrComment) is T token)
		{
			ReadNextToken();
			return token;
		}

		return default;
	}

	public SymbolToken Require(Symbol symbol, bool skipWhitespaceOrComment = true)
	{
		return Match(symbol, skipWhitespaceOrComment) ?? SymbolToken.CreateMissing(symbol, GetCoordinates());
	}

	public IdentifierToken RequireIdentifier(bool skipWhitespaceOrComment = true)
	{
		return Match<IdentifierToken>(skipWhitespaceOrComment) ?? IdentifierToken.CreateMissing(GetCoordinates());
	}

	public StringToken RequireString()
	{
		return Match<StringToken>() ?? StringToken.CreateMissing(GetCoordinates());
	}

	private IToken? Peek(bool skipWhitespaceOrComment)
	{
		if(SourceReader.End)
			return NextToken;

		if(NextToken == null)
			ReadNextToken();

		if(skipWhitespaceOrComment)
		{
			while(NextToken is WhiteSpaceToken or CommentToken)
			{
				ReadNextToken();
			}
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
		else if(NextToken is null)
		{
			LastSeparator = ReadSeparator();
		}

		var separatedToken = (ISeparatedToken?)IdentifierParser.Parse(SourceReader, LastSeparator, ReadSeparator)
			?? (ISeparatedToken?)SymbolParser.Parse(SourceReader, LastSeparator, ReadSeparator)
			?? (ISeparatedToken?)UnitValueParser.Parse(SourceReader, LastSeparator, ReadSeparator)
			?? (ISeparatedToken?)HexValueParser.Parse(SourceReader, LastSeparator, ReadSeparator)
			?? (ISeparatedToken?)StringParser.Parse(SourceReader, LastSeparator, ReadSeparator)
			?? throw new Exception("Failed to parse any tokens");

		LastSeparator = separatedToken.TrailingSeparator;

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

		return new Separator(tokens);

		ISeparatorToken? ReadSeparatorToken()
		{
			return (ISeparatorToken?)CommentParser.Parse(SourceReader)
				?? WhiteSpaceParser.Parse(SourceReader);
		}
	}
}
