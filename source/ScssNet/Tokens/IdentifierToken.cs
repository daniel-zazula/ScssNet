using ScssNet.Parsing;

namespace ScssNet.Tokens;

public class IdentifierToken : IValueToken
{
	public string Text { get; }

	public SourceCoordinates Start { get; }
	public SourceCoordinates End { get; }
	public IEnumerable<Issue> Issues => _issues;

	private readonly ICollection<Issue> _issues = [];

	internal IdentifierToken(string text, SourceCoordinates start, SourceCoordinates end) : this(text, start, end, []) { }

	private IdentifierToken(string text, SourceCoordinates start, SourceCoordinates end, ICollection<Issue> issues)
	{
		Text = text;
		Start = start;
		End = end;
		_issues = issues;
	}

	internal static IdentifierToken CreateMissing(SourceCoordinates start)
	{
		return new IdentifierToken("", start, start, [new Issue(IssueType.Error, "Expected identifier")]);
	}
}
