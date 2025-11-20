namespace ScssNet.Tokens;

public record StringToken: IToken, ISeparatedToken
{
	public string Text { get; }

	public SourceCoordinates Start { get; }
	public SourceCoordinates End { get; }
	public Separator? LeadingSeparator { get; }
	public Separator? TrailingSeparator { get; }
	public IEnumerable<Issue> Issues { get; }

	internal StringToken
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

	internal static StringToken CreateMissing(SourceCoordinates start)
	{
		return new StringToken("", start, start, null, null, [new Issue(IssueType.Error, "Expected string")]);
	}
}
