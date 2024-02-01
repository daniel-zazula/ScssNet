namespace ScssNet.Lexing
{
	public enum Symbol
	{
		Colon, SemiColon, OpenBrace, CloseBrace, OpenParenthesis, CloseParenthesis, Asterisk, GreaterThan, PlusSign, Tilde, OpenBracket, CloseBracket, Pipe,
		DolarSign, ExclamationMark, Caret, Ampersand, AtSign, Dot, Hash
	}

	public class SymbolToken(Symbol symbol, int lineNumber, int columnNumber) : IToken
	{
		public Symbol Symbol { get; } = symbol;
		public int LineNumber { get; } = lineNumber;
		public int ColumnNumber { get; } = columnNumber;
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
