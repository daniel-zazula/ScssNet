using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class AtMediaParser(Lazy<MediaQueryParser> mediaQueryParser, Lazy<BlockParser> blockParser)
{
	internal AtMedia? Parse(SymbolToken atSign, AtKeywordToken atKeywordToken, TokenReader tokenReader)
	{
		if(atKeywordToken.Keyword != AtKeyword.Media)
			return null;

		var mediaQuery = mediaQueryParser.Value.Require(tokenReader);
		var block = blockParser.Value.Require(tokenReader);

		return new AtMedia(atSign, atKeywordToken, mediaQuery, block);
	}
}
