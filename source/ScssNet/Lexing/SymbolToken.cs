namespace ScssNet.Lexing
{
	public enum Symbol
	{
		Dot, Hash, Colon, SemiColon, OpenBrace, CloseBrace, OpenBracket, CloseBracket, Equals,
		ContainsWord, StartsWithWord, StartsWith, EndsWith, Contains
	}

	public class SymbolToken : IToken
	{
		public Symbol Symbol { get; }

		public SourceCoordinates Start { get; }
		public SourceCoordinates End { get; }
		public IEnumerable<Issue> Issues => _issues;

		private readonly ICollection<Issue> _issues = [];

		internal SymbolToken(Symbol symbol, SourceCoordinates start, SourceCoordinates end) : this(symbol, start, end, []) { }

		private SymbolToken(Symbol symbol, SourceCoordinates start, SourceCoordinates end, ICollection<Issue> issues)
		{
			Symbol = symbol;
			Start = start;
			End = end;
			_issues = issues;
		}

		internal static SymbolToken CreateMissing(Symbol symbol, SourceCoordinates start)
		{
			return new SymbolToken(symbol, start, start, [new Issue(IssueType.Error, "Expected " + ToChars(symbol))]);
		}

		private static string ToChars(Symbol symbol)
		{
			return symbol switch
			{
				Symbol.ContainsWord => "~=",
				Symbol.StartsWithWord => "|=",
				Symbol.StartsWith => "^=",
				Symbol.EndsWith => "$=",
				Symbol.Contains => "*=",
				Symbol.Dot => ".",
				Symbol.Hash => "#",
				Symbol.Colon => ":",
				Symbol.SemiColon => ";",
				Symbol.OpenBrace => "{",
				Symbol.CloseBrace => "}",
				Symbol.OpenBracket => "[",
				Symbol.CloseBracket => "]",
				Symbol.Equals => "=",
				_ => throw new NotImplementedException("Missing symbol characters"),
			};
		}
	}

	internal class SymbolParser
	{
		public SymbolToken? Parse(ISourceReader reader)
		{
			if (reader.End)
				return null;

			var symbol = ParseTwoCharacterSymbol(reader) ?? ParseOneCharacterSymbol(reader);

			if (symbol is null)
				return null;

			var startCoordinates = reader.GetCoordinates();
			reader.Read(symbol >= Symbol.ContainsWord ? 2 : 1);

			return new SymbolToken(symbol.Value, startCoordinates, reader.GetCoordinates());
		}

		private Symbol? ParseTwoCharacterSymbol(ISourceReader reader)
		{
			return reader.Peek(2) switch
			{
				"~=" => Symbol.ContainsWord,
				"|=" => Symbol.StartsWithWord,
				"^=" => Symbol.StartsWith,
				"$=" => Symbol.EndsWith,
				"*=" => Symbol.Contains,
				_ => null
			};
		}

		private Symbol? ParseOneCharacterSymbol(ISourceReader reader)
		{
			return reader.Peek() switch
			{
				'.' => Symbol.Dot,
				'#' => Symbol.Hash,
				':' => Symbol.Colon,
				';' => Symbol.SemiColon,
				'{' => Symbol.OpenBrace,
				'}' => Symbol.CloseBrace,
				'[' => Symbol.OpenBracket,
				']' => Symbol.CloseBracket,
				'=' => Symbol.Equals,
				_ => null
			};
		}
	}
}
