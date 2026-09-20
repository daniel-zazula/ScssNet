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
		else if (typeOfT.IsSubclassOfGeneric(typeof(KeywordToken<>)))
			throw new InvalidOperationException("Use Match(Keyword keyword) for matching keywords.");

		if(Peek() is T token)
		{
			ReadNextToken();
			return token;
		}

		return default;
	}

	public (T keyword, IdentifierToken identifierToken)? MatchKeyword<T>() where T : struct, Enum
	{
		if(Peek() is IdentifierToken identifierToken)
		{
			var matchedKeyword = MatchesAnyKeywordOfT(identifierToken);
			if(matchedKeyword is not null)
			{
				ReadNextToken();
				return (matchedKeyword.Value, identifierToken);
			}
		}

		return null;

		static T? MatchesAnyKeywordOfT(IdentifierToken identifier)
		{
			var text = identifier.Text;
			var keywords = (T[])Enum.GetValues(typeof(T));
			foreach(var keyword in keywords)
			{
				if(string.Equals(text, keyword.ToString(), StringComparison.OrdinalIgnoreCase))
					return keyword;
			}

			return null;
		}
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
