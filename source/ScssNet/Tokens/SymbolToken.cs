namespace ScssNet.Tokens;

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
		var issue = new Issue(IssueType.Error, "Expected " + symbol.ToChars());
		return new SymbolToken(symbol, start, start, Separator.Empty, Separator.Empty, [issue]);
	}
}
