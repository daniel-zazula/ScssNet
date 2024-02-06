using System.Text;

namespace ScssNet.Lexing
{
	public class StringToken: IToken
	{
		public string Text { get; }
		public int LineNumber { get; }
		public int ColumnNumber { get; }

		internal StringToken(string text, int lineNumber, int columnNumber)
		{
			Text = text;
			LineNumber = lineNumber;
			ColumnNumber = columnNumber;
		}
	}

	internal class StringParser
	{
		public StringToken? Parse(SourceReader reader)
		{
			if(reader.End || reader.Peek() != '\'')
				return null;

			var lineNumber = reader.LineNumber;
			var columnNumber = reader.ColumnNumber;

			var sb = new StringBuilder(reader.Read());
			while(!reader.End)
			{
				if (reader.Peek() == '\'' )
				{
					sb.Append(reader.Read());
					break;
				}
				sb.Append(reader.Read());
			};

			return new StringToken(sb.ToString(), lineNumber, columnNumber);
		}
	}
}
