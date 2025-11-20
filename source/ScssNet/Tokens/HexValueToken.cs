using ScssNet.SourceElements;

namespace ScssNet.Tokens;

public record HexValueToken: IToken, ISeparatedToken, IValueToken
{
	public string Value { get; }

	public SourceCoordinates Start { get; }
	public SourceCoordinates End { get; }
	public Separator? LeadingSeparator { get; }
	public Separator? TrailingSeparator { get; }
	public IEnumerable<Issue> Issues => [];

	public HexValueToken
	(
		string value, SourceCoordinates start, SourceCoordinates end, Separator? before, Separator? after
	)
	{
		Value = value;
		Start = start;
		End = end;
		LeadingSeparator = before;
		TrailingSeparator = after;
	}
}
