using ScssNet.Lexing;
using ScssNet.Structures;

namespace ScssNet.Parsing;

internal class UrlParser(Lazy<FunctionCallParser> functionCallParser)
{
	internal IUrl? Parse(TokenReader tokenReader)
	{
		var functionUrl = ParseFunctionCall(tokenReader);
		if (functionUrl is not null)
			return functionUrl;

		var stringUrl = tokenReader.MatchString();
		if(stringUrl is null)
			return null;

		return new StringUrl(stringUrl);
	}

	internal IUrl Require(TokenReader tokenReader)
	{
		return ParseFunctionCall(tokenReader) ?? new StringUrl(tokenReader.RequireString());
	}

	private IUrl? ParseFunctionCall(TokenReader tokenReader)
	{
		var function = functionCallParser.Value.Parse("url", tokenReader);
		if (function is null)
			return null;

		return new FunctionUrl(function);
	}
}
