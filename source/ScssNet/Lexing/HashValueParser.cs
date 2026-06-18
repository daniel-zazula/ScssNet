using System.Text;
using ScssNet.Tokens;

namespace ScssNet.Lexing;

internal class HashValueParser
{
	public HashValueToken? Parse
	(
		ISourceReader reader, Separator leadingSeparator, Func<Separator> getTrailingSeparator
	)
	{
		if(reader.Peek() != '#')
			return null;

		var startCoordinates = reader.GetCoordinates();

		var stringBuilder = new StringBuilder();
		stringBuilder.Append(reader.Read());

		while(IsHexOrIdChar(reader.Peek()))
		{
			stringBuilder.Append(reader.Read());

			if (reader.End)
				break;
		}

		return new HashValueToken
		(
			stringBuilder.ToString(), startCoordinates, reader.GetCoordinates(), leadingSeparator,
			getTrailingSeparator()
		);

		static bool IsHexOrIdChar(char c)
		{
			return char.IsLetterOrDigit(c) || c == '-' || c == '_';
		}
	}
}
