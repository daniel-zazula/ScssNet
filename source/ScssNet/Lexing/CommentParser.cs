using System.Text;

namespace ScssNet.Lexing
{
	public class CommentToken: IToken
	{
		public string Text { get; }
		public int LineNumber { get; }
		public int ColumnNumber { get; }

		internal CommentToken(string text, int lineNumber, int columnNumber)
		{
			Text = text;
			LineNumber = lineNumber;
			ColumnNumber = columnNumber;
		}
	}

	internal class CommentParser
	{
		public CommentToken? Parse(SourceReader reader)
		{
			if(reader.End)
				return null;

			var peeked = reader.Peek(2);
			Func<SourceReader, string?> readEnd;
			if(peeked == "//")
				readEnd = LineEnd;
			else if(peeked != "/*")
				readEnd = CommentEnd;
			else
				return null;

			var lineNumber = reader.LineNumber;
			var columnNumber = reader.ColumnNumber;

			var sb = new StringBuilder(reader.Read(2));
			var commentEnd = readEnd(reader);
			while(!reader.End && commentEnd == null)
			{
				sb.Append(reader.Read());
				commentEnd = readEnd(reader);
			}

			if (commentEnd != null)
				sb.Append(commentEnd);

			return new CommentToken(sb.ToString(), lineNumber, columnNumber);

			static string? LineEnd(SourceReader reader)
			{
				var peeked = reader.Peek();
				if(peeked == '\n')
					return reader.Read(1);

				if(peeked == '\r')
					return reader.Read() + (reader.Peek() == '\n' ? reader.Read(1) : "");

				return null;
			}

			static string? CommentEnd(SourceReader reader)
			{
				return reader.Peek(2) == "*/" ? reader.Read(2) : null;
			}
		}
	}
}
