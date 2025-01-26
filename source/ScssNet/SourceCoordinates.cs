namespace ScssNet;

public readonly record struct SourceCoordinates
{
	public int LineNumber { get; }
	public int ColumnNumber { get; }

	internal SourceCoordinates(int lineNumber, int columnNumber)
	{
		LineNumber = lineNumber;
		ColumnNumber = columnNumber;
	}
}
