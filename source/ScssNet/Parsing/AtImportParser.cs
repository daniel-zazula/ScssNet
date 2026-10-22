using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal interface IAtImportParser : IAtRuleParserBase<AtImport> { }

internal class AtImportParser(ParserContext parserContext): IAtImportParser
{
	private IUrlParser UrlParser => parserContext.GetParser<IUrlParser>();
	private IMediaQueryParser MediaQueryParser => parserContext.GetParser<IMediaQueryParser>();

	public AtImport? Parse(SymbolToken atSign, AtKeywordToken atKeywordToken, TokenReader tokenReader)
	{
		if(atKeywordToken.Keyword != AtKeyword.Import)
			return null;

		var url = UrlParser.Require(tokenReader);
		var mediaQuery = MediaQueryParser.Parse(tokenReader);
		var semiColon = tokenReader.MatchSymbol(Symbol.SemiColon);

		return new AtImport(atSign, atKeywordToken, url, mediaQuery, semiColon);
	}
}
