using System.IO;
using System.Text;
using ScssNet.Lexing;
using Shouldly;

namespace ScssNet.Test.Lexing
{
	[TestClass]
	public class SourceReaderTests
	{
		private const string SampleSource = "abcdefghijklmnopqrstuvwxyz 123";
		private const int BufferSize = 128;

		[TestMethod]
		public void PeekShouldNotAdvanceTheStream()
		{
			var sourceReader = new SourceReader(new StringReader(SampleSource));

			sourceReader.Peek().ShouldBe(SampleSource[0]);
			sourceReader.Peek().ShouldBe(SampleSource[0]);
		}

		[TestMethod]
		public void PeekManyShouldNotAdvanceTheStream()
		{
			var sourceReader = new SourceReader(new StringReader(SampleSource));

			sourceReader.Peek(2).ShouldBe(SampleSource[0..2]);
			sourceReader.Peek(5).ShouldBe(SampleSource[0..5]);
		}

		[TestMethod]
		public void ReadShouldAdvanceTheStream()
		{
			var sourceReader = new SourceReader(new StringReader(SampleSource));

			sourceReader.Read().ShouldBe(SampleSource[0]);
			sourceReader.Peek().ShouldBe(SampleSource[1]);
		}

		[TestMethod]
		public void ReadManyShouldAdvanceTheStream()
		{
			var sourceReader = new SourceReader(new StringReader(SampleSource));

			sourceReader.Read(5).ShouldBe(SampleSource[0..5]);
			sourceReader.Peek().ShouldBe(SampleSource[5]);
		}

		[TestMethod]
		public void SourceCoordinatesShouldStartAtOneOne()
		{
			var sourceReader = new SourceReader(new StringReader(SampleSource));

			var coordinates = sourceReader.GetCoordinates();
			coordinates.ShouldBe(new SourceCoordinates(1, 1));
		}

		[TestMethod]
		public void SourceCoordinatesColumnShouldIncreaseOnRead()
		{
			var sourceReader = new SourceReader(new StringReader(SampleSource));

			const int readCount = 5;
			sourceReader.Read(readCount);

			var coordinates = sourceReader.GetCoordinates();
			coordinates.ShouldBe(new SourceCoordinates(1, 1 + readCount));
		}

		[TestMethod]
		public void SourceCoordinatesLineShouldIncreaseOnRead()
		{
			const string lineBreak = "\r\n";
			var twoLineSource = GetTwoLineSampleSource(lineBreak);
			var sourceReader = new SourceReader(new StringReader(twoLineSource));

			sourceReader.Read(SampleSource.Length + lineBreak.Length);

			var coordinates = sourceReader.GetCoordinates();
			coordinates.ShouldBe(new SourceCoordinates(2, 1));
		}

		[TestMethod]
		public void SourceCoordinatesColumnAndLineShouldIncreaseOnRead()
		{
			const string lineBreak = "\r\n";
			var twoLineSource = GetTwoLineSampleSource(lineBreak);
			var sourceReader = new SourceReader(new StringReader(twoLineSource));

			const int readCount = 5;
			sourceReader.Read(SampleSource.Length + lineBreak.Length + readCount);

			var coordinates = sourceReader.GetCoordinates();
			coordinates.ShouldBe(new SourceCoordinates(2, 1 + readCount));
		}

		[TestMethod]
		public void SourceCoordinatesColumnAndLineShouldIncreaseOnPeek()
		{
			var sourceReader = new SourceReader(new StringReader(SampleSource));

			const int readCount = 5;
			sourceReader.Peek(readCount);

			var coordinates = sourceReader.GetCoordinates();
			coordinates.ShouldBe(new SourceCoordinates(1, 1));
		}

		[TestMethod]
		public void PeekBeyondBufferShouldFillBuffer()
		{
			var sourceReader = new SourceReader(new StringReader(CreateSampleSourceBiggerThanBuffer()));

			//Reads almost the full buffer
			int readCount = SampleSource.Length;
			while(readCount + SampleSource.Length < BufferSize)
				readCount += SampleSource.Length;

			sourceReader.Read(readCount);

			sourceReader.Peek(SampleSource.Length).ShouldBe(SampleSource);
		}

		[TestMethod()]
		public void ReadBeyondBufferShouldFillBuffer()
		{
			var sourceReader = new SourceReader(new StringReader(CreateSampleSourceBiggerThanBuffer()));

			//Reads almost the full buffer
			int readCount = SampleSource.Length;
			while(readCount + SampleSource.Length < BufferSize)
				readCount += SampleSource.Length;

			sourceReader.Read(readCount);

			sourceReader.Read(SampleSource.Length).ShouldBe(SampleSource);
			sourceReader.Peek().ShouldBe(SampleSource[0]);
		}

		private static string GetTwoLineSampleSource(string lineBreak)
		{
			return $"{SampleSource}{lineBreak}{SampleSource}";
		}

		private static string CreateSampleSourceBiggerThanBuffer()
		{
			var sb = new StringBuilder(BufferSize + SampleSource.Length * 2);
			while(sb.Length < BufferSize)
				sb.Append(SampleSource);

			sb.Append(SampleSource); //One extra

			return sb.ToString();
		}
	}
}
