using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class AtNamespaceParser(Lazy<UrlParser> urlParser)
{
	internal AtNamespace? Parse(SymbolToken atSign, AtKeywordToken atKeywordToken, TokenReader tokenReader)
	{
		if (atKeywordToken.Keyword != AtKeyword.Namespace)
			return null;

		IdentifierToken? prefix = null;
		var url = urlParser.Value.Parse(tokenReader);
		if(url is null)
		{
			prefix = tokenReader.MatchIdentifier();
			url = urlParser.Value.Require(tokenReader);
		}
		var semiColon = tokenReader.MatchSymbol(Symbol.SemiColon);

		return new AtNamespace(atSign, atKeywordToken, prefix, url, semiColon);
	}
}
