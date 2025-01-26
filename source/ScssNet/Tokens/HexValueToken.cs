using ScssNet.Parsing;

namespace ScssNet.Tokens;

public class HexValueToken : IValueToken
{
	public string Value { get; }

	public SourceCoordinates Start { get; }
	public SourceCoordinates End { get; }
	public IEnumerable<Issue> Issues => [];

	internal HexValueToken(string value, SourceCoordinates start, SourceCoordinates end)
	{
		Value = value;
		Start = start;
		End = end;
	}
}
