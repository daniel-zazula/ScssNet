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

	internal static IdentifierToken CreateMissing(SourceCoordinates coordinates)
	{
		var span = new SourceSpan(coordinates, coordinates);
		var issue = new Issue(IssueType.Error, "Expected identifier");
		return new IdentifierToken("", span, Separator.Empty, Separator.Empty, [issue]);
	}
}
