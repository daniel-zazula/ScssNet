namespace ScssNet.Tokens;

public enum MediaQueryOperatorKeyword
{
	Not, Only
}

public record MediaQueryOperatorKeywordToken : KeywordToken<MediaQueryOperatorKeyword>
{
	internal MediaQueryOperatorKeywordToken
	(
		MediaQueryOperatorKeyword keyword, string text, SourceSpan span, Separator before, Separator after,
		ICollection<Issue>? issues = null
	) : base(keyword, text, span, before, after, issues)
	{
	}

	internal MediaQueryOperatorKeywordToken(MediaQueryOperatorKeyword keyword, IdentifierToken identifiertoken)
		: base(keyword, identifiertoken)
	{
	}

	private MediaQueryOperatorKeywordToken(SourceCoordinates coordinates, Issue issue)
		: base(coordinates, issue)
	{
	}

	internal static MediaQueryOperatorKeywordToken CreateMissing(SourceCoordinates coordinates)
	{
		var issue = CreateExpectedKeywordIssue();
		return new MediaQueryOperatorKeywordToken(coordinates, issue);
	}
}
