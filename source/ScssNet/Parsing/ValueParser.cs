using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class ValueParser
{
	internal IValue? Parse(ITokenReader tokenReader)
	{
		var value = ParseSingle(tokenReader);
		if(value == null)
			return null;

		return ParseList(tokenReader, value) ?? value;
	}

	private IValue? ParseList(ITokenReader tokenReader, IValue firstValue)
	{
		var list = new List<IValue> { firstValue };
		var lastValue = firstValue;
		while (HasTrailingSeparator(lastValue))
		{
			lastValue = ParseSingle(tokenReader);
			if (lastValue is null)
				break;

			list.Add(lastValue);
		}

		return list.Count > 1 ? new ValueList(list) : null;
	}

	private IValue? ParseSingle(ITokenReader tokenReader)
	{
		return tokenReader.Match<IValueToken>();
		// TBA more comples values like function calls
	}

	private static bool HasTrailingSeparator(IValue value)
	{
		return value switch
		{
			ISeparatedToken separatedToken => separatedToken.HasTrailingSeparator(),
			ValueList => false,
			_ => throw new NotImplementedException("Unknow value type")
		};
	}
}
