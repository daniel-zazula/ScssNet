namespace ScssNet.Lexing
{
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

		public IToken? Peek()
		{
			return Peek(true);
		}

		public IToken? Read()
		{
			var token = Peek();
			ReadNextToken();
			return token;
		}

		public SourceCoordinates GetCoordinates() => SourceReader.GetCoordinates();

		public SymbolToken? Match(Symbol symbol, bool skipWhitespace = true)
		{
			return Match([symbol], skipWhitespace);
		}

		public SymbolToken? Match(ICollection<Symbol> symbols, bool skipWhitespace = true)
		{
			if(Peek(skipWhitespace) is SymbolToken symbolToken && symbols.Contains(symbolToken.Symbol))
			{
				ReadNextToken();
				return symbolToken;
			}

			return null;
		}

		public T? Match<T>(bool skipWhitespace = true)
			where T : IToken
		{
			if(typeof(T) == typeof(SymbolToken))
				throw new InvalidOperationException("Use Match(Symbol symbol, ...) for matching symbols.");

			if(Peek(skipWhitespace) is T token)
			{
				ReadNextToken();
				return token;
			}

			return default;
		}

		public SymbolToken Require(Symbol symbol, bool skipWhitespace = true)
		{
			return Match(symbol, skipWhitespace) ?? SymbolToken.CreateMissing(symbol, GetCoordinates());
		}

		public IdentifierToken RequireIdentifier(bool skipWhitespace = true)
		{
			return Match<IdentifierToken>(skipWhitespace) ?? IdentifierToken.CreateMissing(GetCoordinates());
		}

		public StringToken RequireString()
		{
			return Match<StringToken>() ?? StringToken.CreateMissing(GetCoordinates());
		}

		private IToken? Peek(bool skipWhitespace)
		{
			if(NextToken == null && !SourceReader.End)
				ReadNextToken();

			if(skipWhitespace)
			{
				while(NextToken is WhiteSpaceToken)
				{
					if(SourceReader.End)
						NextToken = null;
					else
						ReadNextToken();
				}
			}

			return NextToken;
		}

		private void ReadNextToken()
		{
			NextToken = (IToken?)IdentifierParser.Parse(SourceReader)
				?? (IToken?)SymbolParser.Parse(SourceReader)
				?? (IToken?)UnitValueParser.Parse(SourceReader)
				?? (IToken?)HexValueParser.Parse(SourceReader)
				?? (IToken?)StringParser.Parse(SourceReader)
				?? (IToken?)CommentParser.Parse(SourceReader)
				?? WhiteSpaceParser.Parse(SourceReader);
		}
	}
}
