using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal interface IAtRuleParser : IParser<IAtRule> { }

internal class AtRuleParser(ParserContext parserContext) : IAtRuleParser
{
	public IAtRule? Parse(TokenReader tokenReader)
	{
		var atSign = tokenReader.MatchSymbol(Symbol.At);
		if(atSign is null)
			return null;

		var atKeywordToken = tokenReader.RequireKeyword<AtKeyword>();
		IAtRule? atRule = ParseAtRule(atSign, atKeywordToken, tokenReader)
			?? ParseAtImport(atSign, atKeywordToken, tokenReader)
			?? ParseAtMedia(atSign, atKeywordToken, tokenReader)
			?? ParseAtNamespace(atSign, atKeywordToken, tokenReader)
			?? throw new NotImplementedException($"At-rule is not implemented.");

		return atRule;
	}

	private IAtRule? ParseAtRule(SymbolToken atSign, AtKeywordToken atKeywordToken, TokenReader tokenReader)
	{
		return parserContext.GetParser<IAtCharsetParser>().Parse(atSign, atKeywordToken, tokenReader);
	}

	private IAtRule? ParseAtImport(SymbolToken atSign, AtKeywordToken atKeywordToken, TokenReader tokenReader)
	{
		return parserContext.GetParser<IAtImportParser>().Parse(atSign, atKeywordToken, tokenReader);
	}

	private IAtRule? ParseAtMedia(SymbolToken atSign, AtKeywordToken atKeywordToken, TokenReader tokenReader)
	{
		return parserContext.GetParser<IAtMediaParser>().Parse(atSign, atKeywordToken, tokenReader);
	}

	private IAtRule? ParseAtNamespace(SymbolToken atSign, AtKeywordToken atKeywordToken, TokenReader tokenReader)
	{
		return parserContext.GetParser<IAtNamespaceParser>().Parse(atSign, atKeywordToken, tokenReader);
	}
}
