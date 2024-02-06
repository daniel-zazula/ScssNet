using System.Text;

namespace ScssNet.Lexing
{
	public class ValueToken: IToken
	{
		public string Text { get; }
		public int LineNumber { get; }
		public int ColumnNumber { get; }

		internal ValueToken(string text, int lineNumber, int columnNumber)
		{
			Text = text;
			LineNumber = lineNumber;
			ColumnNumber = columnNumber;
		}
	}

	internal class ValueParser
	{
		public ValueToken? Parse(SourceReader reader)
		{
			if (reader.End || !char.IsDigit(reader.Peek()))
				return null;

			var lineNumber = reader.LineNumber;
			var columnNumber = reader.ColumnNumber;

			var sb = new StringBuilder(reader.Read());
			while(!reader.End && char.IsDigit(reader.Peek()))
				sb.Append(reader.Read());

			if(!reader.End || reader.Peek() == '%')
				sb.Append(reader.Read());
			else
				while(!reader.End && char.IsLetter(reader.Peek()))
					sb.Append(reader.Read());

			return new ValueToken(sb.ToString(), lineNumber, columnNumber);
		}
	}
}
