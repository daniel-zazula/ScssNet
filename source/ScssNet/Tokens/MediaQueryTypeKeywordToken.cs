using ScssNet.Structures;

namespace ScssNet.Tokens;

public enum MediaQueryTypeKeyword
{
	All, Screen, Print
}

public record MediaQueryTypeKeywordToken : KeywordToken<MediaQueryTypeKeyword>, IMediaQueryValue
{
	internal MediaQueryTypeKeywordToken
	(
		MediaQueryTypeKeyword keyword, string text, SourceSpan span, Separator before, Separator after,
		ICollection<Issue>? issues = null
	) : base(keyword, text, span, before, after, issues)
	{
	}

	internal MediaQueryTypeKeywordToken(MediaQueryTypeKeyword keyword, IdentifierToken identifiertoken)
		: base(keyword, identifiertoken)
	{
	}

	private MediaQueryTypeKeywordToken(SourceCoordinates coordinates, Issue issue)
		: base(coordinates, issue)
	{
	}

	internal static MediaQueryTypeKeywordToken CreateMissing(SourceCoordinates coordinates)
	{
		var issue = CreateExpectedKeywordIssue();
		return new MediaQueryTypeKeywordToken(coordinates, issue);
	}
}
