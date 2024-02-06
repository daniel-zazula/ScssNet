namespace ScssNet.Lexing
{
	public enum Symbol
	{
		Colon, SemiColon, OpenBrace, CloseBrace, OpenParenthesis, CloseParenthesis, Asterisk, GreaterThan, PlusSign, Tilde, OpenBracket, CloseBracket, Pipe,
		DolarSign, ExclamationMark, Caret, Ampersand, AtSign, Dot, Hash
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

			Symbol? symbol = reader.Peek() switch
			{
				':' => Symbol.Colon,
				';' => Symbol.SemiColon,
				'{' => Symbol.OpenBrace,
				'}' => Symbol.CloseBrace,
				'*' => Symbol.Asterisk,
				'>' => Symbol.GreaterThan,
				'+' => Symbol.PlusSign,
				'~' => Symbol.Tilde,
				'[' => Symbol.OpenBracket,
				']' => Symbol.CloseBracket,
				'|' => Symbol.Pipe,
				'$' => Symbol.DolarSign,
				'!' => Symbol.ExclamationMark,
				'^' => Symbol.Caret,
				'&' => Symbol.Ampersand,
				'@' => Symbol.AtSign,
				'.' => Symbol.Dot,
				'#' => Symbol.Hash,
				_ => null
			};

			if (symbol is null)
				return null;

			var symbolToken = new SymbolToken(symbol.Value, reader.LineNumber, reader.ColumnNumber);
			reader.Read();

			return symbolToken;
		}
	}
}
