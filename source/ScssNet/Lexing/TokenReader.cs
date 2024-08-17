namespace ScssNet.Lexing
{
	internal class TokenReader(ISourceReader sourceReader)
	{
		public bool End => NextToken == null && SourceReader.End;

		private readonly ISourceReader SourceReader = sourceReader;
		private IToken? NextToken;

		private readonly IdentifierParser IdentifierParser = new();
		private readonly SymbolParser SymbolParser = new();
		private readonly UnitValueParser UnitValueParser = new();
		private readonly HexValueParser HexValueParser = new();
		private readonly StringParser StringParser = new();
		private readonly CommentParser CommentParser = new();
		private readonly WhiteSpaceParser WhiteSpaceParser = new();

		public IToken? Peek(bool skipWhitespace = true)
		{
			if (NextToken == null && !SourceReader.End)
				ReadNextToken();

			if(skipWhitespace)
			{
				while(NextToken is WhiteSpaceToken)
				{
					if (SourceReader.End)
						NextToken = null;
					else
						ReadNextToken();
				}
			}

			return NextToken;
		}

		public IToken? Read(bool skipWhitespace = true)
		{
			var token = Peek(skipWhitespace);
			ReadNextToken();
			return token;
		}

		public SourceCoordinates GetCoordinates() => SourceReader.GetCoordinates();

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
