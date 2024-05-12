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
		private const string SingleLineCommentStart = "//";
		private const string MultiLineCommentStart = "/*";
		private const string MultiLineCommentEnd = "*/";

		public CommentToken? Parse(ISourceReader reader)
		{
			var commentStart = reader.Peek(2);
			if(commentStart != SingleLineCommentStart && commentStart != MultiLineCommentStart)
				return null;

			var startCoordinates = reader.GetCoordinates();

			var sb = new StringBuilder(reader.Read(2));
			while(!reader.End)
			{
				if(commentStart == SingleLineCommentStart && IsLineBreak(reader.Peek()))
					break;

				if (commentStart == MultiLineCommentStart && reader.Peek(2) == MultiLineCommentEnd)
				{
					sb.Append(reader.Read(2));
					break;
				}

				sb.Append(reader.Read());
			}

			return new CommentToken(sb.ToString(), startCoordinates, reader.GetCoordinates());

			static bool IsLineBreak(char nextChar) => nextChar == '\r' || nextChar == '\n';
		}
	}
}
