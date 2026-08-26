namespace ScssNet.Tokens;

public abstract record KeywordToken<T>: IToken, ISeparatedToken
	where T : Enum
{
	public T? Keyword { get; }

	public string Text { get; }

	public SourceCoordinates Start { get; }
	public SourceCoordinates End { get; }
	public Separator LeadingSeparator { get; }
	public Separator TrailingSeparator { get; }
	public IEnumerable<Issue> Issues { get; }

	protected KeywordToken
	(
		T keyword, string text, SourceCoordinates start, SourceCoordinates end, Separator before, Separator after,
		ICollection<Issue>? issues = null
	)
	{
		Keyword = keyword;
		Text = text;
		Start = start;
		End = end;
		LeadingSeparator = before;
		TrailingSeparator = after;
		Issues = issues ?? [];
	}

	protected KeywordToken(T keyword, IdentifierToken identifiertoken)
	{
		Keyword = keyword;
		Text = identifiertoken.Text;
		Start = identifiertoken.Start;
		End = identifiertoken.End;
		LeadingSeparator = identifiertoken.LeadingSeparator;
		TrailingSeparator = identifiertoken.TrailingSeparator;
		Issues = identifiertoken.Issues;
	}

	protected KeywordToken(SourceCoordinates coordinates, Issue issue)
	{
		Keyword = default;
		Text = "";
		Start = coordinates;
		End = coordinates;
		LeadingSeparator = Separator.Empty;
		TrailingSeparator = Separator.Empty;
		Issues = [issue];
	}

	protected static Issue CreateExpectedKeywordIssue()
	{
		var keywords = (T[])Enum.GetValues(typeof(T));
		return new Issue(IssueType.Error, "Expected one of the keywords: " + string.Join(", ", keywords));
	}
}
