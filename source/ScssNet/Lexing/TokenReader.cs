using ScssNet.Tokens;

namespace ScssNet.Lexing;

internal class TokenReader
(
	ISourceReader sourceReader, IdentifierParser identifierParser, SymbolParser symbolParser,
	UnitValueParser unitValueParser, HashValueParser hashValueParser, StringParser stringParser,
	CommentParser commentParser, WhiteSpaceParser whiteSpaceParser
)
{
	public bool End => NextToken == null && SourceReader.End;

	private readonly ISourceReader SourceReader = sourceReader;
	private ISeparatedToken? NextToken;

	public T? Match<T>(Func<IToken?, T?> check) where T: IToken
	{
		var match = check(Peek());
		if(match is not null)
			ReadNextToken();

		return match;
	}

	internal SourceCoordinates GetCoordinates() => Peek()?.Span.Start ?? SourceReader.GetCoordinates();

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
