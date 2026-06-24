using System.Text;
using ScssNet.Tokens;

namespace ScssNet.Lexing;

internal class IdentifierParser
{
	public IdentifierToken? Parse
	(
		ISourceReader reader, Separator leadingSeparator, Func<Separator> getTrailingSeparator
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
		// One or two dashes is only a valid identifier start if followed by a letter or underscore.
		// ie -10 is not an identifier, but -foo and --bar are.

		var firstChar = peeked.Length > 0 ? peeked[0] : '\0';
		var secondChar = peeked.Length > 1 ? peeked[1] : '\0';
		var thirdChar = peeked.Length > 2 ? peeked[2] : '\0';

		if(firstChar != '-')
			return char.IsLetter(firstChar) || firstChar == '_';
		else if(secondChar == '-')
			return char.IsLetter(thirdChar);
		else
			return char.IsLetter(secondChar);
	}

	private bool IsIdentifierChar(char c)
	{
		return char.IsLetterOrDigit(c) || c == '_' || c == '-';
	}
}
