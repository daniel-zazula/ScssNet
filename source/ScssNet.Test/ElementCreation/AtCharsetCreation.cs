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
