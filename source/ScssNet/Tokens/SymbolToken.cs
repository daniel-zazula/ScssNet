namespace ScssNet.Tokens;

public record SymbolToken: IToken, ISeparatedToken
{
	public Symbol Symbol { get; }

	public SourceSpan Span { get; }
	public Separator LeadingSeparator { get; }
	public Separator TrailingSeparator { get; }
	public IEnumerable<Issue> Issues { get; }

	internal SymbolToken
	(
		Symbol symbol, SourceSpan span, Separator before, Separator after,
		ICollection<Issue>? issues = null
	)
	{
		Symbol = symbol;
		Span = span;
		LeadingSeparator = before;
		TrailingSeparator = after;
		Issues = issues ?? [];
	}

	internal static SymbolToken CreateMissing(Symbol symbol, SourceCoordinates coordinates)
	{
		var span = new SourceSpan(coordinates, coordinates);
		var issue = new Issue(IssueType.Error, "Expected " + symbol.ToChars());
		return new SymbolToken(symbol, span, Separator.Empty, Separator.Empty, [issue]);
	}
}
