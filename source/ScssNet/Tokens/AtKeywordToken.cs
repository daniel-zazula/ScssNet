namespace ScssNet.Tokens;

public enum AtKeyword
{
	Charset, Import, Media
}

public record AtKeywordToken: KeywordToken<AtKeyword>
{
	public AtKeywordToken
	(
		AtKeyword keyword, string text, SourceCoordinates start, SourceCoordinates end, Separator before, Separator after,
		ICollection<Issue>? issues = null
	): base(keyword, text, start, end, before, after, issues)
	{
	}

	internal AtKeywordToken(AtKeyword keyword, IdentifierToken identifiertoken)
		: base(keyword, identifiertoken)
	{
	}

	private AtKeywordToken(SourceCoordinates coordinates, Issue issue)
		: base(coordinates, issue)
	{
	}

	internal static AtKeywordToken CreateMissing(SourceCoordinates coordinates)
	{
		var issue = CreateExpectedKeywordIssue();
		return new AtKeywordToken(coordinates, issue);
	}
}
