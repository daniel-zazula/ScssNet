using System.Text;
using ScssNet.Tokens;

namespace ScssNet.Lexing;

internal class UnitValueParser
{
	public UnitValueToken? Parse
	(
		ISourceReader reader, Separator? leadingSeparator, Func<Separator?> getTrailingSeparator
	)
	{
		if(!IsUnitStart(reader))
			return null;

		var startCoordinates = reader.GetCoordinates();

		var stringBuilder = new StringBuilder();
		stringBuilder.Append(reader.Read());

		ReadDigitsTo(reader, stringBuilder);
		if(reader.Peek() == '.')
		{
			stringBuilder.Append(reader.Read());
			ReadDigitsTo(reader, stringBuilder);
		}

		var amount = decimal.Parse(stringBuilder.ToString());

		stringBuilder.Clear();

		if(!reader.End && reader.Peek() == '%')
			stringBuilder.Append(reader.Read());
		else
		{
			while(!reader.End && char.IsLetter(reader.Peek()))
				stringBuilder.Append(reader.Read());
		}

		return new UnitValueToken
		(
			amount, stringBuilder.ToString(), startCoordinates, reader.GetCoordinates(), leadingSeparator,
			getTrailingSeparator()
		);
	}

	private static bool IsUnitStart(ISourceReader reader)
	{
		if(reader.End)
			return false;

		var peeked = reader.Peek(2);
		return char.IsDigit(peeked[0]) || (peeked[0] == '-' && char.IsDigit(peeked[1]));
	}

	private static void ReadDigitsTo(ISourceReader reader, StringBuilder stringBuilder)
	{
		while(!reader.End && char.IsDigit(reader.Peek()))
			stringBuilder.Append(reader.Read());
	}
}
