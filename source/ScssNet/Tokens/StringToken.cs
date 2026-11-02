using ScssNet.Lexing;

namespace ScssNet.Tokens;

public record StringToken: IToken, ISeparatedToken, IValueToken
{
	public string Text { get; }

	public SourceSpan Span { get; }
	public Separator LeadingSeparator { get; }
	public Separator TrailingSeparator { get; }
	public Issues Issues { get; }

	internal StringToken
	(
		string text, SourceSpan span, Separator before, Separator after,
		ICollection<Issue>? issues = null
	)
	{
		Text = text;
		Span = span;
		LeadingSeparator = before;
		TrailingSeparator = after;
		Issues = issues ?? [];
	}

	protected StringToken(SourceCoordinates coordinates, Issue issue)
	{
		Text = string.Empty;
		Span = new SourceSpan(coordinates, coordinates);
		LeadingSeparator = Separator.Empty;
		TrailingSeparator = Separator.Empty;
		Issues = [issue];
	}
}

public record MissingStringToken : StringToken, IMissingToken
{
	public MissingStringToken(SourceCoordinates coordinates)
		: base(coordinates, Issue.CreateExpected("string"))
	{
	}
}
