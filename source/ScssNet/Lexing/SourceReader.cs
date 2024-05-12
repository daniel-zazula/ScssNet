using System.IO;
using System.Text;

namespace ScssNet.Lexing
{
	internal interface ISourceReader
	{
		bool End { get; }

		SourceCoordinates GetCoordinates();
		char Peek();
		string Peek(int count);
		char Read();
		string Read(int count);
	}

	internal class SourceReader(TextReader TextReader) : ISourceReader
	{
		public bool End => BufferIsEmpty && TextReaderIsEmpty;

		private const char NullChar = (char)0;

		private int LineNumber = 1;
		private int ColumnNumber = 1;

		private readonly Queue<char> Buffer = new();
		private bool BufferIsEmpty => Buffer.Count == 0;
		private bool TextReaderIsEmpty => TextReader.Peek() == -1;

		public char Peek()
		{
			FillBufferIfNecessary(1);

			return BufferIsEmpty ? NullChar : Buffer.Peek();
		}

		public string Peek(int count)
		{
			if(count < 1)
				throw new ArgumentException($"{nameof(count)} should be greater than 0.");

			FillBufferIfNecessary(count);

			return BufferIsEmpty ? string.Empty : string.Concat(Buffer.Take(count));
		}

		public char Read()
		{
			FillBufferIfNecessary(1);
			return BufferIsEmpty ? NullChar : AdvanceBuffer();
		}

		public string Read(int count)
		{
			if(count < 1)
				throw new ArgumentException($"{nameof(count)} should be greater than 0.");

			FillBufferIfNecessary(count);
			if (BufferIsEmpty)
				return string.Empty;

			var sb = new StringBuilder(count);
			for(int i = 0; i < count && !End; i++)
				sb.Append(AdvanceBuffer());

			return sb.ToString();
		}

		public SourceCoordinates GetCoordinates() => new(LineNumber, ColumnNumber);

		private void FillBufferIfNecessary(int count)
		{
			if(TextReaderIsEmpty || Buffer.Count >= count)
				return;

			const int readSize = 128;
			var readBuffer = new char[readSize];
			var readCount = TextReader.Read(readBuffer, 0, readSize);

			foreach(var readChar in readBuffer.Take(readCount))
				Buffer.Enqueue(readChar);
		}

		private char AdvanceBuffer()
		{
			var readChar = Buffer.Dequeue();
			if(readChar == '\n' || (readChar == '\r' && Buffer.Peek() != '\n'))
			{
				LineNumber++;
				ColumnNumber = 1;
			}
			else
				ColumnNumber++;

			return readChar;
		}
	}
}
