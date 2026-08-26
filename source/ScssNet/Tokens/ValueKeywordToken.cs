namespace ScssNet.Tokens;

public enum ValueKeyword
{
	Important
}

public record ValueKeywordToken : KeywordToken<ValueKeyword>
{
	public ValueKeywordToken
	(
		ValueKeyword keyword, string text, SourceCoordinates start, SourceCoordinates end, Separator before, Separator after,
		ICollection<Issue>? issues = null
	) : base(keyword, text, start, end, before, after, issues)
	{
	}

	internal ValueKeywordToken(ValueKeyword keyword, IdentifierToken identifiertoken)
		: base(keyword, identifiertoken)
	{
	}

	private ValueKeywordToken(SourceCoordinates coordinates, Issue issue)
		: base(coordinates, issue)
	{
	}

	internal static ValueKeywordToken CreateMissing(SourceCoordinates coordinates)
	{
		var issue = CreateExpectedKeywordIssue();
		return new ValueKeywordToken(coordinates, issue);
	}
}
