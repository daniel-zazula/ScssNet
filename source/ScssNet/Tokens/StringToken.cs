namespace ScssNet.Tokens;

public record StringToken: IToken, ISeparatedToken, IValueToken
{
	public string Text { get; }

	public SourceSpan Span { get; }
	public Separator LeadingSeparator { get; }
	public Separator TrailingSeparator { get; }
	public IEnumerable<Issue> Issues { get; }

	internal StringToken
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

	internal static StringToken CreateMissing(SourceCoordinates coordinates)
	{
		var span = new SourceSpan(coordinates, coordinates);
		var issue = new Issue(IssueType.Error, "Expected string");
		return new StringToken("", span, Separator.Empty, Separator.Empty, [issue]);
	}
}
