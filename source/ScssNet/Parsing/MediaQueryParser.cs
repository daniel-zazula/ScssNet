using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class MediaQueryParser
{
	internal IMediaQuery? Parse(TokenReader tokenReader)
	{
		var expression = ParseExpression(tokenReader);
		if(expression is null)
			return null;
		
		return (IMediaQuery?)ParseList(expression, tokenReader) ?? expression;
	}

	internal IMediaQuery Require(TokenReader tokenReader)
	{
		return Parse(tokenReader) ?? RequireValue(tokenReader);
	}

	private MediaQueryList? ParseList(IMediaQueryExpression expression, TokenReader tokenReader)
	{
		SymbolToken? commaToken = tokenReader.MatchSymbol(Symbol.Comma);
		if(commaToken is null)
			return null;

		var items = new List<MediaQueryListItem> { new MediaQueryListItem(expression, commaToken) };
		while(commaToken != null)
		{
			var value = ParseExpression(tokenReader);
			if (value is null)
				break;

			commaToken = tokenReader.MatchSymbol(Symbol.Comma);
			items.Add(new MediaQueryListItem(value, commaToken));
		}

		return new MediaQueryList(items);
	}

	private IMediaQueryExpression? ParseExpression(TokenReader tokenReader)
	{
		var operatorToken = tokenReader.MatchKeyword<MediaQueryOperatorKeywordToken>();
		if(operatorToken is null)
		{
			return ParseValue(tokenReader);
		}

		var value = RequireValue(tokenReader);
		return new MediaQueryUnaryExpression(operatorToken, value);
	}

	private MediaQueryTypeKeywordToken? ParseValue(TokenReader tokenReader)
	{
		return tokenReader.MatchKeyword<MediaQueryTypeKeywordToken>();
	}

	private MediaQueryTypeKeywordToken RequireValue(TokenReader tokenReader)
	{
		return tokenReader.RequireKeyword<MediaQueryTypeKeywordToken>();
	}
}
