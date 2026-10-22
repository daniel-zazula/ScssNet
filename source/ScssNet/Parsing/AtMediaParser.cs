using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal interface IAtMediaParser : IAtRuleParserBase<AtMedia> { }

internal class AtMediaParser(ParserContext parserContext) : IAtMediaParser
{
	private IMediaQueryParser MediaQueryParser => parserContext.GetParser<IMediaQueryParser>();
	private IBlockParser BlockParser => parserContext.GetParser<IBlockParser>();

	public AtMedia? Parse(SymbolToken atSign, AtKeywordToken atKeywordToken, TokenReader tokenReader)
	{
		if(atKeywordToken.Keyword != AtKeyword.Media)
			return null;

		var mediaQuery = MediaQueryParser.Require(tokenReader);
		var block = BlockParser.Require(tokenReader);

		return new AtMedia(atSign, atKeywordToken, mediaQuery, block);
	}
}
