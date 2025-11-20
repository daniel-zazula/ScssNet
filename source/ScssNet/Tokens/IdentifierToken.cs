namespace ScssNet.Tokens;

public record IdentifierToken: IToken, ISeparatedToken
{
	public string Text { get; }

	public SourceCoordinates Start { get; }
	public SourceCoordinates End { get; }
	public Separator? LeadingSeparator { get; }
	public Separator? TrailingSeparator { get; }
	public IEnumerable<Issue> Issues { get; }

	internal IdentifierToken
	(
		string text, SourceCoordinates start, SourceCoordinates end, Separator? before, Separator? after,
		ICollection<Issue>? issues = null
	)
	{
		Text = text;
		Start = start;
		End = end;
		LeadingSeparator = before;
		TrailingSeparator = after;
		Issues = issues ?? [];
	}

	internal static IdentifierToken CreateMissing(SourceCoordinates start)
	{
		return new IdentifierToken("", start, start, null, null, [new Issue(IssueType.Error, "Expected identifier")]);
	}
}
