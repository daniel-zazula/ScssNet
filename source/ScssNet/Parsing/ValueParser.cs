using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class ValueParser(Lazy<FunctionCallParser> functionCallParser)
{
	internal IValue? Parse(TokenReader tokenReader)
	{
		var value = ParseSingle(tokenReader);
		if(value == null)
			return null;

		return ParseCommaList(tokenReader, value) ?? ParseSpacedList(tokenReader, value) ?? value;
	}

	internal IValue? ParseCommaList(TokenReader tokenReader)
	{
		var value = ParseSingle(tokenReader);
		if(value == null)
			return null;

		return ParseCommaList(tokenReader, value) ?? value;
	}

	private ValueList? ParseCommaList(TokenReader tokenReader, IValue firstValue)
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

	private ValueList? ParseSpacedList(TokenReader tokenReader, IValue firstValue)
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
				FunctionCall functionCall => functionCall.CloseParenthesis.HasTrailingSeparator(),
				_ => throw new NotImplementedException("Unknow value type")
			};
		}
	}

	private IValue? ParseSingle(TokenReader tokenReader)
	{
		var valueToken = tokenReader.Match<IValueToken>();
		if (valueToken is IdentifierToken identifierToken)
		{
			return (IValue?)functionCallParser.Value.Parse(tokenReader, identifierToken) ?? valueToken;
		}

		return valueToken;
	}
}
