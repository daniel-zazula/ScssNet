using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class FunctionCallParser(Lazy<ValueParser> valueParser)
{
	internal FunctionCall? Parse(string name, TokenReader tokenReader)
	{
		var nameToken = tokenReader.MatchIdentifier(name);
		if (nameToken is null)
			return null;

		return Parse(tokenReader, nameToken);
	}

	internal FunctionCall? Parse(TokenReader tokenReader, IdentifierToken name)
	{
		var openParenthesis = tokenReader.MatchSymbol(Symbol.OpenParenthesis);
		if(openParenthesis == null)
			return null;

		var arguments = valueParser.Value.ParseCommaList(tokenReader);

		var closeParenthesis = tokenReader.MatchSymbol(Symbol.CloseParenthesis);
		if(closeParenthesis == null)
			return null;

		return new FunctionCall(name, openParenthesis, arguments, closeParenthesis);
	}
}
