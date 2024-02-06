using System.Text;

namespace ScssNet.Lexing
{
	public class IdentifierToken: IToken
	{
		public string Text { get; }
		public int LineNumber { get; }
		public int ColumnNumber { get; }

		internal IdentifierToken(string text, int lineNumber, int columnNumber)
		{
			Text = text;
			LineNumber = lineNumber;
			ColumnNumber = columnNumber;
		}
	}

	internal class IdentifierParser
	{
		public IdentifierToken? Parse(SourceReader reader)
		{
			if (reader.End || !IsValidIdentifierChar(reader.Peek()))
				return null;

			var lineNumber = reader.LineNumber;
			var columnNumber = reader.ColumnNumber;

			var sb = new StringBuilder(reader.Read());
			while(!reader.End && IsValidIdentifierChar(reader.Peek()))
				sb.Append(reader.Read());

			return new IdentifierToken(sb.ToString(), lineNumber, columnNumber);
		}

		private bool IsValidIdentifierChar(char c) => char.IsLetter(c) || c == '_' || c == '-';
	}
}
