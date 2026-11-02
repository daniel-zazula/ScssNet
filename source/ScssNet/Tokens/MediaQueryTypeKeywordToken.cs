using ScssNet.Lexing;
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

	protected MediaQueryTypeKeywordToken(SourceCoordinates coordinates, Issue issue)
		: base(coordinates, issue)
	{
	}
}

public record MissingMediaQueryTypeKeywordToken : MediaQueryTypeKeywordToken, IMissingToken
{
	public MissingMediaQueryTypeKeywordToken(SourceCoordinates coordinates)
		: base(coordinates, Issue.CreateExpectedOneOf<MediaQueryTypeKeyword>())
	{
	}
}
