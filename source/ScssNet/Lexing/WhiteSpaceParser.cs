using System.Text;
using ScssNet.Tokens;

namespace ScssNet.Lexing;

internal class WhiteSpaceParser
{
	public WhiteSpaceToken? Parse(ISourceReader reader)
	{
		if (reader.End || !char.IsWhiteSpace(reader.Peek()))
			return null;

		var startCoordinates = reader.GetCoordinates();

		var sb = new StringBuilder();
		sb.Append(reader.Read());
		while(!reader.End && char.IsWhiteSpace(reader.Peek()))
			sb.Append(reader.Read());

		var span = new SourceSpan(startCoordinates, reader.GetCoordinates());
		return new WhiteSpaceToken(sb.ToString(), span);
	}
}
