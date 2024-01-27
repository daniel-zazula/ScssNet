using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ScssNet.Lexing
{
	internal class SourceReader(TextReader TextReader)
	{
		public int LineNumber { get; private set; } = 1;
		public int ColumnNumber { get; private set; } = 1;
		private readonly Queue<char> Buffer = new();

		public bool End => BufferIsEmpty && TextReaderIsEmpty;
		private bool BufferIsEmpty => Buffer.Count == 0;
		private bool TextReaderIsEmpty => TextReader.Peek() == -1;

		public char Peek()
		{
			if(BufferIsEmpty)
			{
				if(TextReaderIsEmpty)
					return (char)0;
				else
					FillBuffer();
			}

			return Buffer.Peek();
		}

		public string Peek(int count)
		{
			if(count < 1)
				throw new ArgumentException($"{nameof(count)} should be greater than 0.");

			if(Buffer.Count < count)
				FillBuffer();

			return string.Concat(Buffer.Take(count));
		}

		public char Read()
		{
			var nextChar = Peek();
			AdvanceBuffer();
			return nextChar;
		}

		public string Read(int count)
		{
			if(count < 1)
				throw new ArgumentException($"{nameof(count)} should be greater than 0.");

			var text = Peek(count);

			for(int i = 0; i < count && Buffer.Count > 0; i++)
				AdvanceBuffer();

			return text;
		}

		private void FillBuffer()
		{
			if(TextReaderIsEmpty)
				return;

			const int readSize = 128;
			var readBuffer = new char[readSize];
			var readCount = TextReader.Read(readBuffer, 0, readSize);

			foreach(var readChar in readBuffer.Take(readCount))
				Buffer.Enqueue(readChar);
		}

		private void AdvanceBuffer()
		{
			if(Buffer.Peek() == '\n' || (Buffer.Peek() == '\r' && Buffer.Skip(1).FirstOrDefault() != '\n'))
			{
				LineNumber++;
				ColumnNumber = 1;
			}
			else
				ColumnNumber++;

			Buffer.Dequeue();
		}
	}
}
