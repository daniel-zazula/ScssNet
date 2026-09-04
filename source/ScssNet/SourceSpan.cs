namespace ScssNet;

public readonly record struct SourceSpan
{
	public SourceCoordinates Start { get; }
	public SourceCoordinates End { get; }

	internal SourceSpan(SourceCoordinates start, SourceCoordinates end)
	{
		Start = start;
		End = end;
	}

	internal static SourceSpan From(params ISourceElement?[] sourceElements)
	{
		return From((IEnumerable<ISourceElement?>)sourceElements);
	}


	internal static SourceSpan From(IEnumerable<ISourceElement?> sourceElements)
	{
		var nonNulls = sourceElements.Where(se => se is not null);
		var start = nonNulls.First()!.Span.Start;
		var end = nonNulls.Last()!.Span.End;
		return new SourceSpan(start, end);
	}
}

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
