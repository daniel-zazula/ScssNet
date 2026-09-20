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
	internal SymbolToken(Symbol symbol, SourceCoordinates coordinates, Issue issue)
	{
		Symbol = symbol;
		Span = new SourceSpan(coordinates, coordinates);
		LeadingSeparator = Separator.Empty;
		TrailingSeparator = Separator.Empty;
		Issues = [issue];
	}
}
