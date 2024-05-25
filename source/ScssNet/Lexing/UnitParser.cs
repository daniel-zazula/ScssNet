using System.Text;

namespace ScssNet.Lexing
{
	public class UnitToken: IToken
	{
		public decimal Amount { get; }
		public string Unit { get; }
		public SourceCoordinates Start { get; }
		public SourceCoordinates End { get; }

		internal UnitToken(decimal amount, string unit, SourceCoordinates start, SourceCoordinates end)
		{
			Amount = amount;
			Unit = unit;
			Start = start;
			End = end;
		}
	}

	internal class UnitParser
	{
		public UnitToken? Parse(ISourceReader reader)
		{
			if(reader.End || !char.IsDigit(reader.Peek()))
				return null;

			var startCoordinates = reader.GetCoordinates();

			var stringBuilder = new StringBuilder();
			stringBuilder.Append(reader.Read());

			ReadDigitsTo(reader, stringBuilder);
			if (reader.Peek() == '.')
			{
				stringBuilder.Append(reader.Read());
				ReadDigitsTo(reader, stringBuilder);
			}

			var amount = decimal.Parse(stringBuilder.ToString());

			stringBuilder.Clear();

			if(!reader.End && reader.Peek() == '%')
				stringBuilder.Append(reader.Read());
			else
				while(!reader.End && char.IsLetter(reader.Peek()))
					stringBuilder.Append(reader.Read());

			return new UnitToken(amount, stringBuilder.ToString(), startCoordinates, reader.GetCoordinates());
		}

		private static void ReadDigitsTo(ISourceReader reader, StringBuilder stringBuilder)
		{
			while(!reader.End && char.IsDigit(reader.Peek()))
				stringBuilder.Append(reader.Read());
		}
	}
}
