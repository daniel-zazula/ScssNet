namespace ScssNet.Tokens;

public enum Symbol
{
	// One character symbols
	Comma, Dot, Colon, SemiColon, Asterisk,
	OpenBrace, CloseBrace, OpenBracket, CloseBracket, OpenParenthesis, CloseParenthesis,
	Equals, Plus, Tilde, Exclamation, At,

	// Two character symbols
	ContainsWord, StartsWithWord, StartsWith, EndsWith, Contains, GreaterThan, DoubleColon
}

internal static class SymbolExtensions
{
	internal static string ToChars(this Symbol symbol)
	{
		return symbol switch
		{
			Symbol.ContainsWord => "~=",
			Symbol.StartsWithWord => "|=",
			Symbol.StartsWith => "^=",
			Symbol.EndsWith => "$=",
			Symbol.Contains => "*=",
			Symbol.DoubleColon => "::",
			Symbol.Comma => ",",
			Symbol.Dot => ".",
			Symbol.Colon => ":",
			Symbol.SemiColon => ";",
			Symbol.Asterisk => "*",
			Symbol.OpenBrace => "{",
			Symbol.CloseBrace => "}",
			Symbol.OpenBracket => "[",
			Symbol.CloseBracket => "]",
			Symbol.OpenParenthesis => "(",
			Symbol.CloseParenthesis => ")",
			Symbol.Equals => "=",
			Symbol.GreaterThan => ">",
			Symbol.Plus => "+",
			Symbol.Tilde => "~",
			Symbol.Exclamation => "!",
			Symbol.At => "@",
			_ => throw new NotImplementedException("Missing symbol characters"),
		};
	}
}
