using System.Text;
using ScssNet.Tokens;

namespace ScssNet.Lexing;

internal class IdentifierParser
{
	public IdentifierToken? Parse
	(
		ISourceReader reader, Separator? leadingSeparator, Func<Separator?> getTrailingSeparator
	)
	{
		if(reader.End || !IsIdentifierStart(reader.Peek(3)))
			return null;

		var startCoordinates = reader.GetCoordinates();

		var sb = new StringBuilder();
		sb.Append(reader.Read());
		while(!reader.End && IsIdentifierChar(reader.Peek()))
			sb.Append(reader.Read());

		return new IdentifierToken
		(
			sb.ToString(), startCoordinates, reader.GetCoordinates(), leadingSeparator, getTrailingSeparator()
		);
	}

	private bool IsIdentifierStart(string peeked)
	{
		var firstChar = peeked[0];
		return char.IsLetter(firstChar) || firstChar == '_'
			|| (peeked.Length > 1 && firstChar == '-' && char.IsLetter(peeked[1]))
			|| (peeked.Length > 2 && peeked.Substring(2) == "--" && char.IsLetter(peeked[2]));
	}

	private bool IsIdentifierChar(char c)
	{
		return char.IsLetter(c) || c == '_' || c == '-';
	}
}
