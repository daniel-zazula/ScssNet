using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal interface IValueParser: IParser
{
	IValue? ParseCommaList(TokenReader tokenReader);
	IValue RequireCommaOrSpacedList(TokenReader tokenReader);
}

internal class ValueParser(ParserContext parserContext): IValueParser
{
	private IFunctionCallParser FunctionCallParser => parserContext.GetParser<IFunctionCallParser>();

	public IValue? ParseCommaList(TokenReader tokenReader)
	{
		var value = ParseSingle(tokenReader);
		if(value == null)
			return null;

		return ParseCommaList(value, tokenReader) ?? ParseSpacedList(value, tokenReader) ?? value;
	}

	public IValue RequireCommaOrSpacedList(TokenReader tokenReader)
	{
		var value = ParseSingle(tokenReader) ?? throw new NotImplementedException("Implement missing required value");
		return ParseCommaList(value, tokenReader) ?? ParseSpacedList(value, tokenReader) ?? value;
	}

	private ValueList? ParseCommaList(IValue firstValue, TokenReader tokenReader)
	{
		var comma = tokenReader.MatchSymbol(Symbol.Comma);
		if (comma == null)
			return null;

		var items = new List<ValueListItem> { new(firstValue, comma) };
		while(comma is not null)
		{
			var lastValue = ParseSingle(tokenReader);
			if(lastValue is null)
				break;

			comma = tokenReader.MatchSymbol(Symbol.Comma);
			items.Add(new ValueListItem(lastValue, comma));
		}

		return items.Count > 1 ? new ValueList(items) : null;
	}

	private ValueList? ParseSpacedList(IValue firstValue, TokenReader tokenReader)
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
		var valueToken = tokenReader.Match(t => t is IValueToken vt ? vt : default);
		if (valueToken is IdentifierToken identifierToken)
		{
			return (IValue?)FunctionCallParser.Parse(identifierToken, tokenReader) ?? valueToken;
		}

		return valueToken;
	}
}
