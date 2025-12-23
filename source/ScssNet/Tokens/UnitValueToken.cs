using ScssNet.SourceElements;

namespace ScssNet.Tokens;

public record UnitValueToken: IToken, ISeparatedToken, IValueToken
{
	public decimal Amount { get; }
	public string Unit { get; }

	public SourceCoordinates Start { get; }
	public SourceCoordinates End { get; }
	public Separator LeadingSeparator { get; }
	public Separator TrailingSeparator { get; }
	public IEnumerable<Issue> Issues => [];

	internal UnitValueToken
	(
		decimal amount, string unit, SourceCoordinates start, SourceCoordinates end, Separator before, Separator after
	)
	{
		Amount = amount;
		Unit = unit;
		Start = start;
		End = end;
		LeadingSeparator = before;
		TrailingSeparator = after;
	}
}
