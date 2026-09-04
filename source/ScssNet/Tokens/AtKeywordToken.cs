namespace ScssNet.Tokens;

public enum AtKeyword
{
	Charset, Import, Media
}

public record AtKeywordToken: KeywordToken<AtKeyword>
{
	public AtKeywordToken
	(
		AtKeyword keyword, string text, SourceSpan span, Separator before, Separator after,
		ICollection<Issue>? issues = null
	): base(keyword, text, span, before, after, issues)
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
