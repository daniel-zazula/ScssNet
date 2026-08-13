namespace ScssNet.Tokens;

public enum Keyword
{
	Charset, Import, Important, Media
}

public record KeywordToken : IToken, ISeparatedToken
{
	Keyword Keyword { get; }

	public string Text { get; }

	public SourceCoordinates Start { get; }
	public SourceCoordinates End { get; }
	public Separator LeadingSeparator { get; }
	public Separator TrailingSeparator { get; }
	public IEnumerable<Issue> Issues { get; }

	internal KeywordToken
	(
		Keyword keyword, string text, SourceCoordinates start, SourceCoordinates end, Separator before, Separator after,
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

	internal KeywordToken(Keyword keyword, IdentifierToken identifiertoken)
	{
		Keyword = keyword;
		Text = identifiertoken.Text;
		Start = identifiertoken.Start;
		End = identifiertoken.End;
		LeadingSeparator = identifiertoken.LeadingSeparator;
		TrailingSeparator = identifiertoken.TrailingSeparator;
		Issues = identifiertoken.Issues;
	}

	internal static KeywordToken CreateMissing(Keyword keyword, SourceCoordinates coordinates)
	{
		var issue = new Issue(IssueType.Error, "Expected identifier");
		return new KeywordToken(keyword, "", coordinates, coordinates, Separator.Empty, Separator.Empty, [issue]);
	}
}
