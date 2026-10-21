using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Test.ElementCreation;

internal static class AtCharsetCreation
{
	extension(AtCharset)
	{
		internal static AtCharset Create()
		{
			var at = SymbolToken.Create(Symbol.At);
			var keyword = AtKeywordToken.Create(AtKeyword.Charset, predecessor: at);
			var name = StringToken.Create("\"utf-8\"", predecessor: keyword);
			var semiColon = SymbolToken.Create(Symbol.SemiColon, predecessor: name);

			return new AtCharset(at, keyword, name, semiColon);
		}
	}
}

internal static class StringUrlCreation
{
	extension(StringUrl)
	{
		internal static StringUrl Create(string path, ISourceElement? predecessor)
		{
			return new StringUrl(StringToken.Create(path, predecessor: predecessor));
		}
	}
}
internal static class FunctionUrlCreation
{
	extension(FunctionUrl)
	{
		internal static FunctionUrl Create(string path, ISourceElement? predecessor)
		{
			var function = FunctionCall.Create("url", CreateFunctionUrlParam, predecessor: predecessor);
			return new FunctionUrl(function);

			IValue CreateFunctionUrlParam(ISourceElement? predecessor)
			{
				return StringToken.Create(path, predecessor: predecessor);
			}
		}
	}
}
