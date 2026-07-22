using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class FunctionCallParser(Lazy<ValueParser> valueParser)
{
	internal FunctionCall? Parse(ITokenReader tokenReader, IdentifierToken name)
	{
		var openParenthesis = tokenReader.Match(Symbol.OpenParenthesis);
		if(openParenthesis == null)
			return null;

		var arguments = valueParser.Value.ParseList(tokenReader);

		var closeParenthesis = tokenReader.Match(Symbol.CloseParenthesis);
		if(closeParenthesis == null)
			return null;

		return new FunctionCall(name, openParenthesis, arguments, closeParenthesis);
	}
}
