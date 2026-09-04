namespace ScssNet.Tokens;

public enum ValueKeyword
{
	Important
}

public record ValueKeywordToken : KeywordToken<ValueKeyword>
{
	public ValueKeywordToken
	(
		ValueKeyword keyword, string text, SourceSpan span, Separator before, Separator after,
		ICollection<Issue>? issues = null
	) : base(keyword, text, span, before, after, issues)
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
