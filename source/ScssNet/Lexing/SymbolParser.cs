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
		public int LineNumber { get; }
		public int ColumnNumber { get; }

		internal SymbolToken(Symbol symbol, int lineNumber, int columnNumber)
		{
			Symbol = symbol;
			LineNumber = lineNumber;
			ColumnNumber = columnNumber;
		}
	}

	internal class SymbolParser
	{
		public SymbolToken? Parse(SourceReader reader)
		{
			if (reader.End)
				return null;

			var symbol = ParseTwoCharacterSymbol(reader) ?? ParseOneCharacterSymbol(reader);

			if (symbol is null)
				return null;

			var symbolToken = new SymbolToken(symbol.Value, reader.LineNumber, reader.ColumnNumber);
			reader.Read();

			return symbolToken;
		}

		private Symbol? ParseTwoCharacterSymbol(SourceReader reader)
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

		private Symbol? ParseOneCharacterSymbol(SourceReader reader)
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
