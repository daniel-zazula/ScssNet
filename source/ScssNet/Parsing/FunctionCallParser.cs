using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal interface IFunctionCallParser: IParser
{
	FunctionCall? Parse(string name, TokenReader tokenReader);
	FunctionCall? Parse(IdentifierToken name, TokenReader tokenReader);
}

internal class FunctionCallParser(ParserContext parserContext): IFunctionCallParser
{
	private IValueParser ValueParser => parserContext.GetParser<IValueParser>();

	public FunctionCall? Parse(string name, TokenReader tokenReader)
	{
		var nameToken = tokenReader.MatchIdentifier(name);
		if (nameToken is null)
			return null;

		return Parse(nameToken, tokenReader);
	}

	public FunctionCall? Parse(IdentifierToken name, TokenReader tokenReader)
	{
		var openParenthesis = tokenReader.MatchSymbol(Symbol.OpenParenthesis);
		if(openParenthesis == null)
			return null;

		var arguments = ValueParser.ParseCommaList(tokenReader);

		var closeParenthesis = tokenReader.MatchSymbol(Symbol.CloseParenthesis);
		if(closeParenthesis == null)
			return null;

		return new FunctionCall(name, openParenthesis, arguments, closeParenthesis);
	}
}
