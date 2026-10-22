using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal interface IAtCharsetParser : IAtRuleParserBase<AtCharset> { }

internal class AtCharsetParser: IAtCharsetParser
{
	public AtCharset? Parse(SymbolToken atSign, AtKeywordToken atKeywordToken, TokenReader tokenReader)
	{
		if (atKeywordToken.Keyword != AtKeyword.Charset)
			return null;

		var charsetName = tokenReader.RequireString();
		var semiColon = tokenReader.MatchSymbol(Symbol.SemiColon);

		return new AtCharset(atSign, atKeywordToken, charsetName, semiColon);
	}
}
