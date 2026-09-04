namespace ScssNet.Tokens;

public record UnitValueToken: IToken, ISeparatedToken, IValueToken
{
	public decimal Amount { get; }
	public string Unit { get; }

	public SourceSpan Span { get; }
	public Separator LeadingSeparator { get; }
	public Separator TrailingSeparator { get; }
	public IEnumerable<Issue> Issues => [];

	internal UnitValueToken
	(
		decimal amount, string unit, SourceSpan span, Separator before, Separator after
	)
	{
		Amount = amount;
		Unit = unit;
		Span = span;
		LeadingSeparator = before;
		TrailingSeparator = after;
	}
}
