namespace ScssNet.Tokens;

public record UnitValueToken: IToken, ISeparatedToken, IValueToken
{
	public string Amount { get; }
	public string Unit { get; }

	public SourceSpan Span { get; }
	public Separator LeadingSeparator { get; }
	public Separator TrailingSeparator { get; }
	public Issues Issues => [];

	internal UnitValueToken
	(
		string amount, string unit, SourceSpan span, Separator before, Separator after
	)
	{
		Amount = amount;
		Unit = unit;
		Span = span;
		LeadingSeparator = before;
		TrailingSeparator = after;
	}
}
