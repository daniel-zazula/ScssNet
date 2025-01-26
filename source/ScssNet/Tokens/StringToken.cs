namespace ScssNet.Tokens;

public class StringToken : IToken
{
	public string Text { get; }

	public SourceCoordinates Start { get; }
	public SourceCoordinates End { get; }
	public IEnumerable<Issue> Issues => _issues;

	private readonly ICollection<Issue> _issues = [];

	internal StringToken(string text, SourceCoordinates start, SourceCoordinates end) : this(text, start, end, []) { }

	private StringToken(string text, SourceCoordinates start, SourceCoordinates end, ICollection<Issue> issues)
	{
		Text = text;
		Start = start;
		End = end;
		_issues = issues;
	}

	internal static StringToken CreateMissing(SourceCoordinates start)
	{
		return new StringToken("", start, start, [new Issue(IssueType.Error, "Expected string")]);
	}
}
