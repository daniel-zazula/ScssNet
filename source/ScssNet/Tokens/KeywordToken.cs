using ScssNet.Lexing;

namespace ScssNet.Tokens;

public abstract record KeywordToken<T>: IToken, ISeparatedToken
	where T : Enum
{
	public T? Keyword { get; }

	public string Text { get; }

	public SourceSpan Span { get; }
	public Separator LeadingSeparator { get; }
	public Separator TrailingSeparator { get; }
	public IEnumerable<Issue> Issues { get; }

	protected KeywordToken
	(
		T keyword, string text, SourceSpan span, Separator before, Separator after,
		ICollection<Issue>? issues = null
	)
	{
		Keyword = keyword;
		Text = text;
		Span = span;
		LeadingSeparator = before;
		TrailingSeparator = after;
		Issues = issues ?? [];
	}

	protected KeywordToken(T keyword, IdentifierToken identifiertoken)
	{
		Keyword = keyword;
		Text = identifiertoken.Text;
		Span = identifiertoken.Span;
		LeadingSeparator = identifiertoken.LeadingSeparator;
		TrailingSeparator = identifiertoken.TrailingSeparator;
		Issues = identifiertoken.Issues;
	}

	protected KeywordToken(SourceCoordinates coordinates, Issue issue)
	{
		Keyword = default;
		Text = "";
		Span = new SourceSpan(coordinates, coordinates);
		LeadingSeparator = Separator.Empty;
		TrailingSeparator = Separator.Empty;
		Issues = [issue];
	}
}
