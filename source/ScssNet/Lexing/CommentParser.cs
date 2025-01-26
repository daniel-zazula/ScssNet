using System.Text;
using ScssNet.Tokens;

namespace ScssNet.Lexing;

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
