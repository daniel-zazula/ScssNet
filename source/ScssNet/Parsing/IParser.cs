using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal interface IParser { }

internal interface IParser<T> : IParser
{
	T? Parse(TokenReader tokenReader);
}

internal interface IRequiredParser<T> : IParser
{
	T Require(TokenReader tokenReader);
}

internal interface IAtRuleParserBase<T>: IParser where T : IAtRule
{
	T? Parse(SymbolToken atSign, AtKeywordToken atKeywordToken, TokenReader tokenReader);
}
