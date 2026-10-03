using ScssNet.Lexing;

namespace ScssNet.Parsing;

internal class UrlParser(Lazy<FunctionCallParser> functionCallParser)
{
	internal IValue? Parse(TokenReader tokenReader)
	{
		return (IValue?)functionCallParser.Value.Parse("url", tokenReader) ?? tokenReader.MatchString();
	}

	internal IValue Require(TokenReader tokenReader)
	{
		return (IValue?)functionCallParser.Value.Parse("url", tokenReader) ?? tokenReader.RequireString();
	}
}
