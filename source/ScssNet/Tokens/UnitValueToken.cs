namespace ScssNet.Tokens;

public class UnitValueToken : IToken
{
	public decimal Amount { get; }
	public string Unit { get; }

	public SourceCoordinates Start { get; }
	public SourceCoordinates End { get; }
	public IEnumerable<Issue> Issues => [];

	internal UnitValueToken(decimal amount, string unit, SourceCoordinates start, SourceCoordinates end)
	{
		Amount = amount;
		Unit = unit;
		Start = start;
		End = end;
	}
}
