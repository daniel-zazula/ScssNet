using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class AtRuleParser
(
	Lazy<AtCharsetParser> atCharsetParser, Lazy<AtImportParser> atImportParser, Lazy<AtMediaParser> atMediaParser
)
{
	internal IAtRule? Parse(TokenReader tokenReader)
	{
		var atSign = tokenReader.MatchSymbol(Symbol.At);
		if(atSign is null)
			return null;

		var atKeywordToken = tokenReader.RequireKeyword<AtKeyword>();
		IAtRule? atRule = (IAtRule?)atCharsetParser.Value.Parse(atSign, atKeywordToken, tokenReader)
			?? (IAtRule?)atImportParser.Value.Parse(atSign, atKeywordToken, tokenReader)
			?? (IAtRule?)atMediaParser.Value.Parse(atSign, atKeywordToken, tokenReader)
			?? throw new NotImplementedException($"At-rule is not implemented.");

		return atRule;
	}
}
