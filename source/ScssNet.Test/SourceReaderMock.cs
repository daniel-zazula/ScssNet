using System.Text;
using ScssNet.Lexing;

namespace ScssNet.Test;

internal class SourceReaderMock(string source) : ISourceReader
{
	private IEnumerable<char> remainingSource = source.AsEnumerable();

	private int LineNumber = 1;
	private int ColumnNumber = 1;

	public bool End => !remainingSource.Any();

	public char Peek()
	{
		return remainingSource.FirstOrDefault();
	}

	public string Peek(int count)
	{
		return string.Concat(remainingSource.Take(count));
	}

	public char Read()
	{
		return remainingSource.Any() ? AdvanceBuffer() : (char)0;
	}

	public string Read(int count)
	{
		var sb = new StringBuilder(count);
		for(int i = 0; i < count && remainingSource.Any(); i++)
			sb.Append(AdvanceBuffer());

		return sb.ToString();
	}

	private char AdvanceBuffer()
	{
		var readChar = remainingSource.First();
		remainingSource = remainingSource.Skip(1);

		if(readChar == '\n' || (readChar == '\r' && Peek() != '\n'))
		{
			LineNumber++;
			ColumnNumber = 1;
		}
		else
			ColumnNumber++;

		return readChar;
	}

	public SourceCoordinates GetCoordinates() => new(LineNumber, ColumnNumber);
}
