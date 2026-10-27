using System.Text;
using ScssNet.Tokens;

namespace ScssNet.Lexing;

internal class UnitValueParser
{
	public UnitValueToken? Parse
	(
		ISourceReader reader, Separator leadingSeparator, Func<Separator> getTrailingSeparator
	)
	{
		if(!IsUnitStart(reader))
			return null;

		var startCoordinates = reader.GetCoordinates();

		var stringBuilder = new StringBuilder();
		ReadAmountInto(reader, stringBuilder);
		var amount = stringBuilder.ToString();

		stringBuilder.Clear();
		ReadUnitInto(reader, stringBuilder);
		var unit = stringBuilder.ToString();

		var span = new SourceSpan(startCoordinates, reader.GetCoordinates());
		return new UnitValueToken(amount, unit, span, leadingSeparator, getTrailingSeparator());
	}

	private static bool IsUnitStart(ISourceReader reader)
	{
		if(reader.End)
			return false;

		var peeked = reader.Peek(2);
		return char.IsDigit(peeked[0]) || (peeked[0] == '-' && char.IsDigit(peeked[1]));
	}

	private static void ReadAmountInto(ISourceReader reader, StringBuilder stringBuilder)
	{
		stringBuilder.Append(reader.Read());

		ReadDigitsInto(reader, stringBuilder);
		if(reader.Peek() == '.')
		{
			stringBuilder.Append(reader.Read());
			ReadDigitsInto(reader, stringBuilder);
		}
	}

	private static void ReadUnitInto(ISourceReader reader, StringBuilder stringBuilder)
	{
		if(!reader.End && reader.Peek() == '%')
			stringBuilder.Append(reader.Read());
		else
		{
			while(!reader.End && char.IsLetter(reader.Peek()))
				stringBuilder.Append(reader.Read());
		}
	}

	private static void ReadDigitsInto(ISourceReader reader, StringBuilder stringBuilder)
	{
		while(!reader.End && char.IsDigit(reader.Peek()))
			stringBuilder.Append(reader.Read());
	}
}
