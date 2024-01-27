namespace ScssNet.Lexing
{
	public class SymbolToken: IToken
	{
		private SymbolToken() { }

		public static SymbolToken Colon = new();
		public static SymbolToken SemiColon = new();
		public static SymbolToken OpenBrace = new();
		public static SymbolToken CloseBrace = new();
		public static SymbolToken OpenParenthesis = new();
		public static SymbolToken CloseParenthesis = new();
		public static SymbolToken Asterisk = new();
		public static SymbolToken GreaterThan = new();
		public static SymbolToken PlusSign = new();
		public static SymbolToken Tilde = new();
		public static SymbolToken OpenBracket = new();
		public static SymbolToken CloseBracket = new();
		public static SymbolToken Pipe = new();
		public static SymbolToken DolarSign = new();
		public static SymbolToken ExclamationMark = new();
		public static SymbolToken Caret = new();
		public static SymbolToken Ampersand = new();
		public static SymbolToken AtSign = new();
		public static SymbolToken Dot = new();
		public static SymbolToken Hash = new();
	}

	internal class SymbolParser
	{
		public SymbolToken? Parse(SourceReader reader)
		{
			if (reader.End)
				return null;

			SymbolToken? symbol = reader.Peek() switch
			{
				':' => SymbolToken.Colon,
				';' => SymbolToken.SemiColon,
				'{' => SymbolToken.OpenBrace,
				'}' => SymbolToken.CloseBrace,
				'*' => SymbolToken.Asterisk,
				'>' => SymbolToken.GreaterThan,
				'+' => SymbolToken.PlusSign,
				'~' => SymbolToken.Tilde,
				'[' => SymbolToken.OpenBracket,
				']' => SymbolToken.CloseBracket,
				'|' => SymbolToken.Pipe,
				'$' => SymbolToken.DolarSign,
				'!' => SymbolToken.ExclamationMark,
				'^' => SymbolToken.Caret,
				'&' => SymbolToken.Ampersand,
				'@' => SymbolToken.AtSign,
				'.' => SymbolToken.Dot,
				'#' => SymbolToken.Hash,
				_ => null
			};

			if (symbol != null)
				reader.Read();

			return symbol;
		}
	}
}
