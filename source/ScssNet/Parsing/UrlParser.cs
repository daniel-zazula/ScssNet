using ScssNet.Lexing;
using ScssNet.Structures;

namespace ScssNet.Parsing;

internal interface IUrlParser: IParser<IUrl>, IRequiredParser<IUrl> { }

internal class UrlParser(ParserContext parserContext): IUrlParser
{
	private IFunctionCallParser FunctionCallParser => parserContext.GetParser<IFunctionCallParser>();

	public IUrl? Parse(TokenReader tokenReader)
	{
		var functionUrl = ParseFunctionCall(tokenReader);
		if (functionUrl is not null)
			return functionUrl;

		var stringUrl = tokenReader.MatchString();
		if(stringUrl is null)
			return null;

		return new StringUrl(stringUrl);
	}

	public IUrl Require(TokenReader tokenReader)
	{
		return ParseFunctionCall(tokenReader) ?? new StringUrl(tokenReader.RequireString());
	}

	private IUrl? ParseFunctionCall(TokenReader tokenReader)
	{
		var function = FunctionCallParser.Parse("url", tokenReader);
		if (function is null)
			return null;

		return new FunctionUrl(function);
	}
}
