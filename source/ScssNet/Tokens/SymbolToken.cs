namespace ScssNet.Tokens;

public enum Symbol
{
	Comma, Dot, Hash, Colon, SemiColon, OpenBrace, CloseBrace, OpenBracket, CloseBracket, Equals,
	ContainsWord, StartsWithWord, StartsWith, EndsWith, Contains
}

public record SymbolToken: IToken
{
	public Symbol Symbol { get; }

	public SourceCoordinates Start { get; }
	public SourceCoordinates End { get; }
	public Separator? LeadingSeparator { get; }
	public Separator? TrailingSeparator { get; }
	public IEnumerable<Issue> Issues { get; }

	internal SymbolToken
	(
		Symbol symbol, SourceCoordinates start, SourceCoordinates end, Separator? before, Separator? after,
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
		return new SymbolToken
		(
			symbol, start, start, null, null, [new Issue(IssueType.Error, "Expected " + ToChars(symbol))]
		);
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
			Symbol.Comma => ",",
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
