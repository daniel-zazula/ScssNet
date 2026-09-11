namespace ScssNet.Test.ElementCreation;

internal static class SourceSpanCreation
{
	extension(SourceSpan)
	{
		internal static SourceSpan Create(ISourceElement? predecessor = null, int length = 1)
		{
			var predecessorEnd = predecessor?.Span.Start;
			var startingLine = predecessorEnd?.LineNumber ?? 1;
			var startingColumn = predecessorEnd?.ColumnNumber + 1 ?? 1;
			var start = new SourceCoordinates(startingLine, startingColumn);
			var end = new SourceCoordinates(startingLine, startingColumn + length - 1);
			return new SourceSpan(start, end);
		}
	}
}
