using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal interface IAtNamespaceParser : IAtRuleParserBase<AtNamespace> { }

internal class AtNamespaceParser(ParserContext parserContext): IAtNamespaceParser
{
	private IUrlParser UrlParser => parserContext.GetParser<IUrlParser>();

	public AtNamespace? Parse(SymbolToken atSign, AtKeywordToken atKeywordToken, TokenReader tokenReader)
	{
		if (atKeywordToken.Keyword != AtKeyword.Namespace)
			return null;

		IdentifierToken? prefix = null;
		var url = UrlParser.Parse(tokenReader);
		if(url is null)
		{
			prefix = tokenReader.MatchIdentifier();
			url = UrlParser.Require(tokenReader);
		}
		var semiColon = tokenReader.MatchSymbol(Symbol.SemiColon);

		return new AtNamespace(atSign, atKeywordToken, prefix, url, semiColon);
	}
}
