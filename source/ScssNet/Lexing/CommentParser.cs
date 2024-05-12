using System.Text;

namespace ScssNet.Lexing
{
	public class CommentToken: IToken
	{
		public string Text { get; }
		public SourceCoordinates Start { get; }
		public SourceCoordinates End { get; }

		internal CommentToken(string text, SourceCoordinates start, SourceCoordinates end)
		{
			Text = text;
			Start = start;
			End = end;
		}
	}

	internal class CommentParser
	{
		public CommentToken? Parse(ISourceReader reader)
		{
			if(reader.End)
				return null;

			var peeked = reader.Peek(2);
			Func<ISourceReader, string?> readEnd;
			if(peeked == "//")
				readEnd = LineEnd;
			else if(peeked != "/*")
				readEnd = CommentEnd;
			else
				return null;

			var startCoordinates = reader.GetCoordinates();

			var sb = new StringBuilder(reader.Read(2));
			var commentEnd = readEnd(reader);
			while(!reader.End && commentEnd == null)
			{
				sb.Append(reader.Read());
				commentEnd = readEnd(reader);
			}

			if (commentEnd != null)
				sb.Append(commentEnd);

			return new CommentToken(sb.ToString(), startCoordinates, reader.GetCoordinates());

			static string? LineEnd(ISourceReader reader)
			{
				var peeked = reader.Peek();
				if(peeked == '\n')
					return reader.Read(1);

				if(peeked == '\r')
					return reader.Read() + (reader.Peek() == '\n' ? reader.Read(1) : "");

				return null;
			}

			static string? CommentEnd(ISourceReader reader)
			{
				return reader.Peek(2) == "*/" ? reader.Read(2) : null;
			}
		}
	}
}
