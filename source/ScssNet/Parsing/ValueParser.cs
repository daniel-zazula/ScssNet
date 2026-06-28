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

		return ParseCommaSeparatedItems(tokenReader, value) ?? ParseSpacedItems(tokenReader, value) ?? value;
	}

	private IValue? ParseCommaSeparatedItems(ITokenReader tokenReader, IValue firstValue)
	{
		var comma = tokenReader.Match(Symbol.Comma);
		if (comma == null)
			return null;

		var items = new List<ValueListItem> { new(firstValue, comma) };
		while(comma is not null)
		{
			var lastValue = ParseSingle(tokenReader);
			if(lastValue is null)
				break;

			comma = tokenReader.Match(Symbol.Comma);
			items.Add(new ValueListItem(lastValue, comma));
		}

		return items.Count > 1 ? new ValueList(items) : null;
	}

	private IValue? ParseSpacedItems(ITokenReader tokenReader, IValue firstValue)
	{
		var items = new List<ValueListItem> { new(firstValue) };
		var lastValue = firstValue;
		while (HasTrailingSeparator(lastValue))
		{
			lastValue = ParseSingle(tokenReader);
			if (lastValue is null)
				break;

			items.Add(new(lastValue));
		}

		return items.Count > 1 ? new ValueList(items) : null;
		
		static bool HasTrailingSeparator(IValue value)
		{
			return value switch
			{
				ISeparatedToken separatedToken => separatedToken.HasTrailingSeparator(),
				ValueList => false,
				_ => throw new NotImplementedException("Unknow value type")
			};
		}
	}

	private IValue? ParseSingle(ITokenReader tokenReader)
	{
		return tokenReader.Match<IValueToken>();
		// TBA more comples values like function calls
	}
}
