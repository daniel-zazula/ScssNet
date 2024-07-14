using System.Text;

namespace ScssNet.Lexing
{
	public class NumberToken: IToken
	{
		public decimal Number { get; }
		private readonly string NumberString;

		public SourceCoordinates Start { get; }
		public SourceCoordinates End { get; }
		public IEnumerable<Issue> Issues => [];

		internal NumberToken(decimal number, string numberString, SourceCoordinates start, SourceCoordinates end)
		{
			Number = number;
			NumberString = numberString;
			Start = start;
			End = end;
		}

		public override string ToString() => NumberString;
	}

	internal class NumberParser
	{
		public NumberToken? Parse(ISourceReader reader)
		{
			if(!IsUnitStart(reader))
				return null;

			var startCoordinates = reader.GetCoordinates();

			var stringBuilder = new StringBuilder();
			stringBuilder.Append(reader.Read());

			ReadDigitsTo(reader, stringBuilder);
			if(reader.Peek() == '.')
			{
				stringBuilder.Append(reader.Read());
				ReadDigitsTo(reader, stringBuilder);
			}

			var numberString = stringBuilder.ToString();

			return new NumberToken(decimal.Parse(numberString), numberString, startCoordinates, reader.GetCoordinates());
		}

		private static bool IsUnitStart(ISourceReader reader)
		{
			if(reader.End)
				return false;

			var peeked = reader.Peek(2);
			return char.IsDigit(peeked[0]) || (peeked[0] == '-' && char.IsDigit(peeked[1]));
		}

		private static void ReadDigitsTo(ISourceReader reader, StringBuilder stringBuilder)
		{
			while(!reader.End && char.IsDigit(reader.Peek()))
				stringBuilder.Append(reader.Read());
		}
	}
}
