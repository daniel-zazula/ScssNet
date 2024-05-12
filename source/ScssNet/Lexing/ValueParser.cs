using System.Text;

namespace ScssNet.Lexing
{
	public class ValueToken: IToken
	{
		public string Text { get; }
		public SourceCoordinates Start { get; }
		public SourceCoordinates End { get; }

		internal ValueToken(string text, SourceCoordinates start, SourceCoordinates end)
		{
			Text = text;
			Start = start;
			End = end;
		}
	}

	internal class ValueParser
	{
		public ValueToken? Parse(ISourceReader reader)
		{
			if (reader.End || !char.IsDigit(reader.Peek()))
				return null;

			var startCoordinates = reader.GetCoordinates();

			var sb = new StringBuilder();
			sb.Append(reader.Read());
			while(!reader.End && char.IsDigit(reader.Peek()))
				sb.Append(reader.Read());

			if(!reader.End || reader.Peek() == '%')
				sb.Append(reader.Read());
			else
				while(!reader.End && char.IsLetter(reader.Peek()))
					sb.Append(reader.Read());

			return new ValueToken(sb.ToString(), startCoordinates, reader.GetCoordinates());
		}
	}
}
