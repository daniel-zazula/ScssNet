using ScssNet.Tokens;

namespace ScssNet.Lexing;

internal class TokenReader
(
	ISourceReader sourceReader, IdentifierParser identifierParser, SymbolParser symbolParser,
	UnitValueParser unitValueParser, HexValueParser hexValueParser, StringParser stringParser,
	CommentParser commentParser, WhiteSpaceParser whiteSpaceParser
)
{
	public bool End => NextToken == null && SourceReader.End;

	private readonly ISourceReader SourceReader = sourceReader;
	private IToken? NextToken;

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
		if (SourceReader.End)
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
		if (SourceReader.End)
		{
			NextToken = null;
			return;
		}

		NextToken = (IToken?)IdentifierParser.Parse(SourceReader)
			?? (IToken?)SymbolParser.Parse(SourceReader)
			?? (IToken?)UnitValueParser.Parse(SourceReader)
			?? (IToken?)HexValueParser.Parse(SourceReader)
			?? (IToken?)StringParser.Parse(SourceReader)
			?? (IToken?)CommentParser.Parse(SourceReader)
			?? WhiteSpaceParser.Parse(SourceReader);
	}
}
