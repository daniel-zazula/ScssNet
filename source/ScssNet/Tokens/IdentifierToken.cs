namespace ScssNet.Tokens;

public record IdentifierToken: IToken, ISeparatedToken, IValueToken
{
	public string Text { get; }

	public SourceSpan Span { get; }
	public Separator LeadingSeparator { get; }
	public Separator TrailingSeparator { get; }
	public IEnumerable<Issue> Issues { get; }

	internal IdentifierToken
	(
		string text, SourceSpan span, Separator before, Separator after,
		ICollection<Issue>? issues = null
	)
	{
		Text = text;
		Span = span;
		LeadingSeparator = before;
		TrailingSeparator = after;
		Issues = issues ?? [];
	}

	internal IdentifierToken(SourceCoordinates coordinates, Issue issue)
	{
		Text = "";
		Span = new SourceSpan(coordinates, coordinates);
		LeadingSeparator = Separator.Empty;
		TrailingSeparator = Separator.Empty;
		Issues = [issue];
	}
}
