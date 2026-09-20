namespace ScssNet.Tokens;

public record HashValueToken: IToken, ISeparatedToken, IValueToken
{
	public string Value { get; }

	public SourceSpan Span { get; }
	public Separator LeadingSeparator { get; }
	public Separator TrailingSeparator { get; }
	public Issues Issues => [];

	public HashValueToken
	(
		string value, SourceSpan span, Separator before, Separator after
	)
	{
		Value = value;
		Span = span;
		LeadingSeparator = before;
		TrailingSeparator = after;
	}
}
