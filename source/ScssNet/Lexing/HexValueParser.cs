using System.Text;
using ScssNet.Tokens;

namespace ScssNet.Lexing;

internal class HexValueParser
{
	public HexValueToken? Parse(ISourceReader reader)
	{
		if(reader.Peek() != '#')
			return null;

		var startCoordinates = reader.GetCoordinates();

		var stringBuilder = new StringBuilder();
		stringBuilder.Append(reader.Read());
		while(IsHexDigit(reader.Peek()))
			stringBuilder.Append(reader.Read());

		return new HexValueToken(stringBuilder.ToString(), startCoordinates, reader.GetCoordinates());
	}

	private static bool IsHexDigit(char peeked)
	{
		const int lowerCaseA = 'a';
		const int lowerCaseF = 'f';
		const int upperCaseA = 'A';
		const int upperCaseF = 'F';

		return char.IsDigit(peeked) || (lowerCaseA <= peeked && peeked <= lowerCaseF) || (upperCaseA <= peeked && peeked <= upperCaseF);
	}
}
