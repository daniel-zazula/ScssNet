using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class AtImportParser(Lazy<UrlParser> urlParser, Lazy<MediaQueryParser> mediaQueryParser)
{
	internal AtImport? Parse(SymbolToken atSign, AtKeywordToken atKeywordToken, TokenReader tokenReader)
	{
		if(atKeywordToken.Keyword != AtKeyword.Import)
			return null;

		var url = urlParser.Value.Require(tokenReader);
		var mediaQuery = mediaQueryParser.Value.Parse(tokenReader);
		var semiColon = tokenReader.MatchSymbol(Symbol.SemiColon);

		return new AtImport(atSign, atKeywordToken, url, mediaQuery, semiColon);
	}
}
