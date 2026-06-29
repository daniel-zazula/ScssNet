namespace ScssNet.Tokens;

public enum Symbol
{
	// One character symbols
	Comma, Dot, Colon, SemiColon, Asterisk, OpenBrace, CloseBrace, OpenBracket, CloseBracket, Equals,
	Plus, Tilde, Exclamation,

	// Two character symbols
	ContainsWord, StartsWithWord, StartsWith, EndsWith, Contains, GreaterThan, DoubleColon
}

public record SymbolToken: IToken, ISeparatedToken
{
	public Symbol Symbol { get; }

	public SourceCoordinates Start { get; }
	public SourceCoordinates End { get; }
	public Separator LeadingSeparator { get; }
	public Separator TrailingSeparator { get; }
	public IEnumerable<Issue> Issues { get; }

	internal SymbolToken
	(
		Symbol symbol, SourceCoordinates start, SourceCoordinates end, Separator before, Separator after,
		ICollection<Issue>? issues = null
	)
	{
		Symbol = symbol;
		Start = start;
		End = end;
		LeadingSeparator = before;
		TrailingSeparator = after;
		Issues = issues ?? [];
	}

	internal static SymbolToken CreateMissing(Symbol symbol, SourceCoordinates start)
	{
		var issue = new Issue(IssueType.Error, "Expected " + ToChars(symbol));
		return new SymbolToken(symbol, start, start, Separator.Empty, Separator.Empty, [issue]);
	}

	internal string ToChars()
	{
		return ToChars(Symbol);
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
			Symbol.Equals => "=",
			Symbol.GreaterThan => ">",
			Symbol.Plus => "+",
			Symbol.Tilde => "~",
			Symbol.Exclamation => "!",
			_ => throw new NotImplementedException("Missing symbol characters"),
		};
	}
}
